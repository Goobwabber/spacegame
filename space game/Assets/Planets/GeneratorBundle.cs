using Generation;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneratorBundle", menuName = "Planets/Bundles/GeneratorBundle", order = 1)]
public class GeneratorBundle : ScriptableObject {
    [Tooltip("Noise to modify the terrain shape.")]
    public GenerationData[] generators;
    [Tooltip("Level of the sea.")]
    public float seaLevel;
}
