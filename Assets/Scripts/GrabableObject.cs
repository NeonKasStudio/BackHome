using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    public enum ObjectType
    {
        Can,
        Coin,
        Nothing
    }
    public AudioSource fallingSound;


    public ObjectType objectType;


   
    private void OnTriggerStay(Collider other)
    {
        // if it's the player and he doesn't have already an object
        if(other.CompareTag("Player") ){

            if (Input.GetKey(KeyCode.E))
            {
                GrabSystem.Instance.PickUpObject(this);
                
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(GrabSystem.Instance.hasBeenThrown)
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
