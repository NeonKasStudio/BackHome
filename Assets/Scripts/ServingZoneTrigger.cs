using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingZoneTrigger : MonoBehaviour
{
    public static event Action<Collider> OnCanEntered;
    private void OnTriggerStay(Collider other)
    {
        if( other.gameObject.name == "Can")
        {
            OnCanEntered?.Invoke(other);
        }
    }
}
