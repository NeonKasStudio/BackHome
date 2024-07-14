using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    public TMP_Text interactionText;
    public abstract void DisplayInteractionText();
   

    public abstract void PerformAction();

    public virtual void OnTriggerEnter(Collider other)
    {
    }
    public virtual InteractionPriority GetPriority()
    {
        return InteractionPriority.Low;
    }
}