using UnityEngine;
using UnityEngine.UI;

public class UIButtonSFX : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioSource sfxsource;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        if (clickSound != null && sfxsource != null)
        {
            float volume = 2f;
            sfxsource.PlayOneShot(clickSound, volume);
        }
    }
}
