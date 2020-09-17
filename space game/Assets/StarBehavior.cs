using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBehavior : MonoBehaviour {

    public string starName;
    public int seed;
    public int radius;
    public TerrainType terrain;
    public Palette palette;
    public float rotationSpeed;

    private Star star;

	void Start () {
		star = new Star(
            gameObject.transform,
            starName,
            seed,
            radius,
            terrain,
            palette
        );

        star.createTerrain();
        star.createLight();
	}
	

	void Update () {
		transform.Rotate(new Vector3(0, rotationSpeed, 0), Space.Self);
	}
}
