using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Camera
{
    public class SpatialRegion
    {
        private List<Vector3> _points;
        private Bounds _bounds;
        private bool _isInitialized;

        public SpatialRegion()
        {
            _points = new List<Vector3>();
            _isInitialized = false;
        }

        public void AddPoint(Vector3 point)
        {
            _points.Add(point);
            _isInitialized = false;
        }

        public void AddPoints(IEnumerable<Vector3> points)
        {
            _points.AddRange(points);
            _isInitialized = false;
        }

        public void RemovePoint(Vector3 point)
        {
            _points.Remove(point);
            _isInitialized = false;
        }

        public void Clear()
        {
            _points.Clear();
            _isInitialized = false;
        }

        private void Initialize()
        {
            if (_points.Count == 0)
            {
                _bounds = new Bounds(Vector3.zero, Vector3.zero);
                return;
            }

            var minX = _points.Min(p => p.x);
            var maxX = _points.Max(p => p.x);
            var minY = _points.Min(p => p.y);
            var maxY = _points.Max(p => p.y);
            var minZ = _points.Min(p => p.z);
            var maxZ = _points.Max(p => p.z);

            var center = new Vector3(
                (minX + maxX) / 2f,
                (minY + maxY) / 2f,
                (minZ + maxZ) / 2f
            );

            var size = new Vector3(
                maxX - minX,
                maxY - minY,
                maxZ - minZ
            );
            
            _points.ForEach(point => point.y = (maxY + minY) / 2);

            _bounds = new Bounds(center, size);
            _isInitialized = true;
        }

        public bool Contains(Vector3 point)
        {
            if (!_isInitialized) Initialize();

            return _bounds.Contains(point);
        }

        public Vector3 GetDirectionToBoundary(Vector3 point)
        {
            if (!_isInitialized) Initialize();
            
            if (_bounds.Contains(point))
            {
                var closestPoint = _bounds.ClosestPoint(point);
                return (closestPoint - point).normalized;
            }
            else
            {
                var closestPoint = _bounds.ClosestPoint(point);
                return (closestPoint - point).normalized;
            }
        }
        
        public Vector3 ClampXZ(Vector3 targetPosition)
        {
            if (!_isInitialized) Initialize();
            Vector3 min = _bounds.min;
            Vector3 max = _bounds.max;
            float x = Mathf.Clamp(targetPosition.x, min.x, max.x);
            float z = Mathf.Clamp(targetPosition.z, min.z, max.z);
            return new Vector3(x, targetPosition.y, z);
        }
        public Bounds GetBounds() => _bounds;

        public bool Intersects(SpatialRegion other)
        {
            if (!_isInitialized) Initialize();
            if (!other._isInitialized) other.Initialize();

            return _bounds.Intersects(other._bounds);
        }

        public int PointCount => _points.Count;

        public IEnumerable<Vector3> Points => _points.AsEnumerable();
    }
}