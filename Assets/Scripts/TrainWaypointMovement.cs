using UnityEngine;

public class TrainWaypointMovement : MonoBehaviour
{
    public Transform[] mainPath;
    public Transform[] straightPath;
    public Transform[] leftPath;

    public float speed = 5f;

    bool goLeft = false;

    Transform[] activePath;
    int waypointIndex = 0;

    void Start()
    {
        activePath = mainPath;
        transform.position = activePath[0].position;
    }

    void Update()
    {
        // PLAYER INPUT
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            goLeft = true;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            goLeft = false;

        MoveTrain();
    }

    void MoveTrain()
    {
        Transform target = activePath[waypointIndex];

        // rotate toward waypoint
        transform.LookAt(target);

        // move forward
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            waypointIndex++;

            if (waypointIndex >= activePath.Length)
            {
                if (activePath == mainPath)
                {
                    activePath = goLeft ? leftPath : straightPath;
                    waypointIndex = 0;
                }
                else
                {
                    enabled = false;
                }
            }
        }
    }
}