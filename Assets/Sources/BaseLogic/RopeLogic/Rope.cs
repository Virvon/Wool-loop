using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sources.BaseLogic.RopeLogic
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        public void SetUp(Vector3 startPosition, Vector3 endPosition)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPositions(new Vector3[] {startPosition, endPosition});
        }

        public void UpdateRenderer(IReadOnlyList<Vector3> points)
        {
            _lineRenderer.positionCount = points.Count;
            _lineRenderer.SetPositions(points.ToArray());
        }

        public void UpdateEndPoint(int endPointIndex, Vector3 position) =>
            _lineRenderer.SetPosition(endPointIndex, position);
    }
}
