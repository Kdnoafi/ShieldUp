using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitByEnemy : MonoBehaviour
{
    public Image I_healthBar, I_redFlash;
    public Sprite S_redFlash;
    public Text T_multiplierText, T_currentScore, T_highScore;
    public Canvas C_mainCanvas;
    public GameObject GO_healthPopUpPrefab, GO_speedUpPopUpPrefab, GO_deathCanvas, GO_gameplayCanvas;
    public GameObject GO_shield, GO_center, GO_deathSoundSource;

    public static int I_health = 5;
    public static bool B_dead = false;

    private bool B_shieldHit = false;
    private float F_step = 0f, F_health = 100f;

    void FixedUpdate()
    {
        if (B_shieldHit)
        {
            if (F_step <= 0.5f)
            {
                F_step += Time.deltaTime;
            }
            else
            {
                F_step = 0f;
                B_shieldHit = false;
            }
        }
    }

    void Die()
    {
        if(!MenuOptions.B_vibrationIsOn)
        {
           // Handheld.Vibrate();
        }

        //GO_deathSoundSource.GetComponent<AudioSource>().Play();

        B_dead = true;

        GO_gameplayCanvas.gameObject.SetActive(false);

        GameObject[] GO_allObjects = FindObjectsOfType<GameObject>();
        for (int i = 0; i < GO_allObjects.Length; i++)
        {
            if (GO_allObjects[i].name != "Main Camera" && GO_allObjects[i].name != "Background" && GO_allObjects[i].name != "Canvas" && GO_allObjects[i].name != "EventSystem" && GO_allObjects[i].name != "DeathSound")
            {
                Destroy(GO_allObjects[i]);
            }
        }

        GO_deathCanvas.gameObject.SetActive(true);
        T_currentScore.text = "Current Score\n" + DestroyEnemy.I_score.ToString();
        T_highScore.text = "High Score\n" + PlayerPrefs.GetInt(DestroyEnemy.S_bestScoreLocation).ToString();
    }

    void TakeDamage()
    {
        F_health -= 20f;
        I_healthBar.fillAmount = F_health / 100f;

        if (I_health > 1)
        {
            I_health--;
        }
        else
        {
            Die();
        }

        DestroyEnemy.I_combo = 0;
        T_multiplierText.gameObject.SetActive(false);
    }

    void ScreenFlash()
    {
        Image I_newRedFlash = Instantiate(I_redFlash, I_redFlash.transform.position, Quaternion.identity);
        I_newRedFlash.transform.SetParent(C_mainCanvas.transform, false);
        I_newRedFlash.sprite = S_redFlash;
        Color C_alphaColor = I_newRedFlash.color;
        C_alphaColor.a = 255f;
        I_newRedFlash.color = C_alphaColor;
        I_newRedFlash.CrossFadeAlpha(0f, 0.3f, false);
        Destroy(I_newRedFlash.gameObject, 0.3f);
    }

    void AddHealth()
    {
        I_health++;
        F_health += 20f;
        I_healthBar.fillAmount = F_health / 100f;

        GameObject GO_newHealthPupUp = Instantiate(GO_healthPopUpPrefab, new Vector2(0f, 0f), Quaternion.identity);
        Destroy(GO_newHealthPupUp, 0.2f);
    }

    IEnumerator SpeedUp()
    {
        ShieldMovement.F_rotationSpeed *= 1.4f;

        yield return new WaitForSeconds(5f);

        ShieldMovement.F_rotationSpeed /= 1.4f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            B_shieldHit = true;

            ScreenFlash();

            TakeDamage();
        }
        else if (collision.gameObject.tag == "health")
        {
            if (I_health < 5)
            {
                AddHealth();
            }
        }
        else if (collision.gameObject.tag == "speed")
        {
            StartCoroutine(SpeedUp());

            GameObject GO_newSpeedUpPopUp = Instantiate(GO_speedUpPopUpPrefab, GO_speedUpPopUpPrefab.transform.position, Quaternion.identity);
            Destroy(GO_newSpeedUpPopUp, 0.2f);
        }
    }
}
