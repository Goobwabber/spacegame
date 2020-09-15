using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonBehavior : MonoBehaviour {

    Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 gravity = (transform.position - new Vector3(0, 0, 0)).normalized * -20f;
		rigidbody.AddForce(gravity);

        float rotationTorque = 10f;
        Vector3 targetPosition = transform.InverseTransformPoint(new Vector3(0, 0, 0));
        float requiredTorqueX = (targetPosition.x / targetPosition.magnitude);
        float requiredTorqueZ = (targetPosition.z / targetPosition.magnitude);
        Vector3 torque = new Vector3(rotationTorque * requiredTorqueZ, rotationTorque * requiredTorqueX * -1, 0f);
        rigidbody.AddRelativeTorque(torque.z, 0f, torque.y, ForceMode.Force);
	}
}
