using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    [SerializeField] private EnemyVision enemyVision;
    [SerializeField] private FollowPlayerAI followPlayerAI;
    [SerializeField] private float chaseDuration = 10f;
    private enum AIState { Idle, Chase, Return}
    private AIState currentState = AIState.Idle;

    public Vector3 homePosition;
    private float chaseTimer;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyVision = GetComponent<EnemyVision>();
        followPlayerAI = GetComponent<FollowPlayerAI>();

        homePosition = transform.position;
        followPlayerAI.StopFollowing();
    }

    private void Update()
    {
        switch (currentState)
        {
            case AIState.Idle:
                if (enemyVision.CanSeePlayer())
                {
                    currentState = AIState.Chase;
                    followPlayerAI.StartFollowing();
                    chaseTimer = chaseDuration;
                }
                break;

            case AIState.Chase:
                if (enemyVision.CanSeePlayer())
                {
                    chaseTimer = chaseDuration;
                }
                else
                {
                    chaseTimer -= Time.deltaTime;
                    if (chaseTimer <= 0)
                    {
                        currentState = AIState.Return;
                        followPlayerAI.StopFollowing();
                        agent.SetDestination(homePosition);
                    }
                }
                break;

            case AIState.Return:
                if (enemyVision.CanSeePlayer())
                {
                    currentState = AIState.Chase;
                    followPlayerAI.StartFollowing();
                    chaseTimer = chaseDuration;
                }
                else
                {
                    agent.SetDestination(homePosition);
                    if (Vector3.Distance(transform.position, homePosition) < 0.5f)
                    {
                        currentState = AIState.Idle;
                    }
                }
                break;
        }   
    }
}
