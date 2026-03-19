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
        if (train != null)
        {
            // Move player along with train
            controller.Move(train.velocity * Time.deltaTime);
        }
    }
}