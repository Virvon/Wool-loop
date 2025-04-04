using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Infrastructure.JointModel;
using UnityEngine;
using Joint = Sources.BaseLogic.JointLogic.Joint;

namespace Sources.Infrastructure.RopeModel
{
    public class RopeModel
    {
        private List<JointData> _joints;
        private Vector3 _endPoint;
        private Vector3 _startPoint;
        private int _endPointIndex;

        public RopeModel(Vector3 startPosition)
        {
            _joints = new();

            _endPoint = startPosition;
            _startPoint = startPosition;
            
            OnUpdated();
        }

        public event Action<int, Vector3> Moved;
        public event Action<IReadOnlyList<Vector3>> Updated;

        public Vector3 StartPoint => _startPoint;
        public Vector3 EndPoint => _endPoint;

        public void Move(Vector2 position)
        {
            _endPoint = position;
            Moved?.Invoke(_endPointIndex, _endPoint);
            
            Vector2 startCastPosition = _endPoint;
            Vector2 endCastPosition = GetLastFixedPoint();

            if (TryCircleCast(startCastPosition, endCastPosition, out RaycastHit2D hitInfo)
                && hitInfo.transform.TryGetComponent(out Joint joint))
            {
                if (TryGetLastJoint(out Joint lastJoint) == false || joint != lastJoint)
                {
                    Vector2 pointPosition = GetPointPosition(hitInfo.point, joint);
                    JointData newJointData = new JointData(pointPosition, joint);
                    _joints.Add(newJointData);
                }
                else
                {
                    JointData jointData = GetLastJointData();
                    Vector2 pointPosition = GetPointPosition(hitInfo.point, jointData.Joint);

                    jointData.TryAdd(pointPosition);

                    _joints[_joints.Count - 1] = jointData;
                }
                
                OnUpdated();
            }
            else if(TryGetLastJoint(out Joint lastJoint))
            {
                Vector3 penultimateFixedPoint = GetPenultimateFixedPoint();
                
                Debug.DrawLine(_endPoint, GetLastFixedPoint(), Color.red);
                Debug.DrawLine(penultimateFixedPoint, GetLastFixedPoint(), Color.red);
                Debug.DrawLine(penultimateFixedPoint, _endPoint, Color.red);

                if (TryCircleCast(_endPoint, penultimateFixedPoint, out hitInfo) == false)
                {
                    if (GetLastJointData().PointsCount <= 1)
                    {
                        Vector3 lastFixedPoint = GetLastFixedPoint();
                        
                        float dot = Vector2.Dot(
                            ((_endPoint - lastFixedPoint).normalized + (penultimateFixedPoint - lastFixedPoint).normalized).normalized,
                            (lastJoint.transform.position - lastFixedPoint).normalized);
                        
                        if (dot < 0)
                            _joints.RemoveAt(_joints.Count - 1);
                    }
                    else
                    {
                        JointData jointData = GetLastJointData();
                        jointData.RemoveLastPoint();
                        _joints[_joints.Count - 1] = jointData;
                    }
                    
                    OnUpdated();
                }
            }
        }

        private void OnUpdated()
        {
            List<Vector3> points = new();

            points.Add(_startPoint);

            foreach (JointData _jointData in _joints)
                points.AddRange(_jointData.Points);

            points.Add(_endPoint);
            _endPointIndex = points.Count - 1;

            Updated?.Invoke(points);
        }

        private Vector3 GetPenultimateFixedPoint()
        {
            if (TryGetPenultimateFixedPoint(out Vector3 point) == false)
            {
                if (_joints.Count <= 1)
                    return _startPoint;
                else
                    return _joints[_joints.Count - 2].GetLastFixedPoint();
            }
            else
            {
                return point;
            }
        }

        private Vector2 GetPointPosition(Vector2 contactPosition, Joint joint)
        {
            float jointOffset = 0.1f;
            
            Vector2 directionToPoint =
                (contactPosition - new Vector2(joint.transform.position.x, joint.transform.position.y))
                .normalized;

            return directionToPoint * (joint.Radius + jointOffset) + new Vector2(joint.transform.position.x, joint.transform.position.y);
        }

        private JointData GetLastJointData()
        {
            if (TryGetLastsJointData(out JointData jointData) == false)
                throw new Exception($"{nameof(_joints)} is empty");

            return jointData;
        }

        private bool TryGetLastJoint(out Joint joint)
        {
            joint = null;
            
            if (TryGetLastsJointData(out JointData jointData) == false)
                return false;

            joint = jointData.Joint;

            return true;
        }

        private bool TryGetLastsJointData(out JointData jointData)
        {
            jointData = new ();

            if (_joints.Count == 0)
                return false;

            jointData = _joints.Last();

            return true;
        }

        private Vector3 GetLastFixedPoint()
        {
            if (TryGetLastsJointData(out JointData jointData) == false)
                return _startPoint;
            
            return _joints.Last().GetLastFixedPoint();
        }

        private bool TryGetPenultimateFixedPoint(out Vector3 point) =>
            _joints.Last().TryGetPenultimateFixedPoint(out point);
        
        private bool TryCircleCast(Vector2 startCastPosition, Vector2 endCastPosition, out RaycastHit2D hitInfo)
        {
            hitInfo = Physics2D.CircleCast(
                startCastPosition,
                0.1f,
                (endCastPosition - startCastPosition).normalized,
                (endCastPosition - startCastPosition).magnitude);

            return hitInfo.collider != null;
        }
    }
}