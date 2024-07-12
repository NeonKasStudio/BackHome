using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootHit : MonoBehaviour
{
    public Transform enemy;
    public GameObject spawneableSFX;
    public AudioClip enemyFootHitSFX;

    public void PlayFootHitFX()
    {
        var go = Instantiate(spawneableSFX, enemy.position, enemy.rotation);
        go.GetComponent<AudioSource>().PlayOneShot(enemyFootHitSFX);
    }
}
