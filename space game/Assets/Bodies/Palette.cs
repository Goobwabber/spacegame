using UnityEngine;

[CreateAssetMenu(fileName = "Palette", menuName = "Bodies/Palette", order = 2)]
public class Palette : ScriptableObject {
    [Tooltip("Colors for the terrain.")]
    public ColorHeight[] colors;
    [Tooltip("Material for the terrain(to be able to apply the color per layer the material need to be vertex friendly).")]
    public Material material;
    [Tooltip("Material for the sea.")]
    public Material seaMaterial;
    [Tooltip("Enables random coloring instead layer coloring.")]
    public bool randomColoring;
}