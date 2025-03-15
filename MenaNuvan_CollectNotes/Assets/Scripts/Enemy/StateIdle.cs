using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State
{
   public StateIdle(AIcontroller ai) : base(ai)
    {

    }

    public override void Enter()
    {
        Debug.Log("Idle State");
    }

 
    public override void Update()
    {
       if (Ai.CanSeePlayer())
        {
            Ai.ChangeState(new StateChase(Ai));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Idle State");
    }
}
