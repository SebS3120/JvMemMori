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
        float dt = Time.deltaTime;
        Vector3 current = transform.position;

        // Avoid dividing by zero or propagating invalid positions/velocities
        if (dt <= Mathf.Epsilon || !IsFiniteVector(current) || !IsFiniteVector(lastPosition))
        {
            velocity = Vector3.zero;
            if (IsFiniteVector(current))
                lastPosition = current;
            return;
        }

        Vector3 computed = (current - lastPosition) / dt;

        if (!IsFiniteVector(computed))
        {
            velocity = Vector3.zero;
            lastPosition = current;
            return;
        }

        velocity = computed;
        lastPosition = current;
    }

    static bool IsFiniteVector(Vector3 v)
    {
        return !(float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z)
                 || float.IsInfinity(v.x) || float.IsInfinity(v.y) || float.IsInfinity(v.z));
    }
}