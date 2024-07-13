using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : BaseGrabable
{

    public AudioSource InsertingCoinSound;
    public Transform spawnPoint;
    public GameObject canPrefab;
    public GameObject sign;

    private bool releasingCan = false;
    private float signCooldownTime = 3.0f;


    public override void DisplayInteractionText()
    {
    }

    public override void PerformAction()
    {
        Debug.Log("SOY UNA MAQUINA DE COCA COLA");
        if (!releasingCan)
        {

            if (InteractionManager.Instance.GetCurrentGrabable() as Coin)
            {
                Debug.Log("SOY una persona con un euro");

                InteractionManager.Instance.GetCurrentGrabable().DestroyObject();

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
        releasingCan = true;
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Suelto lata");
        Instantiate(canPrefab, spawnPoint.position, spawnPoint.rotation);
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
