using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public TrainWaypointMovement train;
    public AudioSource crashSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            train.enabled = false;

            crashSound.Play();

            gameOverCanvas.SetActive(true);
        }
    }
}