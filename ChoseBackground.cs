using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoseBackground : MonoBehaviour
{
    public Sprite[] S_backgroundArray = new Sprite[9];
    public GameObject GO_background;

    void Start()
    {
        int I_randomBackground = Random.Range(0, 9);

        GO_background.GetComponent<SpriteRenderer>().sprite = S_backgroundArray[I_randomBackground];
    }
}
