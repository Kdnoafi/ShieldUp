using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class RestartGame : MonoBehaviour
{
    private float F_timeToRestart = 0f;

    void Update()
    {
        if (HitByEnemy.B_dead)
        {
            if (F_timeToRestart < 1f)
            {
                F_timeToRestart += Time.deltaTime;
            }
            else//nzm da li je dobro da bude do 1 sec, stavi da se nesto broji(coini) ili tako nesto dok se ceka
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (EventSystem.current.currentSelectedGameObject == null)
                    {
                        Restart();
                    }
                }
            }
        }
    }

    void ResetValues()
    {
        HitByEnemy.B_dead = false;

        DestroyEnemy.I_combo = 0;
        DestroyEnemy.I_numberOfFlicks = 0;
        DestroyEnemy.I_score = 0;

        HitByEnemy.I_health = 5;

        HealthAddSpawn.B_healthAddSpawned = false;
    }

    public void Home()
    {
        ResetValues();

        StartGame.B_normalClassic = false;
        StartGame.B_normalNoHeal = false;
        StartGame.B_gameStarted = false;
        StartGame.B_constantEasy = false;
        StartGame.B_constantNormal = false;
        StartGame.B_constantHard = false;
        StartGame.B_constantExtreme = false;

        DestroyEnemy.S_bestScoreLocation = "";

        SceneManager.LoadScene(0);
    }

    void Restart()
    {
        ResetValues();

        SceneManager.LoadScene(1);
    }
}
