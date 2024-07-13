using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Interactio Manager coge objetos o interactua con ellos
public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    public Transform Hand;
    private BaseGrabable currentGrabable = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        //DetectAndInteractWithObjects();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("PULSO LA K");
            if (!currentGrabable) // no tiene objeto en las manos
            {
                Debug.Log("No tengo ningun objeto en la smanos");

                DetectAndInteractWithObjects();

            }
            else // tiene ya un objeto en las manos
            {
                switch (currentGrabable)
                {

                    case Can can:
                        if (can.isEmpty)
                        {
                            can.Throw();
                        }
                        else
                        {
                            can.Drink();
                        }
                        break;
                    case Coin:
                        DetectAndInteractWithObjects();
                        break;




                }
            }
        }


    }

    public void SetCurrentGrabbable(BaseGrabable grabable)
    {
        currentGrabable = grabable;
    }

    public void ClearCurrentGrabbable()
    {
        currentGrabable = null;
    }

    public void Interact()
    {
        if (currentGrabable != null)
        {
            currentGrabable.PerformAction();
        }
    }
    public BaseGrabable GetCurrentGrabable()
    {
        return currentGrabable;
    }

    public void DisplayInteractionText()
    {
        if (currentGrabable != null)
        {
            currentGrabable.DisplayInteractionText();
        }
    }

    public void PickUpGrabbable(BaseGrabable grabable)
    {
        SetCurrentGrabbable(grabable);
        grabable.PickUp(Hand);
    }

    public void ThrowGrabbable()
    {
        if (currentGrabable != null)
        {
            currentGrabable.Throw();
            ClearCurrentGrabbable();
        }
    }
    public void DetectAndInteractWithObjects()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3.0f);
        BaseGrabable highestPriorityInteractable = null;
        InteractionPriority highestPriority = InteractionPriority.Low;

        //Debug.Log(hitColliders.Length);
        foreach (var hitCollider in hitColliders)
        {
            BaseGrabable interactable = hitCollider.GetComponent<BaseGrabable>();
            if (interactable != null)
            {
                if (interactable.GetPriority() > highestPriority)
                {
                    highestPriority = interactable.GetPriority();
                    highestPriorityInteractable = interactable;
                }
            }
        }

        if (highestPriorityInteractable != null)
        {
            highestPriorityInteractable.DisplayInteractionText();
           
            //if(Input.GetKey(KeyCode.E))
                highestPriorityInteractable.PerformAction();

            
        }
    }
}