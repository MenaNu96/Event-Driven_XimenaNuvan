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

           // Debug.Log("Can see player");
        }
        else if (Ai.CanHearPlayer(Ai.playervolume) && !Ai.CanSeePlayer())
        {
            Ai.ChangeState(new StateSearch( Ai));
        } 
        else
        {
           // Debug.Log("Player line of sight lost");
            Ai.Patrolling();
        }
    }


    public override void Exit()
    {
        //Debug.Log("Exit Patrol state");
    }
}
