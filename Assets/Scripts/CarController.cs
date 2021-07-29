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

    private float maxSpeed;
    public float tarmacMaxSpeed;
    public float grassMaxSpeed;
    
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
    public float distToGround;
    public bool lastFrameIsGrounded=true;
    private Vector3 regularCg;
    private Vector3 inAirCg;

    private float upsideDownTimer;
    private bool timeToFlip;


    private void Awake()
    {
        timeToFlip = false;
        rb = this.GetComponent<Rigidbody>();
        distToGround = this.GetComponent<Collider>().bounds.extents.y;
        //Debug.Log("Player distToGround" + distToGround.ToString());
        regularCg = rb.centerOfMass + Vector3.down * 0.3f;
        inAirCg = rb.centerOfMass + Vector3.down * 5f;
        rb.centerOfMass = regularCg;
        maxSpeed = tarmacMaxSpeed;
    }
    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();

        // restrict speed on grass
        if (IsOnGrass())
        {
            maxSpeed = grassMaxSpeed;
            rb.drag = 1f;
        }
        else
        {
            maxSpeed = tarmacMaxSpeed;
            rb.drag = 0f;
        }

        // adaptive CG to balance realistic behavior and prevent flip over
        if (IsGrounded() != lastFrameIsGrounded)
        {
            lastFrameIsGrounded = IsGrounded();
            if (lastFrameIsGrounded)
            {
                rb.centerOfMass = regularCg;
                Debug.Log("regular cg");
            }
            else
            {
                rb.centerOfMass = inAirCg;
                Debug.Log("recovery CG");
            }
        }
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
    private bool IsGrounded()
    {
        //return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        return Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), distToGround + 0.1f);
    }

    private bool IsOnGrass()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, distToGround + 0.1f))
        { 
            return hit.collider.gameObject.CompareTag("Grass");
        }
        else
        {
            return false;
        }
        
    }
    private void CorrectFlips()
    {
        if (Mathf.Abs(transform.rotation.eulerAngles.z) > 20f)
        {
            float direction = (transform.rotation.eulerAngles.z > 0f) ? -1f : 1f;
            rb.AddRelativeTorque(new Vector3(0, 0, direction * 1000));
            Debug.Log("correcting z rotation");
        }

        if (Mathf.Abs(transform.rotation.eulerAngles.x) > 20f)
        {
            float direction = (transform.rotation.eulerAngles.x > 0f) ? -1f : 1f;
            rb.AddRelativeTorque(new Vector3(direction * 1000, 0, 0));
            Debug.Log("correcting x rotation");
        }
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
                float dragForce = 0f;
                frontLeftWheelCollider.motorTorque = dragForce;
                frontRightWheelCollider.motorTorque = dragForce;
                rearLeftWheelCollider.motorTorque = dragForce;
                rearRightWheelCollider.motorTorque = dragForce;
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
