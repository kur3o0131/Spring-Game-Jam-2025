using UnityEngine;
using UnityEngine.UI;

public class UIButtonSFX : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioSource sfxSource;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        if (clickSound != null && sfxSource != null)
        {
            float volume = 2f; // set volume here
            sfxSource.PlayOneShot(clickSound, volume); // now it actually uses the volume
        }
    }
}
