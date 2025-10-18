using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;

public class AgentMove2d : MonoBehaviour, IAgentOverride
{
    private AgentOverride2d agentOverride2D;
    private Rigidbody2D rb;

    void Awake()
    {
        agentOverride2D = GetComponent<AgentOverride2d>();
        rb = GetComponent<Rigidbody2D>();

        rb.isKinematic = true;
        
        agentOverride2D.agentOverride = this;
    }

    public void UpdateAgent()
    {
        var agent = agentOverride2D.Agent;
        if (agent == null) return;

        rb.MovePosition(agentOverride2D.Agent.nextPosition);
    }
}
