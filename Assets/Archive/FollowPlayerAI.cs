using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerAI : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;
        agent.speed = followSpeed;
    }

    void FixedUpdate()
    {
        // Vector3 desiredPosition = (target.position - transform.position).normalized;
        agent.SetDestination(target.position);
    }
}
