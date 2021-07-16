using UnityEngine;


public class NPCLogicHandler : MonoBehaviour
{
    private Animator profAnimator;
    private UnityEngine.AI.NavMeshAgent profMeshAgent;
    public GameObject[] waypoints;
    private int currWaypoint;
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject professor = GameObject.Find("professor");
        profAnimator = professor.GetComponent<Animator>();
        profMeshAgent = professor.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Debug.Log("I have found the  professor. | "+ profAnimator);
        profAnimator.SetBool("startWalking", true);
        currWaypoint = 0;
        profMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!profMeshAgent.pathPending && profMeshAgent.remainingDistance - profMeshAgent.stoppingDistance <= 0.2) {
            setNextWaypoint();
        }
    }

    private void setNextWaypoint() {
        if (currWaypoint == 0)
        {
            currWaypoint = 1;
            profMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);
        }
        else {
            currWaypoint = 0;
            profMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);
        }
    }

    
}
