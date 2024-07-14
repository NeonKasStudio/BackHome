using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wrench : BaseGrabable
{
    public GameObject latamanTrigger;

    public void Start()
    {
        Debug.Log("ENTRADMOS AQUI");
    }
    public override void DisplayInteractionText()
    {

       
            interactionText.text = "E | Grab Wrench.";

       

    }

    public override void PerformAction()
    {

        base.PerformAction();
        latamanTrigger.SetActive(true);
    }
}
