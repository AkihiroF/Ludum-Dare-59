using System;
using System.Linq;
using UnityEngine;

namespace Camera.Data
{
    [Serializable]
    public class AreaMovement
    {
        [SerializeField] public Transform[] pointArea;
        private SpatialRegion _spatialRegion;
        
        public void Init()
        {
            _spatialRegion = new();
            _spatialRegion.AddPoints(pointArea.Select(point => point.position));
        }
        public Vector3 GetPositionInArea(Vector3 targetPosition) => _spatialRegion.ClampXZ(targetPosition);
        public Vector3 CenterOfArea => _spatialRegion.GetBounds().center;
        public Bounds GetBounds() => _spatialRegion.GetBounds();
    }
}