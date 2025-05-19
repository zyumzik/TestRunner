using UnityEngine;

namespace RoadSystem
{
    public class RoadChunk : MonoBehaviour
    {
        [SerializeField] private BoxCollider _floorCollider;
        
        public float HalfLength => _floorCollider.size.z / 2;
        
        public void Initialize()
        {
            
        }
    }
}