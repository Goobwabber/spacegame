using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class BodyBehavior : MonoBehaviour {

    public BodyType bodyType;
    public string bodyName;
    public int seed;
    public int radius;
    public float rotationSpeed;
    public TerrainType terrain;
    public Palette palette;
    [Space(10)]
    public Material selectionMaterial;

    public Body body;

	void Start () {
        switch(bodyType) {
            case BodyType.Body: body = new Body(transform, bodyName, seed, radius, rotationSpeed, terrain, palette); break;
            case BodyType.Star: body = new Star(transform, bodyName, seed, radius, rotationSpeed, terrain, palette); break;
            case BodyType.Planet: body = new Planet(transform, bodyName, seed, radius, rotationSpeed, terrain, palette, selectionMaterial); break;
        }
	}
	
	void Update () {
		body.update();
	}
}

public enum BodyType {
    Body,
    Star,
    Planet
}