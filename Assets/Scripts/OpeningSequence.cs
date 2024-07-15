using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSequence : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator fadeOut;
    public Animator hour;
    public PlayerMovement playerMovement;

    private void Start()
    {
        StartCoroutine(Hour());
        StartCoroutine(TrainHorn());
    }

    IEnumerator TrainHorn()
    {
        yield return new WaitForSeconds(2f);
        audioSource.Play();
        StartCoroutine(FadeOut());
    }

    IEnumerator Hour()
    {
        yield return new WaitForSeconds(0.5f);
        hour.enabled = true;
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(4.5f);
        fadeOut.enabled = true;
        FindObjectOfType<ParanormalEventManager>().EnableAllLights();
        playerMovement.enabled = true;
    }
}
