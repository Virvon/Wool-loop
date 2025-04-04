using System;
using UnityEngine;

namespace Sources.Infrastructure.JointModel
{
    [Serializable]
    public struct JointValidationData
    {
        [SerializeField] public int Id;
        [SerializeField] public Vector2 Position;
        [SerializeField] public float TargetAngle;
    }
}