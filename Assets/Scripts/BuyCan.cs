using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public AudioSource InsertingCoinSound;
    public Transform spawnPoint;
    public GameObject canPrefab;
    public GameObject sign;
 

    private bool releasingCan = false;
    private bool isCanServed = false;

    private List<GameObject> objectsInTrigger = new List<GameObject>();
    private float signCooldownTime = 3.0f;

    private void OnEnable()
    {
        ServingZoneTrigger.OnCanEntered += HandleCanEntered;
        ServingZoneTrigger.OnCanExited += HandleCanExited;
    }

    private void OnDisable()
    {
        ServingZoneTrigger.OnCanEntered -= HandleCanEntered;
        ServingZoneTrigger.OnCanExited -= HandleCanExited;


    }
  

    private void OnTriggerStay (Collider other)
    {

        //Debug.Log("ENTRO AL TRIGGER");
        

        if (other.CompareTag("Player"))
        {
            //Debug.Log("SOY UN PLAYER");

            if (Input.GetKey(KeyCode.E) ) {
                Debug.Log("PULSO E");

                // si no hay una lata ya pedida
                if (!releasingCan)
                {

                    //if (GrabSystem.Instance.GetCurrentObjectType() == GrabableObject.ObjectType.Coin  )
                    //{
                    Debug.Log("SOY una persona con un euro");

                    
                        if (!InsertingCoinSound.isPlaying)
                            InsertingCoinSound.Play();

                        GrabSystem.Instance.DeleteCurrentObject();
                        StartCoroutine(ReleaseCan());
                    //}
                    //else if(GrabSystem.Instance.GetCurrentObjectType() == GrabableObject.ObjectType.Nothing)
                    //{
                    //    StartCoroutine(ActivateSign());
                    //}


                }
                
            }
            
               

        }
        else
        {
            //Debug.Log("NO SOY UN PLAYER");
        }
       
       

    }
    private void HandleCanEntered(Collider other)
    {
        isCanServed = true;
    }
    private void HandleCanExited(Collider other)
    {
        isCanServed = false;
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
