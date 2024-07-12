using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingZoneTrigger : MonoBehaviour
{
    public static event Action<Collider> OnCanEntered;
    public static event Action<Collider> OnCanExited;

    public void Start()
    {
        Debug.Log("ESTO ESTA VIVO O QIE");
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("HaY ALGO");

        if ( other.gameObject.name == "Can")
        {
            Debug.Log("HAY UNA LATA SERVIDA");
            OnCanEntered?.Invoke(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Can")
        {
            Debug.Log("NO HAY LATA SERVIDA");

            OnCanExited?.Invoke(other);
        }
    }
    
}
