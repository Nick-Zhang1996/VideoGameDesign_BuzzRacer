using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle = 0.0f;
    public float maxSpeed;
    public bool handbrakeOn = false;
    public bool mainBrakeOn = false;
    private Rigidbody rb;

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
    public float handbrakeForce;
    public float brakeForce;

    public float currentMotorTorque;
    public float currentBrakeTorque;

    private float upsideDownTimer;
    private bool timeToFlip;


    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        timeToFlip = false;
    }

    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        HandleUpsideDown();
    }
    void OnHandbrake(InputValue movementValue)
    {
        
        float val = movementValue.Get<float>();
        handbrakeOn = val > 0.5f;

    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        //throttle
        verticalInput = movementVector.y;
        horizontalInput = movementVector.x;


    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        // z is forward direction
        // x rightward
        Vector3 localVelocity = transform.InverseTransformVector(rb.velocity);
        // forward and braking
        if (localVelocity.z > 0f && verticalInput < 0f )
        {
            mainBrakeOn = true;
            frontLeftWheelCollider.brakeTorque = brakeForce;
            frontRightWheelCollider.brakeTorque = brakeForce;
            rearLeftWheelCollider.brakeTorque = brakeForce;
            rearRightWheelCollider.brakeTorque = brakeForce;
            frontLeftWheelCollider.motorTorque = 0f;
            frontRightWheelCollider.motorTorque = 0f;
            rearLeftWheelCollider.motorTorque = 0f;
            rearRightWheelCollider.motorTorque = 0f;
            currentMotorTorque = verticalInput * motorForce;
            currentBrakeTorque = brakeForce;
        } else if (localVelocity.z < -0f && verticalInput > 0f)
        {
            mainBrakeOn = true;
            frontLeftWheelCollider.brakeTorque = brakeForce;
            frontRightWheelCollider.brakeTorque = brakeForce;
            rearLeftWheelCollider.brakeTorque = brakeForce;
            rearRightWheelCollider.brakeTorque = brakeForce;
            frontLeftWheelCollider.motorTorque = 0f;
            frontRightWheelCollider.motorTorque = 0f;
            rearLeftWheelCollider.motorTorque = 0f;
            rearRightWheelCollider.motorTorque = 0f;
            currentBrakeTorque = brakeForce;
        } else
        {
            mainBrakeOn = false;
            frontLeftWheelCollider.brakeTorque = 0f;
            frontRightWheelCollider.brakeTorque = 0f;
            rearLeftWheelCollider.brakeTorque = 0f;
            rearRightWheelCollider.brakeTorque = 0f;

            if (Mathf.Abs(localVelocity.z) < maxSpeed)
            {
                frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
                frontRightWheelCollider.motorTorque = verticalInput * motorForce;
                rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
                rearRightWheelCollider.motorTorque = verticalInput * motorForce;
                
            }
            else
            {
                frontLeftWheelCollider.motorTorque = 0f;
                frontRightWheelCollider.motorTorque = 0f;
                rearLeftWheelCollider.motorTorque = 0f;
                rearRightWheelCollider.motorTorque = 0f;
            }
            currentMotorTorque = verticalInput * motorForce;
            currentBrakeTorque = 0f;
        }
        

        if (!mainBrakeOn)
        {
            float handbrakeTorque = handbrakeOn ? handbrakeForce : 0f;
            rearLeftWheelCollider.brakeTorque = handbrakeTorque;
            rearRightWheelCollider.brakeTorque = handbrakeTorque;
        }
        
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


    /// <summary>
    /// Applies rotation force and Y force in order to flip car over until it's
    /// z rotation is within a specified range.
    /// </summary>
    /// <param name="tolerance">The number in degrees for it to be correct again and stop applying force</param>
    private void FlipCarOver(float tolerance)
    {
        float currRotation = this.GetComponent<Transform>().localEulerAngles.z;
        Vector3 angularVelocity = new Vector3(0, 0, -360);
        Quaternion deltaRotation = Quaternion.Euler(angularVelocity * Time.fixedDeltaTime);

        Debug.Log("Should have applied a force");
        Vector3 upForce = new Vector3(0f, 300.0f, 0f);
        rb.AddForce(upForce, ForceMode.Impulse);
        rb.MoveRotation(rb.rotation * deltaRotation);

        if (Math.Abs(currRotation) < tolerance)
        {
            timeToFlip = false;
        }
    }

    private void HandleUpsideDown()
    {
        float currRotation = this.GetComponent<Transform>().localEulerAngles.z;
        //Debug.Log("Rotation z axis | " + currRotation);
        if (Math.Abs(currRotation) >= 89 && Math.Abs(currRotation) <= 271)
        {
            if (upsideDownTimer > 3.0f)
            {
                timeToFlip = true;
            }
            else
            {
                Debug.Log("We are adding to the upside down timer | " + upsideDownTimer); ;
                upsideDownTimer += Time.deltaTime;
            }
        }
        else
        {
            upsideDownTimer = 0f;
        }

        if (timeToFlip)
        {
            FlipCarOver(20);
        }
    }
}