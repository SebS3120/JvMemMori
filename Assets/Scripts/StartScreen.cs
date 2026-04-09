using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour
{
    public CanvasGroup canvasGroup; // assign your panel's CanvasGroup
    public float fadeDuration = 1f;

    private bool hasStarted = false;

    void Start()
    {
        Time.timeScale = 0f; // pause game
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (!hasStarted && Input.anyKeyDown)
        {
            hasStarted = true;
            StartCoroutine(FadeOutAndStart());
        }
    }

    IEnumerator FadeIn()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOutAndStart()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = 1f - (t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;

        Time.timeScale = 1f; // resume game
        gameObject.SetActive(false);
    }
}