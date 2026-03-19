using UnityEngine;
using TMPro; // <-- important

public class TrainLever : MonoBehaviour
{
    public enum LeverDirection { Neutral, Left, Right }
    public LeverDirection currentDirection = LeverDirection.Neutral;

    public Transform leverHandle;
    public Vector3 neutralRotation = new Vector3(0, 0, 0);
    public Vector3 leftRotation = new Vector3(0, 0, -30);
    public Vector3 rightRotation = new Vector3(0, 0, 30);

    public TextMeshProUGUI interactPrompt; // <-- changed to TMP

    bool playerInRange = false;

    void Start()
    {
        UpdateVisual();
        if (interactPrompt != null)
            interactPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle between Left and Right
            if (currentDirection == LeverDirection.Neutral || currentDirection == LeverDirection.Right)
                currentDirection = LeverDirection.Left;
            else if (currentDirection == LeverDirection.Left)
                currentDirection = LeverDirection.Right;

            UpdateVisual();
            Debug.Log("Lever set to: " + currentDirection);
        }
    }

    void UpdateVisual()
    {
        if (leverHandle != null)
        {
            switch (currentDirection)
            {
                case LeverDirection.Neutral:
                    leverHandle.localEulerAngles = neutralRotation;
                    break;
                case LeverDirection.Left:
                    leverHandle.localEulerAngles = leftRotation;
                    break;
                case LeverDirection.Right:
                    leverHandle.localEulerAngles = rightRotation;
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactPrompt != null)
                interactPrompt.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactPrompt != null)
                interactPrompt.gameObject.SetActive(false);
        }
    }

    public void ResetToNeutral()
    {
        currentDirection = LeverDirection.Neutral;
        UpdateVisual();
    }
}