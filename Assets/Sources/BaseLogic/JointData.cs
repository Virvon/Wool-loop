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
        
        private float _angle;

        public JointData(Vector3 firstPoint, Joint joint)
        {
            _points = new()
            {
                firstPoint,
            };

            _joint = joint;
            
            _angle = 0;
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
            if(GetLastFixedPoint() == point)
                return;
            
            _points.Add(point);
            AddAngle();
        }

        public void RemoveLastPoint()
        {
            RemoveAngle();
            _points.Remove(_points.Last());
        }

        private void RemoveAngle()
        {
            if (_points.Count <= 1)
            {
                _angle = 0;
                return;
            }
            
            _angle -= CalculateAngle(_points[_points.Count - 2], _points.Last());
        }

        private void AddAngle()
        {
            if(_points.Count <= 1)
                return;
            
            _angle += CalculateAngle(_points[_points.Count - 2], _points.Last());
        }

        private float CalculateAngle(Vector3 penultimatePoint, Vector3 lastPoint)
        {
            Vector3 penultimatePointDirection = (penultimatePoint - _joint.transform.position).normalized;
            Vector3 lastPointDirection = (lastPoint - _joint.transform.position).normalized;

            float angle = Vector3.Angle(penultimatePointDirection, lastPointDirection);
            int modifier = Vector3.Cross(lastPointDirection, penultimatePointDirection).z < 0 ? -1 : 1;

            return angle * modifier;
        }
    }
}