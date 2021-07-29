using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Collider MainCollider;
    private Collider[] AllColliders;
    private Rigidbody[] AllRigigidbodies;
    private bool currentRagdollEnabled;
    public bool forceRagdoll = false;
    public float maxCollisionForce;
    public GameObject goal;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public float travelDistance;
    private float startDelay;
    private bool isWalking = false;
    private Animator animator;
    

    public Rigidbody pelvis;
    void OnDrawGizmosSelected()
    {

        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * travelDistance);

    }
    private void Awake()
    {
        MainCollider = GetComponent<Collider>();
        AllColliders = GetComponentsInChildren<Collider>(true);
        AllRigigidbodies = GetComponentsInChildren<Rigidbody>(true);
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        DoRagdoll(false);
        Debug.DrawLine(initialPosition, initialPosition + Vector3.forward * travelDistance, Color.white, 100f);
        startDelay = (float)Random.Range(0, 10);
    }

    public void Restart()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        DoRagdoll(false);
        Debug.Log("ragdoll restart");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWalking && Time.timeSinceLevelLoad > startDelay)
        {
            //Debug.Log("start walking");
            animator.SetTrigger("StartWalking");
            isWalking = true;
        }
        if (forceRagdoll)
        {
            DoRagdoll(true);
            //pelvis.AddForce(new Vector3(0f, impulseForce, 0f), ForceMode.Impulse);
            forceRagdoll = false;
            Debug.Log("Forced ragdoll mode");
        }

        if ((transform.position - initialPosition).magnitude > travelDistance)
        {
            Restart();
        }
    }

    public void DoRagdoll(bool isRagdoll)
    {
        foreach (var col in AllColliders)
            col.enabled = isRagdoll;
        MainCollider.enabled = !isRagdoll;
        foreach (var rb in AllRigigidbodies)
            rb.useGravity = isRagdoll;
        GetComponent<Animator>().enabled = !isRagdoll;
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("enable rugdoll");
            DoRagdoll(true);
            var player_rb = collider.gameObject.GetComponent<Rigidbody>();
            float impulseForce = Mathf.Min(1f, player_rb.velocity.magnitude / 25f) * maxCollisionForce;
            pelvis.AddForce(new Vector3(0f, impulseForce, 0f), ForceMode.Impulse);
            return;
        }

        /*
        if (collider.gameObject.GetComponent<PedestrianGoal>() != null)
        {
            Restart();
        }
        (
        */
    }

}
