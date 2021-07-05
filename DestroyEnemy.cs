using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject GO_enemyDestroyEffect, GO_shieldAdd, GO_shieldAdd1, GO_shieldAdd2, GO_shieldAdd3, GO_shieldHitPrefab;
    public Text T_score, T_popupScoreText, T_multiplierText, T_newBestScore, T_speedUpText;
    public Image I_shieldUp, I_blackScreen;
    public Button B_shieldUp;
    public Animator A_powerUpButtonAnimator, A_popupScoreAnimator, A_scoreMultiplierAnimator, A_scoreAnimator;
    public Animator A_shieldHitAnimator, A_newBestScoreAnimator;
    public Canvas C_mainCanvas;

    private GameObject GO_bestScore;

    public static int I_combo = 0, I_numberOfFlicks = 0, I_score = 0;
    public static string S_bestScoreLocation = "";

    private int I_scoreMultiplayer = 1;
    private float F_power = 0f, F_blueShiledFlickTime = 0f;
    private bool B_shieldIsUp = false, B_blueShieldIsVisible = true, B_blueShieldDestroyFlick = false, B_blueShieldIsVisible1 = false;
    private bool B_newBestAnimation = false, B_speedUp = false, B_speedUp1 = false, B_speedUp2 = false, B_speedUp3 = false;

    private bool B_bestScoreSet = false, B_blackScreenFaded = false;

    void FixedUpdate()
    {
        if(!B_blackScreenFaded)
        {
            Color C_blackScreenColor = I_blackScreen.color;

            if (I_blackScreen.color.a > 0)
            {
                C_blackScreenColor.a -= Time.deltaTime * 3f;
                I_blackScreen.color = C_blackScreenColor;
            }
            else
            {
                B_blackScreenFaded = true;
            }
        }

        if(!B_bestScoreSet && StartGame.B_gameStarted)
        {
            GO_bestScore = GameObject.FindGameObjectWithTag("bestscoretext");
            GO_bestScore.GetComponent<Text>().text = "Best:" + (PlayerPrefs.GetInt(S_bestScoreLocation, 0)).ToString();

            B_bestScoreSet = true;
        }

        if (B_shieldIsUp && I_numberOfFlicks < 11)
        {
            F_blueShiledFlickTime += Time.deltaTime;

            if (F_blueShiledFlickTime >= 0.05f)
            {
                GO_shieldAdd.GetComponent<SpriteRenderer>().enabled = B_blueShieldIsVisible;
                B_blueShieldIsVisible = !B_blueShieldIsVisible;
                F_blueShiledFlickTime = 0f;
                I_numberOfFlicks++;
            }
        }

        if (B_blueShieldDestroyFlick)
        {
            F_blueShiledFlickTime += Time.deltaTime;

            if (F_blueShiledFlickTime >= 0.05f)
            {
                GO_shieldAdd.GetComponent<SpriteRenderer>().enabled = B_blueShieldIsVisible1;
                B_blueShieldIsVisible1 = !B_blueShieldIsVisible1;
                F_blueShiledFlickTime = 0f;
            }
        }
    }

    void Start()
    {
        if (StartGame.B_normalNoHeal)
        {
            Camera.main.GetComponent<HealthAddSpawn>().enabled = false;
        }
        else
        {
            Camera.main.GetComponent<HealthAddSpawn>().enabled = true;
        }

        T_score.text = "0";
    }

    IEnumerator DestroyShieldAdd()
    {
        yield return new WaitForSeconds(10f);

        GO_shieldAdd.gameObject.SetActive(false);
        B_shieldIsUp = false;
        I_numberOfFlicks = 0;
        B_blueShieldIsVisible = true;
    }

    IEnumerator FlickOnDestroy()
    {
        yield return new WaitForSeconds(9f);

        B_blueShieldDestroyFlick = true;
        B_blueShieldIsVisible1 = false;
    }

    public void ShieldUpButtonPress()
    {
        B_blueShieldDestroyFlick = false;
        B_shieldUp.gameObject.SetActive(false);
        GO_shieldAdd.gameObject.SetActive(true);
        StartCoroutine(DestroyShieldAdd());
        StartCoroutine(FlickOnDestroy());
        I_shieldUp.fillAmount = 0f;
        F_power = 0f;
        B_shieldIsUp = true;
    }

    void SpawnPopupText()
    {
        Text T_newPopUpText = Instantiate(T_popupScoreText, T_popupScoreText.transform.position, Quaternion.identity);
        T_newPopUpText.transform.SetParent(C_mainCanvas.transform, false);
        T_newPopUpText.text = "+" + I_scoreMultiplayer.ToString();
        A_popupScoreAnimator.Play("PopUp");
        Destroy(T_newPopUpText.gameObject, 0.8f);
    }

    void NewBest()
    {
        if (!B_newBestAnimation)
        {
            Text T_newBestScoreText = Instantiate(T_newBestScore, T_newBestScore.transform.position, Quaternion.identity);
            T_newBestScoreText.transform.SetParent(C_mainCanvas.transform, false);
            A_newBestScoreAnimator.Play("newbestscoreanim");
            Destroy(T_newBestScoreText.gameObject, 1.6f);

            B_newBestAnimation = true;
        }
    }

    void FillBlueShieldLine()
    {
        if (!B_shieldIsUp)
        {
            if (F_power < 100f)
            {
                F_power += 5f;
            }

            if (F_power == 100f)
            {
                B_shieldUp.gameObject.SetActive(true);
                A_powerUpButtonAnimator.Play("PowerUpButton");
            }

            I_shieldUp.fillAmount = F_power / 100f;
        }
    }

    void AddScoreMultiplier()
    {
        if ((I_combo >= 10 && I_combo % 8 == 0 && I_scoreMultiplayer < 5) || I_combo == 10)
        {
            I_scoreMultiplayer++;

            if (I_scoreMultiplayer >= 2)
            {
                A_scoreMultiplierAnimator.Play("MultiplierUp");
            }
        }

        if (I_combo < 10)
        {
            I_scoreMultiplayer = 1;
        }

        if (I_scoreMultiplayer > 1)
        {
            T_multiplierText.gameObject.SetActive(true);
            T_multiplierText.text = "X" + I_scoreMultiplayer.ToString();
            A_scoreMultiplierAnimator.Play("MultiplayerText");
        }
    }

    IEnumerator SpeedUpTextDestroy()
    {
        yield return new WaitForSeconds(3.5f);

        T_speedUpText.text = "";
    }

    void SpeedUp()
    {
        if (!B_speedUp && I_score >= 25 && I_score < 100)
        {
            EnemySpawn.F_enemySpeed = 2.9f;
            EnemySpawn.F_timeToSpawnEnemy = 1.1f;

            if (ShieldMovement.F_rotationSpeed > 0f)
            {
                ShieldMovement.F_rotationSpeed = 180f;
            }
            else
            {
                ShieldMovement.F_rotationSpeed = -180f;
            }

            T_speedUpText.text = "shall we speed things up?";
            StartCoroutine(SpeedUpTextDestroy());

            B_speedUp = true;
        }
        else if (!B_speedUp1 && I_score >= 100 && I_score < 300)
        {
            EnemySpawn.F_enemySpeed = 3.35f;
            EnemySpawn.F_timeToSpawnEnemy = 0.75f;

            if (ShieldMovement.F_rotationSpeed > 0f)
            {
                ShieldMovement.F_rotationSpeed = 220f;
            }
            else
            {
                ShieldMovement.F_rotationSpeed = -220f;
            }

            T_speedUpText.text = "ready to get going?";
            StartCoroutine(SpeedUpTextDestroy());

            B_speedUp1 = true;
        }
        else if (!B_speedUp2 && I_score >= 300 && I_score <= 600)
        {
            EnemySpawn.F_enemySpeed = 3.6f;
            EnemySpawn.F_timeToSpawnEnemy = 0.55f;

            if (ShieldMovement.F_rotationSpeed > 0f)
            {
                ShieldMovement.F_rotationSpeed = 285f;
            }
            else
            {
                ShieldMovement.F_rotationSpeed = -285f;
            }

            T_speedUpText.text = "getting bored?";
            StartCoroutine(SpeedUpTextDestroy());

            B_speedUp2 = true;
        }
        else if (!B_speedUp3 && I_score >= 600)
        {
            EnemySpawn.F_enemySpeed = 3.75f;
            EnemySpawn.F_timeToSpawnEnemy = 0.42f;

            if (ShieldMovement.F_rotationSpeed > 0f)
            {
                ShieldMovement.F_rotationSpeed = 355f;
            }
            else
            {
                ShieldMovement.F_rotationSpeed = -355f;
            }

            T_speedUpText.text = "wanna go faster?";
            StartCoroutine(SpeedUpTextDestroy());

            B_speedUp3 = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            //Camera.main.GetComponent<AudioSource>().Play();

            GameObject GO_newDestroyEffect = Instantiate(GO_enemyDestroyEffect, collision.transform.position, collision.transform.rotation);
            Destroy(GO_newDestroyEffect, 1f);

            CameraShaker.Instance.ShakeOnce(2f, 15f, 0.2f, 0.3f);
            A_scoreAnimator.Play("ScoreAnim");

            if (!B_shieldIsUp)
            {
                GameObject GO_newHitAnimation = Instantiate(GO_shieldHitPrefab, transform.position, transform.rotation);
                GO_newHitAnimation.transform.SetParent(transform);
                GO_newHitAnimation.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
                A_shieldHitAnimator.Play("ShieldHitAnim");
                Destroy(GO_newHitAnimation, 0.7f);
            }

            FillBlueShieldLine();

            I_combo++;

            AddScoreMultiplier();

            SpawnPopupText();

            if (!StartGame.B_constantEasy && !StartGame.B_constantNormal && !StartGame.B_constantHard && !StartGame.B_constantExtreme)
            {
                SpeedUp();
            }

            I_score += I_scoreMultiplayer;
            T_score.text = I_score.ToString();

            if (I_score > PlayerPrefs.GetInt(S_bestScoreLocation))
            {
                NewBest();
                GO_bestScore.GetComponent<Text>().text = "Best:" + I_score.ToString();
                PlayerPrefs.SetInt(S_bestScoreLocation, I_score);
            }
        }
    }
}
