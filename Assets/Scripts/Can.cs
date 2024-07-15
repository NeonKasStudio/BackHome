using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : BaseGrabable
{
    public bool isEmpty = false;
    public AudioSource drinkingCanSource;
    public AudioSource fallingSound;
    bool isEnabled = true;

    // Start is called before the first frame update
   
    public override void DisplayInteractionText() {

        if (isEnabled)
        {
            if (InteractionManager.Instance.GetCurrentGrabable() == null)
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
        if (!isEnabled)
            return;

        if ( collision.gameObject.tag=="Ground")
        {
            isEnabled = false;
            PlayFallingSound();
            StartCoroutine(PlayDramaticHitCoroutine());
            

            // StartCoroutine(CoolDownDestroyingCan());

        }
    }



    public IEnumerator CoolDownDestroyingCan ()
    {

        yield return new WaitForSeconds(4f);
        InteractionManager.Instance.shouldInteract = true;
        InteractionManager.Instance.objectHasBeenThrow = false;

        Destroy(this);

    }
    public void PlayFallingSound()
    {
        if (!fallingSound.isPlaying)
        {
            fallingSound.Play();

        }
    }

    IEnumerator PlayParanormalEventCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("pre paranormal");
        FindObjectOfType<ParanormalEventManager>().PlayParanormalEvent();
        InteractionManager.Instance.shouldInteract = true;
        InteractionManager.Instance.objectHasBeenThrow = false;

        Destroy(this);

    }

    IEnumerator PlayDramaticHitCoroutine()
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioManager>().PlayDramaticHit();
        Debug.Log("dramatic hit");
        StartCoroutine(PlayParanormalEventCoroutine());

    }

    
    private IEnumerator DrinkingTime()
    {
        InteractionManager.Instance.interactionText.text = string.Empty;

        InteractionManager.Instance.shouldInteract = false;
        yield return new WaitForSeconds(6.0f);
        InteractionManager.Instance.shouldInteract = true;


    }
   

}
