using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject GO_mods, GO_mainMenu, GO_normal, GO_constant, GO_back;
    public Text T_normalC, T_normalNH, T_constantE, T_constantN, T_constantH, T_constantEX, T_gamesPlayed;

    private GameObject GO_shield, GO_bestScore;

    public static bool B_normalClassic = false, B_normalNoHeal = false, B_gameStarted = false;
    public static bool B_constantEasy = false, B_constantNormal = false, B_constantHard = false, B_constantExtreme = false;

    private int I_menuNumber = 0, I_gamesPlayed = 0;

    void Start()
    {
        I_gamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0);
        T_gamesPlayed.text = I_gamesPlayed.ToString();

        T_normalC.text = "Best:" + PlayerPrefs.GetInt("BestNC").ToString();
        T_normalNH.text = "Best:" + PlayerPrefs.GetInt("BestNNH").ToString();
        T_constantE.text = "Best:" + PlayerPrefs.GetInt("BestCE").ToString();
        T_constantN.text = "Best:" + PlayerPrefs.GetInt("BestCN").ToString();
        T_constantH.text = "Best:" + PlayerPrefs.GetInt("BestCH").ToString();
        T_constantEX.text = "Best:" + PlayerPrefs.GetInt("BestCEX").ToString();
    }

    void ActivateScripts()
    {
        if (B_normalClassic)
        {
            DestroyEnemy.S_bestScoreLocation = "BestNC";
        }
        else if (B_normalNoHeal)
        {
            DestroyEnemy.S_bestScoreLocation = "BestNNH";
        }
        else if (B_constantEasy)
        {
            DestroyEnemy.S_bestScoreLocation = "BestCE";
        }
        else if (B_constantNormal)
        {
            DestroyEnemy.S_bestScoreLocation = "BestCN";
        }
        else if (B_constantHard)
        {
            DestroyEnemy.S_bestScoreLocation = "BestCH";
        }
        else if (B_constantExtreme)
        {
            DestroyEnemy.S_bestScoreLocation = "BestCEX";
        }

        B_gameStarted = true;

        SceneManager.LoadScene(1);
        I_gamesPlayed++;
        PlayerPrefs.SetInt("GamesPlayed", I_gamesPlayed);
    }

    public void BackButton()
    {
        I_menuNumber--;

        if (I_menuNumber == 0)
        {
            GO_back.gameObject.SetActive(false);
            GO_mods.gameObject.SetActive(false);
            GO_mainMenu.gameObject.SetActive(true);
        }
        else if (I_menuNumber == 1)
        {
            GO_normal.gameObject.SetActive(false);
            GO_constant.gameObject.SetActive(false);
            GO_mods.gameObject.SetActive(true);
        }
    }

    public void ButtonStart()
    {
        GO_mainMenu.gameObject.SetActive(false);
        GO_mods.gameObject.SetActive(true);
        GO_back.gameObject.SetActive(true);

        I_menuNumber++;
    }

    public void ButtonNormal()
    {
        GO_mods.gameObject.SetActive(false);
        GO_normal.gameObject.SetActive(true);

        I_menuNumber++;
    }

    public void ButtonConstant()
    {
        GO_mods.gameObject.SetActive(false);
        GO_constant.gameObject.SetActive(true);

        I_menuNumber++;
    }

    public void NormalClassic()
    {
        B_normalClassic = true;

        ActivateScripts();
    }

    public void NormalNoHeal()
    {
        B_normalNoHeal = true;

        ActivateScripts();
    }

    public void ConstantEasy()
    {
        B_constantEasy = true;

        ActivateScripts();
    }

    public void ConstantNormal()
    {
        B_constantNormal = true;

        ActivateScripts();
    }

    public void ConstantHard()
    {
        B_constantHard = true;

        ActivateScripts();
    }

    public void ConstantExtreme()
    {
        B_constantExtreme = true;

        ActivateScripts();
    }
}
