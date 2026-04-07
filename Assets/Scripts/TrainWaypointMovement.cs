using UnityEngine;

public class TrainWaypointMovement : MonoBehaviour
{
    public float speed = 5f;

    [HideInInspector] public bool goLeft = false;
    
    public Transform[] startingPath;

    private Transform[] activePath;
    private int waypointIndex = 0;

    void Start()
    {
        SetPath(startingPath);
    }

    void Update()
    {
        if (activePath == null || activePath.Length == 0) return;

        MoveTrain();
    }

    void MoveTrain()
    {
        Transform target = activePath[waypointIndex];

        // Rotate toward target
        transform.LookAt(target);

        // Move toward waypoint
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // Check if reached waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            waypointIndex++;

            // Stop at end of segment (wait for next junction)
            if (waypointIndex >= activePath.Length)
            {
                waypointIndex = activePath.Length - 1;
            }
        }
    }

    public void SetPath(Transform[] newPath)
    {
        activePath = newPath;
        waypointIndex = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Junction"))
        {
            Junction junction = other.GetComponent<Junction>();

            if (junction != null)
            {
                Transform[] nextPath = goLeft 
                    ? junction.leftPath 
                    : junction.rightPath;

                SetPath(nextPath);
            }
        }
    }
}