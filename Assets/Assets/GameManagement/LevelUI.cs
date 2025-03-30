using UnityEngine;
using TMPro;
using System.Collections;

public class LevelUI : MonoBehaviour
{
    // instantiate the tmp text object for the level
    public TMP_Text levelText;
    // how long the level text will be displayed
    public float displayTime = 3f;
    public float fadeDuration = 1f;
    private CanvasGroup canvasGroup;
    private bool isFading = false;
    void Awake()
    {
        if (levelText == null)
        {
            return;
        }
        // Get the CanvasGroup component on the levelText object
        canvasGroup = levelText.GetComponent<CanvasGroup>();
    }
    void Start()
    {
        // get the level number from the spawnbugs script
        displaylevel(spawnbugs.current_level_static);
    }
    public void displaylevel(int level)
    {
        if (!isFading)
        {
            // turning on the text so it shows
            TurnOnTextCanvas();

            levelText.text = $"Level {level}";
            StartCoroutine(Fadepattern());
        }
    }
    private void TurnOnTextCanvas()
    {
        // Ensure the parent Canvas and levelText are active before starting the fade
        if (!levelText.gameObject.activeSelf)
        {
            levelText.gameObject.SetActive(true); // Activate the levelText
        }
        if (!levelText.transform.parent.gameObject.activeSelf)
        {
            levelText.transform.parent.gameObject.SetActive(true); // Activate the Canvas (if inactive)
        }
    }
    private IEnumerator Fadepattern()
    {
        if (isFading) yield break;
        isFading = true;
        // fade in
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));
        yield return new WaitForSeconds(displayTime);
        // fade out
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));
        // Disable the object after fading out
        levelText.gameObject.SetActive(false);
        isFading = false;
    }
    // choosing from what to what state to fade
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
