using UnityEngine;
using TMPro;

public class JunctionInput : MonoBehaviour
{
    public TrainWaypointMovement train;
    public TextMeshProUGUI messageText;

    bool playerInZone = false;
    bool decisionLocked = false;

    void Start()
    {
        messageText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            playerInZone = true;
            decisionLocked = false;

            messageText.text = "VITE!\nUtilisez les flèches pour sélectionner\nla BONNE rail";
            messageText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            playerInZone = false;
            decisionLocked = true;

            messageText.text = "Choix vérouillé";
            Invoke("HideMessage", 2f);
        }
    }

    void Update()
    {
        if (!playerInZone || decisionLocked) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            train.goLeft = true;
            messageText.text = "Jonction GAUCHE sélectionnée";
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            train.goLeft = false;
            messageText.text = "Train continue tout droit";
        }
    }

    void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }
}
