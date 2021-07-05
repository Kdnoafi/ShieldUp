using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAddSpawn : MonoBehaviour
{
    public GameObject GO_speedAddPrefab;

    private Vector2 V2_randomSpawnPosition;

    private float F_speedAddSpawnTime = 0f;
    private int I_upOrDownPosition;

    void FixedUpdate()
    {
      /*  F_speedAddSpawnTime += Time.deltaTime;

        if (F_speedAddSpawnTime >= 30f)
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

            Instantiate(GO_speedAddPrefab, V2_randomSpawnPosition, Quaternion.identity);

            F_speedAddSpawnTime = 0f;
        }*/
    }
}
