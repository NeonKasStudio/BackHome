using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseGrabable : BaseInteractable
{
    public AudioSource fallingSound;

    protected bool isGrabbed = false;

    public override void DisplayInteractionText()
    {

    }

    public void DestroyObject () {
        Destroy(this.gameObject);
    }

    


    public void PickUp(Transform playerHand)
    {
        Debug.Log("Object picked up.");
        // Mover el objeto a la mano del jugador
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<GrabableObject>().enabled = false; ;
        //InteractionManager.Instance.PickUpGrabbable(this);
    }

    public void Throw()
    {
        Debug.Log("Object thrown.");
        // Desvincular el objeto de la mano del jugador
        transform.SetParent(null);

        // Habilitar el collider y deshabilitar el modo cinemático del Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        GetComponent<Collider>().enabled = true;

        // Aplicar una fuerza al objeto para lanzarlo
        rb.AddForce(transform.forward * 5f, ForceMode.Impulse);



        
    }

    public override InteractionPriority GetPriority()
    {
        return InteractionPriority.High;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GrabSystem.Instance.hasBeenThrown)
        {
            PlayFallingSound();
            GrabSystem.Instance.hasBeenThrown = false;
        }
    }

    public void PlayFallingSound()
    {
        if (!fallingSound.isPlaying)
        {
            fallingSound.Play();

        }
    }
   


}