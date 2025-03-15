using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public State currentState;
   

    public void ChangeState(State NewState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = NewState;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}
