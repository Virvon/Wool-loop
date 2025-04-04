using Sources.Infrastructure.JointModel;
using UnityEngine;

namespace Sources.BaseLogic.JointLogic
{
    public class Joint : MonoBehaviour
    {
        [SerializeField] private float _radius;

        public float Radius => _radius;

        public void Initialize(JointValidationData jointValidationData)
        {
        }
    }
}