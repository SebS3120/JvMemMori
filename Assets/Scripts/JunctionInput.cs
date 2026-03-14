using UnityEngine;

public class JunctionInput : MonoBehaviour
{
    public TrainWaypointMovement train;

    bool playerInZone = false;
    string message = "";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            playerInZone = true;
            message = "JUNCTION AHEAD: ← LEFT to turn | → RIGHT to stay straight";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            playerInZone = false;
            message = "";
        }
    }

    void Update()
    {
        if (!playerInZone) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            train.goLeft = true;
            message = "TRACK SET TO LEFT";
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            train.goLeft = false;
            message = "TRACK SET TO STRAIGHT";
        }
    }

    void OnGUI()
    {
        if (playerInZone)
        {
            GUI.Label(
                new Rect(Screen.width / 10, 10, 500, 50),
                message
            );
        }
    }
}