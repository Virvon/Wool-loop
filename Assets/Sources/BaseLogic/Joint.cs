using System.Collections.Generic;
using UnityEngine;

namespace Sources.BaseLogic
{
    public class Joint : MonoBehaviour
    {
        [SerializeField] private float _radius;

        public float Radius => _radius;
    }
}