using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabable : IInteractable
{
    void PickUp(Transform playerHand);
    void Throw();
}