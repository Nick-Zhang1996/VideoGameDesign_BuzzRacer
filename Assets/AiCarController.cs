using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AiCarController : MonoBehaviour
{
    private Rigidbody rb;
    private ParticleSystem explosion;
    public GameObject visual;
    private PurePursuitAi ai;

    public float P = 1.0f;

    public float targetSpeed = 0.0f;
    public float steerAngle = 0.0f;
    public float throttle = 0.0f;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    public float maxSteeringAngle;
    public float motorForce;
    public float brakeForce;

  


    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        ai = this.GetComponent<PurePursuitAi>();
        explosion = this.GetComponent<ParticleSystem>();
    }
    private void FixedUpdate()
    {
        ai.UpdateControl();
        steerAngle = ai.steering;
        targetSpeed = ai.targetSpeed;
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Pursuiter"))
        {
            Debug.Log("Ai go booom");
            explosion.Play();
            visual.SetActive(false);
            Destroy(gameObject, explosion.main.duration);
        }
    }
    private void HandleSteering()
    {
        //steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        // z is forward direction
        // x rightward
        Vector3 localVelocity = transform.InverseTransformVector(rb.velocity);
        // forward and braking
        
       
        if (localVelocity.z < targetSpeed)
        {
            throttle = (targetSpeed - localVelocity.z) * P;
        }
        else
        {
            throttle = 0f;
        }
        frontLeftWheelCollider.motorTorque = throttle * motorForce;
        frontRightWheelCollider.motorTorque = throttle * motorForce;
        rearLeftWheelCollider.motorTorque = throttle * motorForce;
        rearRightWheelCollider.motorTorque = throttle * motorForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

}