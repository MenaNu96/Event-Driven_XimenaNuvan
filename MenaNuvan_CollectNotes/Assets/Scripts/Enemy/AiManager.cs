using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    public List<AIcontroller> registeredAgents = new List<AIcontroller>();


    public static AiManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
   
    public void REGISTERAGENT(AIcontroller AI)
    {
        if (AI.IsManaged)
        {
            registeredAgents.Add(AI);
        }

    }

    public void UNREGISTERAGENT(AIcontroller AI)
    {
        if (AI.IsManaged)
        {
            registeredAgents.Remove(AI);
        }

    }

    public void AlertPlayerSpotted()
    {
        //Player Spotted
        //Update player pos
        // notify all managed agents

        foreach ( var ai in registeredAgents )
        {
            ai.ChangeState(new StateSearch(ai));
        }
    }
}
