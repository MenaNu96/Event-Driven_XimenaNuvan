using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : State
{
    public StatePatrol(AIcontroller ai) : base(ai)
    {

    }

    public override void Enter()
    {
        Debug.Log("Patrol State");
    }


    public override void Update()
    {
        if (Ai.CanSeePlayer())
        {
            Ai.ChangeState(new StateChase(Ai));

        }

        else
        {
            Ai.patrol();
        }
    }


    public override void Exit()
    {
        Debug.Log("Exit Patrol state");
    }
}
