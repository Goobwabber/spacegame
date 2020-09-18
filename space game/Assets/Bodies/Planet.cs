using UnityEngine;
using Generation;
using System;

public class Planet : Body {

    private GameObject selector;
    private Material selectMaterial;
    private int lastSelected;

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
        if (lastSelected != triangleIndex) {
            Vector3 v1 = mesh.vertices[mesh.triangles[triangleIndex * 3]];
            Vector3 v2 = mesh.vertices[mesh.triangles[triangleIndex * 3 + 1]];
            Vector3 v3 = mesh.vertices[mesh.triangles[triangleIndex * 3 + 2]];
            Vector3 direction = Vector3.Cross(v1-v2, v1-v3).normalized;
            Vector3[] vertices = new Vector3[] {
                v1 + (-direction * 0.1f),
                v2 + (-direction * 0.1f),
                v3 + (-direction * 0.1f),
                v1 + (direction * 2),
                v2 + (direction * 2),
                v3 + (direction * 2)
            };

            Mesh selectorMesh = new Mesh();
            selectorMesh.vertices = vertices;
            selectorMesh.triangles = new int[] {0, 1, 2, 3, 4, 5, 0, 3, 1, 1, 4, 3, 1, 4, 2, 2, 5, 4, 2, 5, 0, 0, 3, 5};
            selectorMesh.RecalculateBounds();
            selectorMesh.RecalculateNormals();
            selectorMesh.RecalculateTangents();

            selector.transform.GetComponent<MeshFilter>().mesh = selectorMesh;
            lastSelected = triangleIndex;
        }
    }
}