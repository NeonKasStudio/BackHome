using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip dramaticHit;
    public AudioClip horrorHit;
    public AudioClip TenseSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDramaticHit()
    {
        audioSource.PlayOneShot(dramaticHit);
    }

    public void PlayHorrorHit()
    {
        audioSource.PlayOneShot(horrorHit);
    }

    public void PlayTenseSound()
    {
        audioSource.PlayOneShot(TenseSound);
    }
}
