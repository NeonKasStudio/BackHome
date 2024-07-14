using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : BaseGrabable
{
    public bool enablesLataManTrigger = false;
    public GameObject latamanTrigger;

    public override void DisplayInteractionText()
    {
        interactionText.text = "E | Grab Coin";
    }

    // Update is called once per frame
    public override void PerformAction()
    {
        if(InteractionManager.Instance.GetCurrentGrabable()  == null)
        {
            InteractionManager.Instance.PickUpGrabbable(this);
            if (enablesLataManTrigger)
                latamanTrigger.SetActive(true);
        }

    }
}
