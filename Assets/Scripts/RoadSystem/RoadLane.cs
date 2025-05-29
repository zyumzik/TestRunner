using System.Collections.Generic;
using UnityEngine;

namespace RoadSystem
{
    public class RoadLane
    {
        public int Index { get; }
        public Transform LaneTransform { get; }

        private readonly Dictionary<Direction, RoadLane> _neighbors = new();

        public RoadLane(int index, Transform laneTransform)
        {
            Index = index;
            LaneTransform = laneTransform;
        }

        public void SetNeighbor(Direction direction, RoadLane neighbor)
        {
            _neighbors[direction] = neighbor;
        }

        public bool TryGetNeighbor(Direction direction, out RoadLane neighbour)
        {
            return _neighbors.TryGetValue(direction, out neighbour);
        }

        public enum Direction
        {
            Left,
            Right
        }
    }
}