using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurePursuitAi : MonoBehaviour
{

    public GameObject Target;

    public float speed;
    public float steering;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateControl()
    {
        // calc 2d steering
        Debug.DrawLine(transform.position, Target.transform.position);
        
        Vector3 targetPosLocal = transform.InverseTransformVector(Traget.transform.position);
        float distance = Mathf.Sqrt(targetPosLocal.x * targetPosLocal.x + targetPosLocal.z * targetPosLocal.z);
        float targetAngle = Mathf.Atan2(targetPosLocal.z, targetPosLocal.x);

    }
}
