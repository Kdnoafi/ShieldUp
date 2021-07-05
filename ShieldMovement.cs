using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShieldMovement : MonoBehaviour
{
    public Transform T_rotationCenter;

    public static float F_rotationSpeed = 150f;

    void Start()
    {
        if (StartGame.B_constantEasy)
        {
            F_rotationSpeed = 180f;
        }
        else if (StartGame.B_constantNormal)
        {
            F_rotationSpeed = 220f;
        }
        else if (StartGame.B_constantHard)
        {
            F_rotationSpeed = 285f;
        }
        else if (StartGame.B_constantExtreme)
        {
            F_rotationSpeed = 335f;
        }
        else
        {
            F_rotationSpeed = 150f;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                F_rotationSpeed *= -1;
            }
        }

        transform.RotateAround(T_rotationCenter.position, Vector3.forward, Time.deltaTime * F_rotationSpeed);
    }
}
