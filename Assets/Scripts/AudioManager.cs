using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip dramaticHit;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDramaticHit()
    {
        audioSource.PlayOneShot(dramaticHit);
    }
}
