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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
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
    }
}
