using UnityEngine;

public class TrainMotion : MonoBehaviour
{
    public Vector3 velocity { get; private set; }
    public Quaternion deltaRotation { get; private set; }

    Vector3 lastPosition;
    Quaternion lastRotation;

    void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    void LateUpdate()
    {
        float dt = Time.deltaTime;
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        // Safety checks
        if (dt <= Mathf.Epsilon || !IsFiniteVector(currentPosition) || !IsFiniteVector(lastPosition))
        {
            velocity = Vector3.zero;
            deltaRotation = Quaternion.identity;

            if (IsFiniteVector(currentPosition))
                lastPosition = currentPosition;

            lastRotation = currentRotation;
            return;
        }

        // Compute velocity
        Vector3 computedVelocity = (currentPosition - lastPosition) / dt;

        if (!IsFiniteVector(computedVelocity))
        {
            velocity = Vector3.zero;
        }
        else
        {
            velocity = computedVelocity;
        }

        // Compute rotation delta
        deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);

        // Store last frame values
        lastPosition = currentPosition;
        lastRotation = currentRotation;
    }

    static bool IsFiniteVector(Vector3 v)
    {
        return !(float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z)
              || float.IsInfinity(v.x) || float.IsInfinity(v.y) || float.IsInfinity(v.z));
    }
}