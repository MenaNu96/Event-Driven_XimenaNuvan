using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChase : State
{
    public StateChase(AIcontroller ai) : base(ai)
    {

    }

    public override void Enter()
    {
        Debug.Log("Chase Player");
    }


    public override void Update()
    {
        Ai.Chase();
        if(!Ai.CanSeePlayer())
        {
            Ai.ChangeState(new StatePatrol(Ai));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exist Chase Player");
    }
}

