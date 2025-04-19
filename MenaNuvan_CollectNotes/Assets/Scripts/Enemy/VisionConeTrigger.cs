using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// MESH TO DETERMINE VISION
/// </summary>
public class VisionConeTrigger : MonoBehaviour
{

    private AIcontroller ai;

    // Start is called before the first frame update
    void Start()
    {
     ai = GetComponentInParent<AIcontroller>();   
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ai.SetPlayerInVisionCone(true);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        if (ai.HasLineOfSight(other.transform))
    //        {
    //            ai.ChangeState(new StateChase(ai));
    //        }
    //    }
    //}
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           ai.SetPlayerInVisionCone(false);
        }
    }
}
