using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    public float seconds = 3f;

    void Start()
    {
        StartCoroutine(DestroyInSecondsCoroutine());
    }

    IEnumerator DestroyInSecondsCoroutine()
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
