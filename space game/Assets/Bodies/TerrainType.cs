using Generation;
using UnityEngine;

[CreateAssetMenu(fileName = "Terrain", menuName = "Bodies/Terrain", order = 1)]
public class TerrainType : ScriptableObject {
    [Tooltip("Noise to modify the terrain shape.")]
    public GenerationData[] generators;
    [Tooltip("Level of the sea.")]
    public float seaLevel;
}
