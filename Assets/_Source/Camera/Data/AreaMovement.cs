using System;
using System.Linq;
using UnityEngine;

namespace Camera.Data
{
    [Serializable]
    public class AreaMovement
    {
        private SpatialRegion _spatialRegion;
        
        public void Init()
        {
            _spatialRegion = new();
        }

        public void Rebuild(Vector3[] newPositions)
        {
            _spatialRegion.AddPoints(newPositions);
        }
        public Vector3 GetPositionInArea(Vector3 targetPosition) => _spatialRegion.ClampXZ(targetPosition);
        public Vector3 CenterOfArea => _spatialRegion.GetBounds().center;
        public Bounds GetBounds() => _spatialRegion.GetBounds();
    }
}