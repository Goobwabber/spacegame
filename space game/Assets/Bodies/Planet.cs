using UnityEngine;
using Generation;
using System;

public class Planet : Body {

    private GameObject selector;
    private Material selectMaterial;

    // Constructor
    public Planet(
        Transform planet, 
        string name, 
        int seed,
        int radius,
        float rotationSpeed,
        TerrainType terrain,
        Palette palette,
        Material selectMaterial
    ) : base(planet, name, seed, radius, rotationSpeed, terrain, palette) {
        this.selectMaterial = selectMaterial;
        createSelector();
    }

    private void createSelector() {
        selector = new GameObject();
        selector.transform.parent = this.body;
        selector.AddComponent<MeshFilter>();
        MeshRenderer renderer = selector.AddComponent<MeshRenderer>();
        renderer.material = selectMaterial;
    }

    public void select(int triangleIndex, Mesh mesh) {
        int t1 = mesh.triangles[triangleIndex * 3];
        int t2 = mesh.triangles[triangleIndex * 3 + 1];
        int t3 = mesh.triangles[triangleIndex * 3 + 2];
        Vector3[] vertices = new Vector3[] {
            mesh.vertices[t1] + ((-mesh.vertices[t1] + body.transform.position).normalized * 0.1f),
            mesh.vertices[t2] + ((-mesh.vertices[t2] + body.transform.position).normalized * 0.1f),
            mesh.vertices[t3] + ((-mesh.vertices[t3] + body.transform.position).normalized * 0.1f),
            mesh.vertices[t1] + ((mesh.vertices[t1] - body.transform.position).normalized * 2),
            mesh.vertices[t2] + ((mesh.vertices[t2] - body.transform.position).normalized * 2),
            mesh.vertices[t3] + ((mesh.vertices[t3] - body.transform.position).normalized * 2)
        };

        Mesh selectorMesh = new Mesh();
        selectorMesh.vertices = vertices;
        selectorMesh.triangles = new int[] {0, 1, 2, 3, 4, 5, 0, 3, 1, 1, 4, 3, 1, 4, 2, 2, 5, 4, 2, 5, 0, 0, 3, 5};
        selectorMesh.RecalculateBounds();
        selectorMesh.RecalculateNormals();
        selectorMesh.RecalculateTangents();

        selector.transform.GetComponent<MeshFilter>().mesh = selectorMesh;
    }
}