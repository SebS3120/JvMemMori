using UnityEngine;

public class PlayerOnTrain : MonoBehaviour
{
    public TrainMotion train;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (train == null || controller == null)
            return;

        // --- Apply train translation ---
        Vector3 vel = train.velocity;
        if (IsFiniteVector(vel))
        {
            controller.Move(vel * Time.deltaTime);
        }

        // --- Apply train rotation ---
        Quaternion deltaRot = train.deltaRotation;

        if (IsFiniteQuaternion(deltaRot))
        {
            // Rotate player around train pivot
            Vector3 relativePos = transform.position - train.transform.position;
            relativePos = deltaRot * relativePos;

            Vector3 newWorldPos = train.transform.position + relativePos;

            // Temporarily disable controller to avoid conflicts
            controller.enabled = false;

            transform.position = newWorldPos;
            transform.rotation = deltaRot * transform.rotation;

            controller.enabled = true;
        }
    }

    static bool IsFiniteVector(Vector3 v)
    {
        return !(float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z)
              || float.IsInfinity(v.x) || float.IsInfinity(v.y) || float.IsInfinity(v.z));
    }

    static bool IsFiniteQuaternion(Quaternion q)
    {
        return !(float.IsNaN(q.x) || float.IsNaN(q.y) || float.IsNaN(q.z) || float.IsNaN(q.w)
              || float.IsInfinity(q.x) || float.IsInfinity(q.y)
              || float.IsInfinity(q.z) || float.IsInfinity(q.w));
    }
}