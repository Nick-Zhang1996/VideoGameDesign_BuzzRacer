using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePickup : MonoBehaviour
{
    public float rotateSpeed;
    private void Start()
    {
        rotateSpeed = 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed,0, Space.World);

    }
}
