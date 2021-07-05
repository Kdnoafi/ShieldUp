using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterFlicker : MonoBehaviour
{
    public Text[] T_letterArray = new Text[8];

    private int I_randomLetter;
    private float F_flickTime = 0f, F_maxTime = 0f, F_startTime = 0f;
    private bool B_letterIsChosen = false, B_letterIsOn = false;


    void FixedUpdate()
    {
        F_startTime += Time.deltaTime;

        if (F_startTime >= 5f)
        {
            B_letterIsChosen = true;
            I_randomLetter = Random.Range(0, 8);
            F_startTime = 0f;
        }

        if (B_letterIsChosen)
        {
            if (F_maxTime < 0.7f)
            {
                F_flickTime += Time.deltaTime;
                F_maxTime += Time.deltaTime;

                if (F_flickTime >= 0.05f)
                {
                    T_letterArray[I_randomLetter].gameObject.SetActive(B_letterIsOn);
                    F_flickTime = 0f;
                    B_letterIsOn = !B_letterIsOn;
                }
            }
            else
            {
                B_letterIsChosen = false;
                B_letterIsOn = false;
                F_maxTime = 0f;
                F_flickTime = 0f;
            }
        }
    }
}
