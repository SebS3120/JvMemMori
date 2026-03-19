using UnityEngine;

public class TrainMotion : MonoBehaviour
{
    public Vector3 velocity { get; private set; }

    Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void LateUpdate()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }
}