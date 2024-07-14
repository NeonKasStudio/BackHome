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

        if(InteractionManager.Instance.GetCurrentGrabable() == null)
        {
            interactionText.text = "E | Grab Can.";

        }
        else
        {
            if (isEmpty)
            {
                interactionText.text = "E | Throw.";
            }
            else
            {
                interactionText.text = "E | Drink.";

            }
        }
       
    }

    // Update is called once per frame
   
   
    public void Drink()
    {
        isEmpty = true;
        drinkingCanSource.Play();
        StartCoroutine(DrinkingTime());


    }
    private void OnCollisionEnter(Collision collision)
    {

        if (InteractionManager.Instance.objectHasBeenThrow)
        {
            PlayFallingSound();
            StartCoroutine(CoolDownDestroyingCan());
            InteractionManager.Instance.objectHasBeenThrow = false;
        }
    }

    public IEnumerator CoolDownDestroyingCan ()
    {
        yield return new WaitForSeconds(2.0f);
        Debug.Log("LA MATE");

        Destroy(this);

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
