using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.BaseLogic
{
    public struct JointData
    {
        private readonly List<Vector3> _points;
        private readonly Joint _joint;

        public JointData(Vector3 firstPoint, Joint joint)
        {
            _points = new()
            {
                firstPoint,
            };

            _joint = joint;
        }

        public IReadOnlyList<Vector3> Points => _points;
        public int PointsCount => _points.Count;
        public Joint Joint => _joint;

        public Vector3 GetLastFixedPoint()
        {
            if(_points.Count == 0)
                throw new Exception($"{nameof(_points)} is empty");
            
            return _points.Last();
        }

        public bool TryGetPenultimateFixedPoint(out Vector3 point)
        {
            point = Vector3.zero;
            
            if (_points.Count <= 1)
                return false;

            point = _points[_points.Count - 2];

            return true;
        }

        public void TryAdd(Vector3 point)
        {
            if(GetLastFixedPoint() != point)
                _points.Add(point);
        }

        public void RemoveLastPoint() =>
            _points.RemoveAt(_points.Count - 1);
    }
}