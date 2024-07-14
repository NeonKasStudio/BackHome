using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : BaseGrabable
{
    public override void DisplayInteractionText()
    {
        interactionText.text = "E | Grab Coin";
    }

    // Update is called once per frame
    public override void PerformAction()
    {
        InteractionManager.Instance.PickUpGrabbable(this);
    }
}
