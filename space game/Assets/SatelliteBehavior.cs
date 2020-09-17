using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteBehavior : MonoBehaviour {

    public Transform target;
    public float distance = 50f;
    public float speed = 0.1f;

    float yRotation = 0f;
    float objectRadius;

	// Use this for initialization
	void Start () {
        objectRadius = target.GetComponent<PlanetBehavior>().radius;
        objectRadius = target.GetComponent<StarBehavior>().radius;
    }
	
	// Update is called once per frame
	void Update () {
        yRotation += speed;
		Quaternion toRotation = Quaternion.Euler(0, yRotation, 0);
        Quaternion rotation = toRotation;

        Vector3 negDistance = new Vector3(0.0f, 0.0f, -(distance + objectRadius));
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
	}
}
