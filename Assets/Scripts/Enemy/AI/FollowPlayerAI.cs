using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayerAI : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;
    private NavMeshAgent agent;
    private bool isActive = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = followSpeed;
    }

    void FixedUpdate()
    {
        if (!isActive || agent == null || !agent.isOnNavMesh || target == null) return;
        agent.SetDestination(target.position);
    }

    public void StartFollowing() => isActive = true;
    public void StopFollowing() => isActive = false;
}
