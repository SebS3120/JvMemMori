using UnityEngine;
using System.Collections;

public class CrashDetector : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public TrainWaypointMovement train;

    public AudioSource crashSound;
    public AudioSource musicSource;
    public AudioSource trainSource; // 🔊 ADD THIS

    public float fadeDuration = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            train.enabled = false;

            crashSound.Play();

            // 🔊 Fade BOTH audio sources
            StartCoroutine(FadeOutAudio(musicSource));
            StartCoroutine(FadeOutAudio(trainSource));

            gameOverCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    IEnumerator FadeOutAudio(AudioSource source)
    {
        if (source == null) yield break;

        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        source.Stop();
        source.volume = startVolume; // reset for reuse
    }
}