using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianGoal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var ragdoll = collision.collider.gameObject.GetComponent<RagdollController>();
        if (ragdoll != null)
        {
            Debug.Log("Pedestrian Goal triggered");
            ragdoll.Restart();
        }
    }
}
