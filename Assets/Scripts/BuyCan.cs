using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public AudioSource InsertingCoinSound;
    public Transform spawnPoint;
    public GameObject canPrefab;
    public GameObject sign;
    public Collider triggerVendingMachine;
    public Collider triggerServingZone;

    private bool releasingCan = false;
    private bool isCanServed = false;

    private List<GameObject> objectsInTrigger = new List<GameObject>();
    private float signCooldownTime = 3.0f;

    private void OnEnable()
    {
        ServingZoneTrigger.OnCanEntered += HandleCanEntered;
    }



    private void OnTriggerStay (Collider other)
    {



        if (other.gameObject.name == "Can")
        {
            Debug.Log("SOY una lata y estoy servida");
            isCanServed = true;
        }

        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E) ) {
                if (!releasingCan && !isCanServed)
                {
                    if (GrabSystem.Instance.GetCurrentObjectType() == GrabableObject.ObjectType.Coin  )
                {
                    Debug.Log("SOY una persona con un euro");
                    Debug.Log("La lata esta servida: " + isCanServed);

                    if (!releasingCan && !isCanServed)
                    {
                        if (!InsertingCoinSound.isPlaying)
                            InsertingCoinSound.Play();

                        GrabSystem.Instance.DeleteCurrentObject();
                        StartCoroutine(ReleaseCan());
                    }
                    
                }
                else
                {
                    StartCoroutine(ActivateSign());
                }
                    }
            }
            
               

        }
       
       

    }
    private void HandleCanEntered(Collider other)
    {
        isCanServed = true;
    }
    private IEnumerator ReleaseCan()
    {
        releasingCan = true;
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Suelto lata");
        Instantiate(canPrefab, spawnPoint.position, spawnPoint.rotation);

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
