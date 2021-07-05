using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject GO_enemyPrefab;

    private Vector2 V2_randomPosition;

    public static float F_enemySpeed = 2.3f, F_timeToSpawnEnemy = 2f;

    private int I_upOrDownPosition;
    private float F_enemySpawnTime = 0f;

    void Start()
    {
        if(StartGame.B_constantEasy)
        {
            F_enemySpeed = 2.9f;
            F_timeToSpawnEnemy = 1.1f;
        }
        else if (StartGame.B_constantNormal)
        {
            F_enemySpeed = 3.35f;
            F_timeToSpawnEnemy = 0.75f;
        }
        else if (StartGame.B_constantHard)
        {
            F_enemySpeed = 3.6f;
            F_timeToSpawnEnemy = 0.55f;
        }
        else if (StartGame.B_constantExtreme)
        {
            F_enemySpeed = 3.75f;
            F_timeToSpawnEnemy = 0.42f;
        }
        else
        {
            F_enemySpeed = 2.3f;
            F_timeToSpawnEnemy = 2f;
        }
    }

    void FixedUpdate()
    {
        if (!HitByEnemy.B_dead)
        {
            F_enemySpawnTime += Time.deltaTime;

            if (F_enemySpawnTime >= F_timeToSpawnEnemy)
            {
                I_upOrDownPosition = Random.Range(0, 2);

                if (I_upOrDownPosition == 0)
                {
                    V2_randomPosition = new Vector2(Random.Range(-3.4f, 3.4f), 5.5f);
                }
                else
                {
                    V2_randomPosition = new Vector2(Random.Range(-3.4f, 3.4f), -5.5f);
                }

                Instantiate(GO_enemyPrefab, V2_randomPosition, Quaternion.identity);

                F_enemySpawnTime = 0f;
            }
        }
	}
}
