using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatamanSpawnEvent : MonoBehaviour
{
    public GameObject lataman;

    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<ParanormalEventManager>().PlayLatamanAppareance();
    }
}
