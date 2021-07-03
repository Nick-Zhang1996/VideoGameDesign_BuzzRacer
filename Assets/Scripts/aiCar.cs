using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class aiCar : MonoBehaviour
{

    public Animator anim;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    public GameObject userCar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = userCar.transform.position;
        VelocityReporter vr = userCar.GetComponent<VelocityReporter>();
        float distance = (currentPos - navMeshAgent.transform.position).magnitude;
        float lookAheadT = distance / navMeshAgent.speed;
        Vector3 futureTarget = currentPos + (lookAheadT * vr.velocity);
        navMeshAgent.SetDestination(futureTarget);
        

        //anim.SetFloat("vely", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
    }

    
}
