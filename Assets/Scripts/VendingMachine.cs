using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : BaseInteractable
{

    public AudioSource InsertingCoinSound;
    public Transform spawnPoint;
    public GameObject canPrefab;
    public GameObject sign;


    private bool releasingCan = false;
    private float signCooldownTime = 3.0f;


    public override void DisplayInteractionText()
    {
        interactionText.text = "E | Interact Vending Machine";
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

                
                    StartCoroutine(ReleaseCan());
            }
            else
            {
                StartCoroutine(ActivateSign());
            }


        }
    }

    private IEnumerator ReleaseCan()
    {
        InteractionManager.Instance.shouldInteract = false;
        interactionText.text = string.Empty;
       releasingCan = true;
        yield return new WaitForSeconds(5.0f);
        InteractionManager.Instance.shouldInteract = true;
        GameObject can = Instantiate(canPrefab, spawnPoint.position, spawnPoint.rotation);
        BaseGrabable g_can = can.GetComponent<BaseGrabable>();
        g_can.interactionText = InteractionManager.Instance.interactionText;
        releasingCan = false;


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
