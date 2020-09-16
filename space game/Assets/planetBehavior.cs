using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehavior : MonoBehaviour {

    public string planetName;
    public int seed;
    public int radius;
    public GeneratorBundle generator;
    public PaletteBundle palette;
    public float rotationSpeed;

    private Planet planet;

	void Start () {
		planet = new Planet(
            gameObject.transform,
            planetName,
            seed,
            radius,
            generator,
            palette
        );

        planet.createTerrain();
	}
	

	void Update () {
		transform.Rotate(new Vector3(0, rotationSpeed, 0), Space.Self);
	}
}
