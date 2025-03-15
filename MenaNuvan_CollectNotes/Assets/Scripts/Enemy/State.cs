using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    protected AIcontroller Ai;
    public State(AIcontroller ai ) 
    {
      this.Ai = ai;
    }

    public abstract void Enter();

    public abstract void Update();

    public abstract void Exit();
}
