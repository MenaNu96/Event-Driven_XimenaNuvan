using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{

    public Transform PlayerTransform;
    public NavMeshAgent Nav;
    public Animator anim;
    public LayerMask whatIsGround, WhatIsPlayer;

    [Header("-------- Patroling --------")]
    public Vector3 WalkPoint;
    bool walKPointSet;
    public float walkpointrange;

    [Header("-------- Attack --------")]
    public float timeBetweenattacks;
    bool AlreadyAttacked;

    [Header("-------- States --------")]
    public float SightRange, attackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    // Start is called before the first frame update
    private void Awake()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Body").transform;
        Nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);
        if (!PlayerInSightRange && !PlayerInAttackRange) Patrolling();
        if (PlayerInSightRange && !PlayerInAttackRange) ChasePlayer();
        if (PlayerInAttackRange && !PlayerInSightRange) AttackPlayer();
       // Nav.destination = PlayerTransform.position;
    }

    private void Patrolling()
    {
        anim.SetTrigger("Walk");
        if (!walKPointSet) SearchWalkPoint();
        if (walKPointSet) Nav.SetDestination(WalkPoint);
        Vector3 distanceToWalkPoint = transform.position - WalkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walKPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkpointrange, walkpointrange);
        float randomX = Random.Range(-walkpointrange, walkpointrange);
        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(WalkPoint, -transform.up, 2f, whatIsGround)) walKPointSet = true;
    }

    private void ChasePlayer()
    {
        anim.SetTrigger("chase");
        Nav.SetDestination(PlayerTransform.position);
        
    }

    private void AttackPlayer() 
    {
        anim.SetTrigger("AttackScream");
        Nav.SetDestination(PlayerTransform.position);
        transform.LookAt(PlayerTransform);
        if (!AlreadyAttacked)
        {
            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenattacks);
        }
    }

    private void ResetAttack()
    {
        AlreadyAttacked = false;
    }
}

