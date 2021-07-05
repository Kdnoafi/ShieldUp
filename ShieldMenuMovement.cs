using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMenuMovement : MonoBehaviour
{
    public Transform T_rotationCenter;

    void FixedUpdate()
    {
        transform.RotateAround(T_rotationCenter.position, Vector3.forward, Time.deltaTime * -80f);
    }
}
