using DG.Tweening;
using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/PlayerConfiguration")]
    public class PlayerConfiguration : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public float ChangeLaneDuration { get; private set; }
        [field: SerializeField] public Ease ChangeLaneEase { get; private set; }
    }
}