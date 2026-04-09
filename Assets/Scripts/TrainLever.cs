using UnityEngine;

public class TrainLever : MonoBehaviour
{
    public enum LeverDirection { Left, Right, Neutral }
    public LeverDirection currentDirection = LeverDirection.Neutral;

    public float rotationAngle = 30f;
    public float rotationSpeed = 5f;

    // 🔑 Interaction
    public Transform player;           // assign your player
    public float interactDistance = 3f;
    public GameObject pressEUI;        // your "Press E" UI

    private Quaternion neutralRotation;
    private Quaternion targetRotation;

    void Start()
    {
        neutralRotation = transform.localRotation;
        targetRotation = neutralRotation;

        // hide UI at start
        if (pressEUI != null)
            pressEUI.SetActive(false);
    }

    void Update()
    {
        HandleInteraction();
        UpdateTargetRotation();

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    void HandleInteraction()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        bool playerInRange = distance <= interactDistance;

        // Show / hide UI
        if (pressEUI != null)
            pressEUI.SetActive(playerInRange);

        // Handle input ONLY if near
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
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