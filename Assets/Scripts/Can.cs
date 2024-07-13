using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : BaseGrabable
{
    public bool isEmpty = false;
    public AudioSource drinkingCanSource;
    public AudioSource fallingSound;

    // Start is called before the first frame update
    public override void DisplayInteractionText() {
        if (isEmpty)
        {
            Debug.Log("Press 'E' to throw the can.");
        }
        else
        {
            Debug.Log("Press 'E' to drink the can.");
        }
    }

    // Update is called once per frame
   
   
    public void Drink()
    {
        Debug.Log("You drank the can.");
        isEmpty = true;
        drinkingCanSource.Play();
        StartCoroutine(DrinkingTime());


    }
    private void OnCollisionEnter(Collision collision)
    {

        if (InteractionManager.Instance.objectHasBeenThrow)
        {
            PlayFallingSound();
            Destroy(this);
            InteractionManager.Instance.objectHasBeenThrow = false;
        }
    }

    public void PlayFallingSound()
    {
        if (!fallingSound.isPlaying)
        {
            fallingSound.Play();

        }
    }

    private IEnumerator DrinkingTime()
    {
        InteractionManager.Instance.shouldInteract = false;
        yield return new WaitForSeconds(6.0f);
        InteractionManager.Instance.shouldInteract = true;


    }
   

}
