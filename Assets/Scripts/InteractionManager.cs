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
    public Vector3 hitColliderTest;
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
            BaseInteractable interactable;

            if(currentGrabable as Coin || currentGrabable as Wrench)
            {
                 interactable = hitCollider.GetComponent<VendingMachine>();

            }
            else
            {
                interactable = hitCollider.GetComponent<BaseInteractable>();

            }

         
                if (interactable != null)
                {

                    //Debug.Log("COLISIONO CON:" + hitCollider.gameObject.name);
                    Vector3 directionToTarget = hitCollider.transform.position - Camera.main.transform.position;
                    hitColliderTest = hitCollider.transform.position;
                    //Debug.Log("DIRECTION TO TARGET:" + directionToTarget);


                // Proyectar el vector de dirección en el plano horizontal
                directionToTarget.y = 0;
                    Vector3 forwardDirection = Camera.main.transform.forward;
                    forwardDirection.y = 0;


                if (directionToTarget.sqrMagnitude > 0.01f)
                    {
                        directionToTarget.Normalize();
                        forwardDirection.Normalize();
                        float angle = Vector3.Angle(forwardDirection, directionToTarget);
                    
                    if (angle <= 60 / 2)
                        {
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
                }
            else
            {
                //Debug.Log("No estoy en el angulo correcto");
            }
        }
        if(highestPriorityInteractable as Wrench) {
            //Debug.Log("TENEMOS UNA LLAVE INGLESA");
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
    void OnDrawGizmos()
    {
        // Configurar el color de los Gizmos
        Gizmos.color = Color.yellow;

        // Dibujar una esfera para mostrar el radio de interacción
        Gizmos.DrawWireSphere(Camera.main.transform.position, 3.0f);

        // Dibujar las líneas del campo de visión
        Vector3 leftBoundary = Quaternion.Euler(0, -60 / 2, 0) * Camera.main.transform.forward * 3.0f;
        Vector3 rightBoundary = Quaternion.Euler(0, 60 / 2, 0) * Camera.main.transform.forward * 3.0f;

        Gizmos.DrawLine(Camera.main.transform.position, hitColliderTest);

        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + leftBoundary);
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + rightBoundary);
    }
}