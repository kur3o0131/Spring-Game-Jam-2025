using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public ScreenFader screenFader;     // Reference to the ScreenFader script
    public AudioSource menuMusic;       // Drag your AudioSource (with music) here

    void Start()
    {
        if (menuMusic != null)
        {
            menuMusic.volume = 0.1f;
        }
    }

    public void StartGame()
    {
        if (menuMusic != null)
        {
            menuMusic.volume = 0.03f; // Set initial volume
            StartCoroutine(FadeOutMusic(menuMusic, 3f, 0f)); // Fade to 0 over 1 second
        }

        // Start the fade out effect before switching scenes
        StartCoroutine(FadeOutAndLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeOutAndLoad()
    {
        yield return StartCoroutine(screenFader.FadeOut());
        SceneManager.LoadScene("level1"); // Replace with your actual game scene name
    }

    private IEnumerator FadeOutMusic(AudioSource source, float duration, float targetVolume = 0f)
    {
        float startVolume = source.volume;

        while (source.volume > targetVolume)
        {
            source.volume -= (startVolume - targetVolume) * Time.deltaTime / duration;
            yield return null;
        }

        source.volume = targetVolume;

        if (targetVolume == 0f)
            source.Stop();
    }
}
