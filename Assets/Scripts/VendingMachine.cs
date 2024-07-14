using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : BaseInteractable
{

    public AudioSource InsertingCoinSound;
    public AudioSource KickSound;
    public Transform spawnPoint;
    public GameObject canPrefab;
    public GameObject sign;


    private bool releasingCan = false;
    private float signCooldownTime = 3.0f;


    public override void DisplayInteractionText()
    {
        interactionText.text = "E | Interact";
    }

    public override void PerformAction()
    {
        if (!releasingCan)
        {

            if (InteractionManager.Instance.GetCurrentGrabable() as Coin)
            {

                InteractionManager.Instance.DestroyCurrentGrabable();

                if (!InsertingCoinSound.isPlaying)
                    InsertingCoinSound.Play();

                
                    StartCoroutine(ReleaseCan(5f));
            }
            else if(InteractionManager.Instance.GetCurrentGrabable() as Wrench)
            {
                if (!KickSound.isPlaying)
                    KickSound.Play();


                StartCoroutine(ReleaseAndThrow(1.5f));
            }
            else
            {
                StartCoroutine(ActivateSign());
            }


        }
    }

    private IEnumerator ReleaseCan(float secondsWaiting)
    {
        InteractionManager.Instance.shouldInteract = false;
        interactionText.text = string.Empty;
        releasingCan = true;

        yield return new WaitForSeconds(secondsWaiting);

        InteractionManager.Instance.shouldInteract = true;
        GameObject can = Instantiate(canPrefab, spawnPoint.position, spawnPoint.rotation);
        BaseGrabable g_can = can.GetComponent<BaseGrabable>();
        g_can.interactionText = InteractionManager.Instance.interactionText;
        releasingCan = false;


    }
    private IEnumerator ReleaseAndThrow(float secondsWaiting)
    {
        InteractionManager.Instance.shouldInteract = false;
        interactionText.text = string.Empty;
        releasingCan = true;
    
        yield return new WaitForSeconds(secondsWaiting);

        GameObject can = Instantiate(canPrefab, spawnPoint.position, spawnPoint.rotation);
        BaseGrabable g_can = can.GetComponent<BaseGrabable>();
        g_can.Throw(5f);
        g_can.interactionText = InteractionManager.Instance.interactionText;
        StartCoroutine(TimeDesactivatingInteractions(1f));
       
        releasingCan = false;


    }
    private IEnumerator TimeDesactivatingInteractions(float secondsWaiting)
    {
        yield return new WaitForSeconds(secondsWaiting);
        InteractionManager.Instance.shouldInteract = true;

    }

    private IEnumerator ActivateSign()
    {
        // Activar el objeto
        sign.SetActive(true);

        // Esperar por el tiempo de cooldown
        yield return new WaitForSeconds(signCooldownTime);

        // Desactivar el objeto
        sign.SetActive(false);
    }
}
