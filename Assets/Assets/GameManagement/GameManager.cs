using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = gameOverScreen.GetComponent<CanvasGroup>();
    }
    public void GameOver()
    {
        Debug.Log("GAME OVER");
        Time.timeScale = 0f; 

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
            StartCoroutine(FadeIn());
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        spawnbugs.current_level_static = 1;
        SceneManager.LoadScene("MainMenu"); 
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // routine for fade effect when game over
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float fadeDuration = 2f;
        canvasGroup.alpha = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
}
