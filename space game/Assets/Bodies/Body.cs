using UnityEngine;
using Generation;
using System;

public class Body {

    public Transform body;
    public string name;
    public int seed;
    public int radius;
    public float rotationSpeed;
    public TerrainType terrain;
    public Palette palette;

    private ColorHeight[] colors;
    private int subdivisions;

    // Constructor
    public Body(
        Transform body,
        string name,
        int seed,
        int radius,
        float rotationSpeed,
        TerrainType terrain,
        Palette palette
    ) {
        // Set Variables
        this.body = body;
        this.name = name;
        this.seed = seed;
        this.radius = radius;
        this.rotationSpeed = rotationSpeed;
        this.terrain = terrain;
        this.palette = palette;

        createTerrain();
    }

    private void createTerrain() {
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

        // Adjust subdivisions based on body radius
        this.subdivisions = (int)Math.Floor(Math.Log(radius / 42.0, Math.Sqrt(2.0)) + 3);
        if(this.subdivisions > 5) {
            this.subdivisions = 5;
        }

        TerrainGen.AddTerrainToQueue(
            this.body,
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
            this.subdivisions,
            1
        );

        TerrainGen.StartDataQueue();
    }

    public void update() {
        body.transform.Rotate(new Vector3(0, rotationSpeed, 0), Space.Self);
    }
}