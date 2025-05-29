using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/GameConfiguration")]
    public class GameConfiguration : ScriptableObject
    {
        [field: SerializeField] public float ScorePerSecond { get; private set; }
    }
}