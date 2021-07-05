using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAddMovement : MonoBehaviour
{ 
    private GameObject GO_rotationCenter;

    void Start()
    {
        GO_rotationCenter = GameObject.FindGameObjectWithTag("center");
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, GO_rotationCenter.transform.position, Time.deltaTime * 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "enemy")
        {
            Destroy(gameObject);
        }
    }
}
