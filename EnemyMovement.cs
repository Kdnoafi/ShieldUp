using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject GO_rotationCenter;

    private bool B_enemyIsMoving = true;

    private float F_movmentSpeed;

    void Start()
    {
        GO_rotationCenter = GameObject.FindGameObjectWithTag("center");

        F_movmentSpeed = EnemySpawn.F_enemySpeed;
    }

    void FixedUpdate()
    {
        if (B_enemyIsMoving)
        {
            MoveEnemy();
        }
    }

    void MoveEnemy()
    {
        Vector2 V2_position = GO_rotationCenter.transform.position - transform.position;

        float F_angle = Mathf.Atan2(V2_position.y, V2_position.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(F_angle, Vector3.forward);
        transform.Translate(Vector2.right * Time.deltaTime * F_movmentSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "enemy" && collision.gameObject.tag != "health" && collision.gameObject.tag != "speed")
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<CapsuleCollider2D>().enabled = false;
            GameObject GO_enemyGlow = this.gameObject.transform.GetChild(2).gameObject;
            GO_enemyGlow.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponentInChildren<ParticleSystem>().Stop();

            B_enemyIsMoving = false;

            Destroy(gameObject, 2f);
        }
    }
}
