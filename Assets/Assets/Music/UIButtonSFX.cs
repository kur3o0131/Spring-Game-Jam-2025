using UnityEngine;
using UnityEngine.UI;

public class UIButtonSFX : MonoBehaviour
{
    // the sound to play when the button is clicked 
    public AudioClip clickSound;
    public AudioSource sfxsource;

    void Start()
    {
        // getting the button and adding the listener to it
        GetComponent<Button>().onClick.AddListener(PlaySound);
    }
    void PlaySound()
    {
        // if the sound is not null and the source is not null play the sound
        if (clickSound != null && sfxsource != null)
        {
            // initial volume
            float volume = 2f;
            sfxsource.PlayOneShot(clickSound, volume);
        }
    }
}
