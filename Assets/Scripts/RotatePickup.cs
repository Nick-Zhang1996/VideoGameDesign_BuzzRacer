using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePickup : MonoBehaviour
{
    private float rotateSpeed = 0.3f;
    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed,0, Space.World);

    }
}
