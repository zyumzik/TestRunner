using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/RoadMovementConfiguration")]
    public class RoadMovementConfiguration : ScriptableObject
    {
        [field: SerializeField] public Vector3 MovementDirection { get; private set; }
        [field: SerializeField] public float MinSpeed { get; private set; }
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float SpeedUpDuration { get; private set; }
        [field: SerializeField] public AnimationCurve SpeedCurve { get; private set; }
    }
}