using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIcontroller : MonoBehaviour
{
    StateMachine statemachine;
    public Transform player;
    public Transform[] patrolWayPoints;
    public int currentWaypointsIndex;
    public float PatrolSpeed = 5;
    public float detectionRange = 3;


    private void Start()
    {
        statemachine = new StateMachine();
        statemachine.ChangeState(new StateIdle(this));
    }

    private void Update()
    {
           statemachine.Update();
    }

    public bool CanSeePlayer()
    {
        return Vector3.Distance(transform.position, player.position) < detectionRange;
    }

    public void ChangeState(State newState)
    {
        statemachine.ChangeState(newState);
    }

    public void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * PatrolSpeed);
    }
    public void patrol()
    {
        if (patrolWayPoints.Length == 0)
        {
            return;
        }
        Transform targetWayPoint = patrolWayPoints[currentWaypointsIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, Time.deltaTime * PatrolSpeed);

        if (Vector3.Distance(transform.position, targetWayPoint.position) < 0.2f)
        {
            currentWaypointsIndex = (currentWaypointsIndex + 1) % patrolWayPoints.Length;
        }
        
      
    }
}
