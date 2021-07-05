using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAddSpawn : MonoBehaviour
{
    public GameObject GO_healthAdd;

    private Vector2 V2_randomSpawnPosition;

    public static bool B_healthAddSpawned = false;

    private int I_spawnChance, I_upOrDownPosition;
    private float F_spawnTime = 0f;

    void FixedUpdate()
    {
        if (!StartGame.B_normalNoHeal && HitByEnemy.I_health < 5 && !B_healthAddSpawned && !HitByEnemy.B_dead)
        {
            F_spawnTime += Time.deltaTime;

            if (F_spawnTime >= 1f)
            {
                I_spawnChance = Random.Range(0, 5);

                if (I_spawnChance == 2)
                {
                    I_upOrDownPosition = Random.Range(0, 2);

                    if (I_upOrDownPosition == 0)
                    {
                        V2_randomSpawnPosition = new Vector2(Random.Range(-3.4f, 3.4f), 5.5f);
                    }
                    else
                    {
                        V2_randomSpawnPosition = new Vector2(Random.Range(-3.4f, 3.4f), -5.5f);
                    }

                    Instantiate(GO_healthAdd, V2_randomSpawnPosition, Quaternion.identity);
                    B_healthAddSpawned = true;
                }

                F_spawnTime = 0f;
            }
        }
    }
}
