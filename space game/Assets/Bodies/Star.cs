using UnityEngine;
using Generation;
using System;

public class Star {

    public Transform star;
    public string name;
    public int seed;
    public int radius;
    public TerrainType terrain;
    public Palette palette;

    private ColorHeight[] colors;

    // Constructor
    public Star(
        Transform star,
        string name,
        int seed,
        int radius,
        TerrainType terrain,
        Palette palette
    ) {
        // Set variables
        this.star = star;
        this.name = name;
        this.seed = seed;
        this.radius = radius;
        this.terrain = terrain;
        this.palette = palette;

        // Adjust palette layers based on sea level
        this.colors = new ColorHeight[palette.colors.Length];
        for(int i = 0; i < palette.colors.Length; i++) {
            ColorHeight colorHeight = new ColorHeight();
            colorHeight.layer = palette.colors[i].layer + (int)terrain.seaLevel;
            colorHeight.color = palette.colors[i].color;
            if (colorHeight.layer < 0) {
                colorHeight.layer = 0;
            }
            this.colors[i] = colorHeight;
        }
        this.colors[colors.Length-1].layer = 0;
    }

    public void createTerrain() {
        TerrainGen.AddTerrainToQueue(
            this.star,
            new Vector3(0, 0, 0),
            this.seed,
            false,
            Style.LowPoly,
            this.radius + this.terrain.seaLevel,
            this.terrain.generators,
            this.colors,
            new PopulateData[] {},
            this.palette.material,
            this.palette.seaMaterial,
            this.palette.randomColoring,
            this.radius,
            4, // Do not adjust subdivisions based on size, stars are large and can cause intense lag.
            1
        );

        TerrainGen.StartDataQueue();
    }

    public void createLight() {
        this.star.gameObject.AddComponent<Light>();
        Light light = this.star.gameObject.GetComponent<Light>();

        light.type = LightType.Point;
        light.intensity = 3;
        light.range = 1000;
    }
}