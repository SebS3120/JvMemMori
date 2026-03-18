using UnityEngine;
using System.Collections;

public class SuccessDetector : MonoBehaviour
{
    public GameObject successCanvas;
    public AudioSource successSound;

    public float displayTime = 2f;

    bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train") && !triggered)
        {
            triggered = true;

            // Show UI
            successCanvas.SetActive(true);

            // Play sound
            if (successSound != null)
                successSound.Play();

            // Hide after delay
            StartCoroutine(HideSuccess());
        }
    }

    IEnumerator HideSuccess()
    {
        yield return new WaitForSeconds(displayTime);
        successCanvas.SetActive(false);
    }
}