using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePickup : MonoBehaviour
{
    public int rotateSpeed;
    private void Start()
    {
        rotateSpeed = 2;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed,0, Space.World);

    }
}
