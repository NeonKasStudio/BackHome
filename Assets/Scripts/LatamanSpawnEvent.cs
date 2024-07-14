using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatamanSpawnEvent : MonoBehaviour
{
    public GameObject lataman;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<ParanormalEventManager>().PlayLatamanAppareance();
        }
    }
}
