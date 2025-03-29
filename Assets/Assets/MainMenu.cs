using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public ScreenFader screenFader;  // Reference to the ScreenFader script

    public void StartGame()
    {
        // Start the fade out effect before switching scenes
        StartCoroutine(FadeOutAndLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeOutAndLoad()
    {
        // Start fading the screen and UI elements out
        yield return StartCoroutine(screenFader.FadeOut());

        // After fade-out, load the game scene
        SceneManager.LoadScene("level1");  // Replace with your actual game scene name
    }
}
