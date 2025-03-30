using UnityEngine;
using System.Collections;

public class LevelMusicManager : MonoBehaviour
{
    public AudioSource levelMusic;
    public float fadeDuration = 5f;
    public float targetVolume = .5f;

    void Start()
    {
        if (levelMusic != null)
        {
            levelMusic.volume = 0f;
            levelMusic.Play();
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            levelMusic.volume = Mathf.Lerp(0f, targetVolume, time / fadeDuration);
            yield return null;
        }

        levelMusic.volume = targetVolume;
    }
}
