using UnityEngine;
using Generation;
using System;

public class Planet {

    public Transform planet;
    public string name;
    public int seed;
    public int radius;
    public GeneratorBundle generator;
    public PaletteBundle palette;

    private ColorHeight[] colors;
    private int subdivisions;

    public Planet(
        Transform planet,
        string name,
        int seed,
        int radius,
        GeneratorBundle generator,
        PaletteBundle palette
    ) {
        this.planet = planet;
        this.name = name;
        this.seed = seed;
        this.radius = radius;
        this.generator = generator;
        this.palette = palette;

        this.colors = new ColorHeight[palette.colors.Length];
        for(int i = 0; i < palette.colors.Length; i++) {
            ColorHeight color = new ColorHeight();
            color.layer = palette.colors[i].layer + (int)generator.seaLevel;
            color.color = palette.colors[i].color;
            if (color.layer < 0) {
                color.layer = 0;
            }
            this.colors[i] = color;
        }
        this.colors[colors.Length-1].layer = 0;

        this.subdivisions = (int)Math.Floor(Math.Log(radius / 42.0, Math.Sqrt(2.0)) + 3);
    }

    public void createTerrain() {
        TerrainGen.AddTerrainToQueue(
            this.planet,
            new Vector3(0, 0, 0),
            this.seed,
            false,
            Style.LowPoly,
            this.radius + this.generator.seaLevel,
            this.generator.generators,
            this.colors,
            new PopulateData[] {},
            this.palette.material,
            this.palette.seaMaterial,
            this.radius,
            this.subdivisions,
            1
        );

        TerrainGen.StartDataQueue();
    }
}