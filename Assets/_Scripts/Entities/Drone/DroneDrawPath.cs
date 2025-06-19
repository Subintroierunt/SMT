using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    public class DroneDrawPath : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private NavMeshAgent agent;

        private bool drawPath;

        public void SetDraw(bool value) =>
            drawPath = value;

        private void Update()
        {
            if (drawPath && agent.path != null)
            {
                Vector3[] corners = agent.path.corners;
                lineRenderer.positionCount = corners.Length;
                lineRenderer.SetPositions(corners);
            }
            else
            {
                lineRenderer.positionCount = 0;
            }
        }
    }
}
