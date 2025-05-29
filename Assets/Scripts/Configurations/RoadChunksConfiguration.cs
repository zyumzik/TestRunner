using RoadSystem;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/RoadChunksConfiguration")]
    public class RoadChunksConfiguration : ScriptableObject
    {
        [field: SerializeField] public RoadChunk[] RoadChunkPrefabs {get; private set; }
        [field: SerializeField] public int StartChunks {get; private set; }
        [field: SerializeField] public int MaxChunks {get; private set; }
        [field: SerializeField] public int InitialPoolSize {get; private set; }
        [field: SerializeField] public float DeconstructRoadDistance {get; private set; }
        [field: SerializeField] public float ClearRoadsOnReviveDistance {get; private set; }
    }
}