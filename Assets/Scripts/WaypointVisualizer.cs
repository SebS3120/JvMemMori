using UnityEngine;

public class WaypointVisualizer : MonoBehaviour
{
    public Transform nextWaypoint;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.3f);

        if (nextWaypoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, nextWaypoint.position);
        }
    }
}