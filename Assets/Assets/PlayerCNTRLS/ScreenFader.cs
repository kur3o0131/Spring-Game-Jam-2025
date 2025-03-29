using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup fadeGroup;  // For the background fade (FadePanel)
    public CanvasGroup uiGroup;    // For the UI elements fade (buttons, text)
    public float fadeDuration = 2f;

    void Start()
    {
        if (fadeGroup == null || uiGroup == null)
        {
            Debug.LogError("CanvasGroup references not assigned.");
            return;
        }

        // Set initial alpha for FadePanel to 1 (fully black)
        fadeGroup.alpha = 1f;

        // Set initial alpha for UI elements to 1 (fully visible)
        uiGroup.alpha = 1f;

        StartCoroutine(FadeIn()); // Start fading the background in
    }

    public IEnumerator FadeIn()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = 1f - (t / fadeDuration);  // Fade background to transparent
            yield return null;
        }

        fadeGroup.alpha = 0f;  // Ensure the background is fully transparent after fading in
    }

    public IEnumerator FadeOut()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeGroup.alpha = t / fadeDuration;  // Fade background to black
            uiGroup.alpha = 1f - (t / fadeDuration);  // Fade out UI elements (buttons and text)
            yield return null;
        }

        fadeGroup.alpha = 1f;  // Ensure background is fully black after fading out
        uiGroup.alpha = 0f;    // Ensure UI is fully transparent after fade-out
    }
}
