using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIcontroller : MonoBehaviour
{
    [Header("-------- Basic Settings --------")]
    public Transform PlayerTransform;
    public NavMeshAgent Nav;
    public Animator anim;
    public LayerMask whatIsGround, WhatIsPlayer;
  
    public Vector3 positionWoman;
    public AudioClip Attacking;
    public AudioClip Chasing;
  //  public AudioSource Walking;

    [Header("-------- State Machine --------")]
    StateMachine statemachine;
    public Transform player;
    public float playervolume = 15f;
    public Transform VisionCone;

    public bool IsManaged = true;
    public Vector3 lastknownPlayerPos; 
    public bool playerInCone;
    public bool canSeePlayer;

    [Header("-------- Patroling --------")]

    public Vector3 WalkPoint;
    bool walKPointSet;
    public float walkpointrange;
   
    

   // public Transform[] patrolWayPoints;
    public int currentWaypointsIndex;
    public float PatrolSpeed = 5;
    public float visionAngle = 90f;
    public float detectionRange = 3;

    public float hearRange = 15f;
    public float hearingthreshold = 10f;

    [Header("-------- Attack --------")]
    public float timeBetweenattacks;
    bool AlreadyAttacked;
    public GameObject Monster;
    public Animator animation;

    [Header("-------- States --------")]
    public float SightRange, attackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    private void Start()
    {
        statemachine = new StateMachine();
        statemachine.ChangeState(new StateIdle(this));
        Monster.SetActive(false);
        
    }

    private void Update()
    {
           statemachine.Update();
        PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);
        Patrolling();
        ChasePlayer();
         AttackPlayer();
    }

    private void Awake()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Body").transform;
        Nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public bool CanSeePlayer()
    {
        return HasLineOfSight(player);
        //return Vector3.Distance(transform.position, player.position) < detectionRange;
    }

    public void AttackPlayer()
    {
        if (PlayerInAttackRange) {
          //  anim.Play("Scream");
            
            Nav.SetDestination(PlayerTransform.position);
       // transform.LookAt(PlayerTransform);
       // StartCoroutine(Screamer());
    }
       
       
        if (!AlreadyAttacked)
        {
            AlreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenattacks);
        }
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Body"))
    //    {
            
    //        //GameObject.FindGameObjectWithTag("Woman").GetComponent<AudioSource>().PlayOneShot(Attacking);
            
    //        //Monster.SetActive(true);
    //    }
    //    else
    //    {
    //        anim.Play("Chasing");
    //    }
    //}
    //public IEnumerator Screamer()
    //{
    //    yield return new WaitForSeconds(1);
    //   // Monster.SetActive(false);
        
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Body"))
    //    {
           
    //        //StartCoroutine(Screamer());

    //        Monster.SetActive(false);
    //    }
    //}
  

    private void ResetAttack()
    {
        AlreadyAttacked = false;
    }
    public bool CanHearPlayer(float noiselevel)
    {
        if (player != null) return false;
        if(Vector3.Distance (transform.position, player.position) < hearRange && noiselevel > hearingthreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPlayerInVisionCone(bool isVisible)
    {
        playerInCone = isVisible;
    }

    public bool HasLineOfSight(Transform target)
    {
        /*if (!playerInCone)
        {
            return false;
        }
        Vector3 directionToTareget = (target.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToTareget, out hit, detectionRange))
        {
            if (hit.transform == target)
            {
                return true;
            }
        }
        return false;*/

        Vector3 directionToTareget = (target.position - transform.position).normalized;
        float AngleToPlayer = Vector3.Angle(transform.forward, directionToTareget);
        if (AngleToPlayer < visionAngle / 2)
        {
            Debug.Log("Player in cone");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToTareget, out hit, detectionRange))
            {
                if (hit.transform == target)
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    public void ChangeState(State newState)
    {
        statemachine.ChangeState(newState);

        
    }

    public void ChasePlayer()
    {
        if (PlayerInSightRange && !PlayerInAttackRange)
        {
           // GameObject.FindGameObjectWithTag("Woman").GetComponent<AudioSource>().PlayOneShot(Chasing, 0.1f);
           
            anim.Play("chase");
            Nav.SetDestination(PlayerTransform.position);
        }
        else 
        {
            Patrolling();
           
        }
       
        
    
        //transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * PatrolSpeed);
        //Vector3 direction = (player.position - transform.position).normalized;
        //if (direction != Vector3.zero)
        //{
        //    float rotationspeed = 3f;
        //    Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationspeed * Time.deltaTime, 0);
        //    transform.rotation = Quaternion.LookRotation(newDirection);

        //}
    }
    public void Patrolling()
    {
        //patrol here
        //if (patrolWayPoints.Length == 0)
        //{
        //    return;
        //}
        //rotate the Ai toward the next waypoint
        
        if (!PlayerInSightRange && !PlayerInAttackRange)
        {
            anim.Play("Walk");
            if (!walKPointSet) SearchWalkPoint();
            if (walKPointSet) Nav.SetDestination(WalkPoint);
            Vector3 distanceToWalkPoint = transform.position - WalkPoint;
            if (distanceToWalkPoint.magnitude < 1f)
                walKPointSet = false;
        }

        //Transform targetWayPoint = patrolWayPoints[currentWaypointsIndex];
        //Vector3 direction = (targetWayPoint.position - transform.position).normalized;
        //if (direction != Vector3.zero)
        //{
        //    float rotationspeed = 3f;
        //    Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationspeed * Time.deltaTime, 0);
        //    transform.rotation = Quaternion.LookRotation(newDirection);

        //}
        //transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, Time.deltaTime * PatrolSpeed);

        //if (Vector3.Distance(transform.position, targetWayPoint.position) < 0.2f)
        //{
        //    currentWaypointsIndex = (currentWaypointsIndex + 1) % patrolWayPoints.Length;
        //}
 
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkpointrange, walkpointrange);
        float randomX = Random.Range(-walkpointrange, walkpointrange);
        WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(WalkPoint, -transform.up, 2f, whatIsGround)) walKPointSet = true;
    }
}
