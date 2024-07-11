using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GrabableObject;

public class GrabSystem : MonoBehaviour
{
    public static GrabSystem Instance { get; private set; }
    public Transform playerHand;

    private GrabableObject currentGrabableObject;
    public bool hasBeenThrown = false;


   
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        currentGrabableObject = null;
    }
    public void Update()
    {
        //Quiza esto se pueda poner en otro sitio, añadir in listener , evento o como sea
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentGrabableObject != null)
            {
                DropCurrentObject();
            }

        }
    }

   
    public void PickUpObject(GrabableObject pickedUpObject)
    {
        if (currentGrabableObject == null )
        {
            currentGrabableObject = pickedUpObject;

            // Mover el objeto a la mano del jugador
            Transform playerHand = GameObject.FindWithTag("Player").transform.Find("Hand");
            pickedUpObject.transform.SetParent(playerHand);
            pickedUpObject.transform.localPosition = Vector3.zero;
            pickedUpObject.transform.localRotation = Quaternion.identity;

            pickedUpObject.GetComponent<Collider>().enabled = false;
            pickedUpObject.GetComponent<Rigidbody>().isKinematic = true;

        }
    }

    public void DropCurrentObject()
    {
        if (currentGrabableObject != null)
        {
            // Desvincular el objeto de la mano del jugador
            currentGrabableObject.transform.SetParent(null);

            // Habilitar el collider y deshabilitar el modo cinemático del Rigidbody
            Rigidbody rb = currentGrabableObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            currentGrabableObject.GetComponent<Collider>().enabled = true;

            // Aplicar una fuerza al objeto para lanzarlo
            rb.AddForce(transform.forward * 5f, ForceMode.Impulse);

            hasBeenThrown = true;

            //currentGrabableObject.PlayFallingSound();
            // Limpiar la referencia al objeto recogido
            currentGrabableObject = null;
        }
    }

    public void DeleteCurrentObject()
    {
        if (currentGrabableObject != null)
        {
            Destroy(currentGrabableObject.gameObject);
            currentGrabableObject = null;
        }
    }

    public GrabableObject GetCurrentGrabableObject()
    {

        return currentGrabableObject;

    }

    public ObjectType GetCurrentObjectType()
    {
        if (currentGrabableObject != null)
            return currentGrabableObject.objectType;
        else
            return ObjectType.Nothing;
    }
}
