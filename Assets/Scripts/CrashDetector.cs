using UnityEngine;
using System.Collections;

public class CrashDetector : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public TrainWaypointMovement train;

    public AudioSource crashSound;
    public AudioSource musicSource;

    public float fadeDuration = 2f; // how long the fade takes

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            train.enabled = false;

            crashSound.Play();

            StartCoroutine(FadeOutMusic());

            gameOverCanvas.SetActive(true);
        }
    }

    IEnumerator FadeOutMusic()
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume; // reset for next play
    }
}