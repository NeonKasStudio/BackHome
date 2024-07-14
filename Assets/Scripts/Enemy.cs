using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    NavMeshAgent agent;
    public Animator animator;

    public float currentVelocity = 1f;
    public float maxVelocity = 6f;
    public float interpolationSpeed = 1f;
    public Collider colliderLataman;
    public Camera normalCamera;
    public Camera jumpscareCamera;
    public GameObject emergencySound;
    public GameObject chaseSound;

    bool triggerEnabled = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        colliderLataman.enabled = false;
    }

    private void Update()
    {
        agent.destination = player.position;
        agent.speed = currentVelocity * 1.5f;

        if(currentVelocity < maxVelocity)
        {
            currentVelocity += Time.deltaTime * interpolationSpeed;
            animator.SetFloat("speedMultiplier", currentVelocity);
        }
    }

    public void StartChase()
    {
        agent.enabled = true;
        agent.isStopped = false;
        animator.SetTrigger("chase");
        StartCoroutine(EnableColliderCoroutine());
    }

    IEnumerator EnableColliderCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        colliderLataman.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!colliderLataman.enabled || !triggerEnabled)
            return;

        if (other.CompareTag("Player")) {
            triggerEnabled = false;
            jumpscareCamera.enabled = true;
            normalCamera.enabled = false;
            FindObjectOfType<AudioManager>().LoudScream();
            StartCoroutine(EndScreamer());
        }
    }

    IEnumerator EndScreamer()
    {
        yield return new WaitForSeconds(3f);
        FindObjectOfType<AudioManager>().StopLoudScream();
        jumpscareCamera.enabled = false;
        Destroy(emergencySound);
        Destroy(chaseSound);
        Destroy(animator);
    }
}
