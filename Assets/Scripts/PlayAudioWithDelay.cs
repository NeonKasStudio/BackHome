using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioWithDelay : MonoBehaviour
{
    AudioSource audioSource;
    public float delayTimeFromStart = 10f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(PlaySoundCoroutine());
    }

    IEnumerator PlaySoundCoroutine()
    {
        yield return new WaitForSeconds(delayTimeFromStart);
        audioSource.Play();
    }
}
