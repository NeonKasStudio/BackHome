using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InteractionPriority
{
    Low,
    Medium,
    High
}


public interface IInteractable
{
    void DisplayInteractionText();
    void PerformAction();

    InteractionPriority GetPriority();
}

