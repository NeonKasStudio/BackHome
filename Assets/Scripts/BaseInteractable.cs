using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    public abstract void DisplayInteractionText();
   

    public abstract void PerformAction();

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

        }
    }
    public virtual InteractionPriority GetPriority()
    {
        return InteractionPriority.Low;
    }
}