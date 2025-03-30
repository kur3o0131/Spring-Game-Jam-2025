using UnityEngine;
using System.Collections;

public class LevelMusicManager : MonoBehaviour
{
    // music for the current level
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
    // this is to not instantly blast the user with music
    IEnumerator FadeIn()
    {
        float time = 0f;
        // slowly ramping up the volume
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            levelMusic.volume = Mathf.Lerp(0f, targetVolume, time / fadeDuration);
            yield return null;
        }
        levelMusic.volume = targetVolume;
    }
}
