using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup; 
    public CanvasGroup uiGroup;  
    public float fadeDuration = 2f;

    void Start()
    {
        // if user didnt assing a canvas group or a ui group
        if (fadeCanvasGroup == null || uiGroup == null)
        {
            return;
        }

        // Set initial alpha to black
        fadeCanvasGroup.alpha = 1f;

        // Set initial alpha to visible
        uiGroup.alpha = 1f;

        StartCoroutine(FadeIn());
    }
    public IEnumerator FadeIn()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - (t / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
    }
    public IEnumerator FadeOut()
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            // fade out background and buttons
            fadeCanvasGroup.alpha = t / fadeDuration; 
            uiGroup.alpha = 1f - (t / fadeDuration);  
            yield return null;
        }
        // ensure everything is faded out
        fadeCanvasGroup.alpha = 1f;
        uiGroup.alpha = 0f; 
    }
}
