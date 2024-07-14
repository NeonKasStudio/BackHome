using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip dramaticHit;
    public AudioClip horrorHit;
    public AudioClip TenseSound;
    public AudioClip LightsOff;
    public AudioClip latamanAppearence;
    public AudioClip loudScream;

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
    public void PlayLightsOff()
    {
        audioSource.PlayOneShot(LightsOff);
    }

    public void LatamanAppearence()
    {
        audioSource.PlayOneShot(latamanAppearence);
    }

    public void LoudScream()
    {
        audioSource.PlayOneShot(loudScream);
    }

    public void StopLoudScream()
    {
        audioSource.Stop();
    }
}
