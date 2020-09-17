using UnityEngine;
using Generation;
using System;

public class Star : Body {
    // Constructor
    public Star(
        Transform star,
        string name,
        int seed,
        int radius,
        float rotationSpeed,
        TerrainType terrain,
        Palette palette
    ) : base(star, name, seed, radius, rotationSpeed, terrain, palette) {
        createLight();
    }

    private void createLight() {
        this.body.gameObject.AddComponent<Light>();
        Light light = this.body.gameObject.GetComponent<Light>();

        light.type = LightType.Point;
        light.intensity = 3;
        light.range = 1000;
    }
}