using UnityEngine;

public class TrainLever : MonoBehaviour
{
    public enum LeverDirection { Left, Right, Neutral }
    public LeverDirection currentDirection = LeverDirection.Neutral;

    public float rotationAngle = 30f;
    public float rotationSpeed = 5f;

    private Quaternion neutralRotation;
    private Quaternion targetRotation;

    void Start()
    {
        neutralRotation = transform.localRotation;
        targetRotation = neutralRotation;
    }

    void Update()
    {
        HandleInput();
        UpdateTargetRotation();

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CycleDirection();
        }
    }

    void CycleDirection()
    {
        if (currentDirection == LeverDirection.Neutral)
        {
            currentDirection = LeverDirection.Left;
        }
        else if (currentDirection == LeverDirection.Left)
        {
            currentDirection = LeverDirection.Right;
        }
        else
        {
            currentDirection = LeverDirection.Left;
        }
    }

    void UpdateTargetRotation()
    {
        if (currentDirection == LeverDirection.Left)
        {
            targetRotation = neutralRotation * Quaternion.Euler(-rotationAngle, 0, 0);
        }
        else if (currentDirection == LeverDirection.Right)
        {
            targetRotation = neutralRotation * Quaternion.Euler(rotationAngle, 0, 0);
        }
        else
        {
            targetRotation = neutralRotation;
        }
    }
}