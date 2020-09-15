using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Generation;

public class planetBehavior : MonoBehaviour {
    public PlanetData[] planets;
    public GameObject viewer;
    public float viewDistance;
    static public float G_viewDistance;
    private PlanetData[] lastPlanets = new PlanetData[]{};

	// void Start () {
    //     if(planets != lastPlanets) {
    //         foreach (var item in planets){
    //             Planet.AddPlanetToQueue(
    //                 item.newName, 
    //                 item.position, 
    //                 item.seed, 
    //                 item.generateRandomSeed, 
    //                 item.terrainStyle, 
    //                 item.seaLevel, 
    //                 item.generationData, 
    //                 item.ColorPerLayer, 
    //                 item.population, 
    //                 item.material, 
    //                 item.seaMaterial, 
    //                 item.radius, 
    //                 item.subdivisions, 
    //                 item.chunckSubdivisions
    //             );

    //             Planet.StartDataQueue();
    //         }
    //         lastPlanets = planets;
    //     }
	// }
	
	void Update () {
        if(planets != lastPlanets) {
            foreach (var item in planets){
                Planet.AddPlanetToQueue(
                    item.newName, 
                    item.position, 
                    item.seed, 
                    item.generateRandomSeed, 
                    item.terrainStyle, 
                    item.seaLevel, 
                    item.generationData, 
                    item.ColorPerLayer, 
                    item.population, 
                    item.material, 
                    item.seaMaterial, 
                    item.radius, 
                    item.subdivisions, 
                    item.chunckSubdivisions
                );

                Planet.StartDataQueue();
            }
            lastPlanets = planets;
        }

		G_viewDistance = viewDistance;
        Planet.InstantiateIntoWorld();
        Planet.HideAndShow(viewer.transform.position);
	}
}

