using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    public Button B_sound, B_vibration;
    public Sprite S_sound, S_noSound, S_vbration, S_noVibration;

    public static bool B_soundIsOn = true, B_vibrationIsOn = true;

    void Start()
    {
        if (PlayerPrefs.GetString("Sound", "") == "y")
        {
            B_soundIsOn = true;
        }
        else if (PlayerPrefs.GetString("Sound", "") == "n")
        {
            B_soundIsOn = false;
        }

        if (B_soundIsOn)
        {
            B_sound.GetComponent<Image>().sprite = S_sound;
        }
        else
        {
            B_sound.GetComponent<Image>().sprite = S_noSound;
        }

        if (PlayerPrefs.GetString("Vibrate", "") == "y")
        {
            B_vibrationIsOn = true;
        }
        else if (PlayerPrefs.GetString("Vibrate", "") == "n")
        {
            B_vibrationIsOn = false;
        }

        if (B_vibrationIsOn)
        {
            B_vibration.GetComponent<Image>().sprite = S_vbration;
        }
        else
        {
            B_vibration.GetComponent<Image>().sprite = S_noVibration;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SoundOnOff()
    {
        B_soundIsOn = !B_soundIsOn;

        string S_soundEnabled;

        if (B_soundIsOn)
        {
            B_sound.GetComponent<Image>().sprite = S_sound;
            S_soundEnabled = "y";
        }
        else
        {
            B_sound.GetComponent<Image>().sprite = S_noSound;
            S_soundEnabled = "n";
        }

        PlayerPrefs.SetString("Sound", S_soundEnabled);
    }

    public void VibrationOnOff()
    {
        B_vibrationIsOn = !B_vibrationIsOn;

        string S_vibrationEnabled;

        if (B_vibrationIsOn)
        {
            B_vibration.GetComponent<Image>().sprite = S_vbration;
            S_vibrationEnabled = "y";
        }
        else
        {
            B_vibration.GetComponent<Image>().sprite = S_noVibration;
            S_vibrationEnabled = "n";
        }

        PlayerPrefs.SetString("Vibrate", S_vibrationEnabled);
    }
}
