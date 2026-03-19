using UnityEngine;
using TMPro;

public class JunctionInput : MonoBehaviour
{
    public TrainWaypointMovement train;   // your train movement script
    public TrainLever lever;              // assign your lever object here
    public TextMeshProUGUI messageText;   // your on-screen UI text

    bool trainInZone = false;
    bool decisionLocked = false;

    void Start()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);

        if (lever != null)
            lever.currentDirection = TrainLever.LeverDirection.Neutral; // start neutral
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            trainInZone = true;
            decisionLocked = false;

            if (messageText != null)
            {
                messageText.text = "VITE!\nUtilisez le levier pour sélectionner le BON rail";
                messageText.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            trainInZone = false;
            decisionLocked = true;

            if (messageText != null)
            {
                messageText.text = "Choix verrouillé";
                Invoke("HideMessage", 2f);
            }
        }
    }

    void Update()
    {
        if (!trainInZone || decisionLocked || lever == null) return;

        // Only apply if lever is Left or Right (ignore Neutral)
        if (lever.currentDirection == TrainLever.LeverDirection.Left)
        {
            train.goLeft = true;
            if (messageText != null)
                messageText.text = "Jonction GAUCHE sélectionnée";
        }
        else if (lever.currentDirection == TrainLever.LeverDirection.Right)
        {
            train.goLeft = false;
            if (messageText != null)
                messageText.text = "Train continue tout droit";
        }
        // If lever is Neutral, do nothing → keeps last valid selection
    }

    void HideMessage()
    {
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }
}