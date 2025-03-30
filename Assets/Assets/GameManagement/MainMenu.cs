using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public ScreenFader screenFader;   
    public AudioSource menuMusic;  

    void Start()
    {
        // set the menu music volume low cuz its loud super loud by default
        if (menuMusic != null)
        {
            menuMusic.volume = 0.05f;
        }
    }

    public void StartGame()
    {
        // reset the level if this is the end scene
        if (SceneManager.GetActiveScene().name == "end")
        {
            spawnbugs.current_level_static = 1;
        }
        // fading out the menu music when start is clciekd
        if (menuMusic != null)
        {
            menuMusic.volume = 0.03f;
            StartCoroutine(FadeOutMusic(menuMusic, 3f, 0f));
        }
        StartCoroutine(FadeOutAndLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator FadeOutAndLoad()
    {
        // load in the next scene after fading out
        yield return StartCoroutine(screenFader.FadeOut());
        SceneManager.LoadScene("level1");
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
