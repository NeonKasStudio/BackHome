using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : BaseGrabable
{
    public bool isEmpty = false;
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
    public override void PerformAction()
    {
        if(isEmpty)
        {

        }
        Debug.Log("SOY UNA LATA Y HE LLEGADO MUY LEJOS");
        InteractionManager.Instance.PickUpGrabbable(this);
       
    }
    public void Drink()
    {
        Debug.Log("You drank the can.");
        isEmpty = true;
        InteractionManager.Instance.ClearCurrentGrabbable();
    }

    
}
