using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurePursuitAi : MonoBehaviour
{

    public GameObject Target;
    private Rigidbody rb;
    public float minSpeed = 5.0f;
    public float targetSpeed;
    public float steering;
    private float wheelbase = 3.33f;
    // Start is called before the first frame update
    void Start()
    {
        rb = Target.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateControl()
    {
        // calc 2d steering
        Debug.DrawLine(transform.position, Target.transform.position);
        
        Vector3 targetPosLocal = transform.InverseTransformPoint(Target.transform.position);
        float distance = Mathf.Sqrt(targetPosLocal.x * targetPosLocal.x + targetPosLocal.z * targetPosLocal.z);
        float targetAngle = Mathf.Atan2(targetPosLocal.z, targetPosLocal.x);
        float radius = distance / (2f * Mathf.Cos(targetAngle));
        steering = Mathf.Atan(wheelbase / radius) * (targetAngle > 0 ? 1f : -1f);
        steering = Mathf.Rad2Deg * steering;

        targetSpeed = Mathf.Max(minSpeed, rb.velocity.magnitude);

    }
}
