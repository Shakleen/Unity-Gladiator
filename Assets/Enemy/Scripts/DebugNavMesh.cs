using UnityEngine;
using UnityEngine.AI;

public class DebugNavMesh : MonoBehaviour
{
    [SerializeField] NavMeshAgent _navMeshAgent;
    
    [Tooltip("Draw nav mesh agent path")] [SerializeField] private bool _drawPath = false;
    [Tooltip("Color to use to draw path")] [SerializeField] private Color _drawPathColor = Color.black;

    [Tooltip("Draw nav mesh agent velocity")] [SerializeField] private bool _drawVelocity = false;
    [Tooltip("Color to use to draw path")] [SerializeField] private Color _drawVelocityColor = Color.red;

    [Tooltip("Draw nav mesh agent desired velocity")] [SerializeField] private bool _drawDesiredVelocity = false;
    [Tooltip("Color to use to draw path")] [SerializeField] private Color _drawDesiredVelocityColor = Color.green;

    private void OnDrawGizmos() 
    {
        if (_drawVelocity)
        {
            Gizmos.color = _drawVelocityColor;
            Gizmos.DrawLine(transform.position, transform.position + _navMeshAgent.velocity);
        }

        if (_drawDesiredVelocity)
        {
            Gizmos.color = _drawVelocityColor;
            Gizmos.DrawLine(transform.position, transform.position + _navMeshAgent.velocity);
        }

        if (_drawPath)
        {
            var agentPath = _navMeshAgent.path;
            Vector3 previousCorner = transform.position;
            Gizmos.color = _drawPathColor;
            
            foreach(var corner in agentPath.corners)
            {
                Gizmos.DrawLine(previousCorner, corner);
                Gizmos.DrawSphere(corner, 0.1f);
                previousCorner = corner;
            }
        }
    }
}
