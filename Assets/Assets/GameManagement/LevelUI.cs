using UnityEngine;
using TMPro;
using System.Collections;

public class LevelUI : MonoBehaviour
{
    public TMP_Text levelText;
    public float displayTime = 3f;
    public float fadeDuration = 1f;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        // get CanvasGroup from the text object
        canvasGroup = levelText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("Missing CanvasGroup on levelText!");
        }
    }

    public void ShowLevel(int level)
    {
        levelText.text = $"Level {level}";
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        levelText.gameObject.SetActive(true);

        // fade in
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // wait
        yield return new WaitForSeconds(displayTime);

        // fade out
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

        levelText.gameObject.SetActive(false);
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
