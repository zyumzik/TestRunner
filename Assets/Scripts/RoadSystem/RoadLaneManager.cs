using System.Collections.Generic;
using UnityEngine;

namespace RoadSystem
{
    public class RoadLaneManager
    {
        private readonly List<RoadLane> _lanes = new();
        private RoadLane _currentLane;
        private readonly int _startRoadLaneIndex;

        public RoadLaneManager(Transform[] laneTransforms, int startIndex = 1)
        {
            for (int i = 0; i < laneTransforms.Length; i++)
            {
                _lanes.Add(new RoadLane(i, laneTransforms[i]));
            }

            for (int i = 0; i < _lanes.Count; i++)
            {
                if (i > 0)
                    _lanes[i].SetNeighbor(RoadLane.Direction.Left, _lanes[i - 1]);
                if (i < _lanes.Count - 1)
                    _lanes[i].SetNeighbor(RoadLane.Direction.Right, _lanes[i + 1]);
            }

            _startRoadLaneIndex = startIndex;
            _currentLane = _lanes[_startRoadLaneIndex];
        }

        /// <summary>
        /// Resets current road lane
        /// </summary>
        /// <returns>Start road lane's transform</returns>
        public Transform Reset()
        {
            _currentLane = _lanes[_startRoadLaneIndex];
            return _currentLane.LaneTransform;
        }
        
        public RoadLane GetCurrentLane() => _currentLane;

        public bool TryChangeLane(RoadLane.Direction direction, out RoadLane newLane)
        {
            if (_currentLane.TryGetNeighbor(direction, out var neighbor))
            {
                _currentLane = neighbor;
                newLane = _currentLane;
                return true;
            }

            newLane = null;
            return false;
        }
    }
}