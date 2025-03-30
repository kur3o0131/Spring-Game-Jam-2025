using UnityEngine;
using TMPro;
using System.Collections;

public class LevelUI : MonoBehaviour
{
    public TMP_Text levelText;
    public float displayTime = 3f;
    public float fadeDuration = 1f;

    private CanvasGroup canvasGroup;
    private bool isFading = false;

    void Awake()
    {
        // Check if levelText is assigned in the inspector
        if (levelText == null)
        {
            Debug.LogError("levelText is not assigned in the inspector!");
            return; // Exit if it's not assigned to avoid further errors
        }

        // Get CanvasGroup from the text object
        canvasGroup = levelText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            // If there's no CanvasGroup attached, log an error and add it automatically
            Debug.LogError("Missing CanvasGroup on levelText! Adding one automatically.");
            canvasGroup = levelText.gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void ShowLevel(int level)
    {
        if (!isFading) // Prevent multiple fade operations
        {
            // Ensure levelText and its parent Canvas are activated at the start
            ActivateLevelTextAndCanvas();

            levelText.text = $"Level {level}";
            StartCoroutine(FadeRoutine());
        }
    }

    private void ActivateLevelTextAndCanvas()
    {
        // Log the status of levelText and its parent Canvas
        Debug.Log($"Before Activation: levelText Active = {levelText.gameObject.activeSelf}, Canvas Active = {levelText.transform.parent.gameObject.activeSelf}");

        // Ensure the parent Canvas and levelText are active before starting the fade
        if (!levelText.gameObject.activeSelf)
        {
            levelText.gameObject.SetActive(true); // Activate the levelText
            Debug.Log("Activating levelText");
        }

        if (!levelText.transform.parent.gameObject.activeSelf)
        {
            levelText.transform.parent.gameObject.SetActive(true); // Activate the Canvas (if inactive)
            Debug.Log("Activating Canvas");
        }

        // Verify the activation after change
        Debug.Log($"After Activation: levelText Active = {levelText.gameObject.activeSelf}, Canvas Active = {levelText.transform.parent.gameObject.activeSelf}");
    }

    private IEnumerator FadeRoutine()
    {
        if (isFading) yield break; // If fading is already in progress, exit the routine
        isFading = true;

        // fade in
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // wait
        yield return new WaitForSeconds(displayTime);

        // fade out
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

        // Disable the object after fading out
        levelText.gameObject.SetActive(false);
        isFading = false;
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            float t = time / duration;
            canvasGroup.alpha = Mathf.Lerp(from, to, t);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = to;
    }
}
