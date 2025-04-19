using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackPlayer : State
{
   public attackPlayer(AIcontroller ai) : base(ai)
    {

    }

    public override void Enter()
    {
        Debug.Log("Idle State");
    }

 
    public override void Update()
    {
        Ai.AttackPlayer();
        if (Ai.CanSeePlayer())
        {
            Ai.ChangeState(new StateChase(Ai));
        }
        else
        {
            // Debug.Log("Player line of sight lost");
            Ai.Patrolling();
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Attack State");
    }
}
