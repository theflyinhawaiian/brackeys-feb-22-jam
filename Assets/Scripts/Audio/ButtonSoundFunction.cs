using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundFunction : MonoBehaviour
{
    public AudioSource buttonSounds;
    public AudioClip hoverSound;
    public AudioClip clickSound;


    public void Hover()
    {
        buttonSounds.PlayOneShot(hoverSound);
    }
    public void Click()
    {
        buttonSounds.PlayOneShot(clickSound);
    }
}
