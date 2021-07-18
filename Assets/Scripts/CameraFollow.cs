using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 angleOffset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private void FixedUpdate()
    {

        HandleTranslation();
        HandleRotation();
    }

    private void HandleTranslation()
    {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }
    private void HandleRotation()
    {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up) * Quaternion.Euler(angleOffset);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}