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
        if (train != null && controller != null)
        {
            Vector3 vel = train.velocity;
            if (IsFiniteVector(vel))
            {
                controller.Move(vel * Time.deltaTime);
            }
        }
    }

    static bool IsFiniteVector(Vector3 v)
    {
        return !(float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z)
                 || float.IsInfinity(v.x) || float.IsInfinity(v.y) || float.IsInfinity(v.z));
    }
}