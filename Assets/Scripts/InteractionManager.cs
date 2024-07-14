using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum InteractionPriority
{
    Low, // implicitamente 0
    Medium, // implicitamente 1
    High // implicitamente 2
}

// Interaction Manager coge objetos o interactua con ellos
public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance;
    public Transform Hand;
    private BaseGrabable currentGrabable;
    public bool shouldInteract = true;
    public TMP_Text interactionText;

    public bool objectHasBeenThrow;

   

    private void Awake()
    {
        objectHasBeenThrow = false;
        currentGrabable = null;
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

        if (shouldInteract)
        {
            //Debug.Log("PULSO LA K");
            if (currentGrabable as Can) //Tengo una lata en las manos
            {
                DisplayInteractionText();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Can can = currentGrabable as Can;
                    if (can.isEmpty)
                    {
                        ThrowGrabbable();
                    }
                    else
                    {
                        can.Drink();
                    }
                }

                


            }
            else // no tengo un objeto o tengo objetos que interactuan con otros ( moneda, llave ingl.) con lo que mando a detectar colisiones a ver si la maquina espendedora esta cerca
            {
                DetectAndInteractWithObjects();

            }
        }


    }

    public void SetCurrentGrabbable(BaseGrabable grabable)
    {
        currentGrabable = grabable;
    }

    
    public void DestroyCurrentGrabable()
    {
        currentGrabable.DestroyObject();
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
            objectHasBeenThrow = true;
            currentGrabable.Throw(5f);
            currentGrabable = null;
        }
    }
    public void DetectAndInteractWithObjects()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3.0f);
        BaseInteractable highestPriorityInteractable = null;
        InteractionPriority highestPriority = InteractionPriority.Low;

        foreach (var hitCollider in hitColliders)
        {
            BaseInteractable interactable = hitCollider.GetComponent<BaseInteractable>();
            if (interactable != null)
            {
                // Debug log para ver la prioridad del interactuable actual


                // Comparar la prioridad del interactuable actual con la prioridad más alta encontrada hasta ahora
                if (interactable.GetPriority() >= highestPriority)
                {
                    // Debug log para ver cuando se encuentra una nueva prioridad más alta

                    // Actualizar la prioridad más alta y el objeto interactuable de mayor prioridad
                    highestPriority = interactable.GetPriority();
                    highestPriorityInteractable = interactable;
                }
            }
        }
        if(highestPriorityInteractable as Wrench) {
            Debug.Log("TENEMOS UNA LLAVE INGLESA");
        }

        // Si se ha encontrado un interactuable con la prioridad más alta, interactuar con él
        if (highestPriorityInteractable != null)
        {

            highestPriorityInteractable.DisplayInteractionText();
            if (Input.GetKeyDown(KeyCode.E))
                highestPriorityInteractable.PerformAction();
        }
        else
        {
            interactionText.text = string.Empty;
        }
    }
}