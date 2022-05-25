using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class DeathInfo : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;

    string message;
    GameStage gameStage;
    // Start is called before the first frame update
    void Start()
    {
        gameStage = FindObjectOfType<GameStage>();    
    }

    
    void Update()
    {
        WriteText();    
    }

    private void WriteText()
    {
        int stage = gameStage.GetDeathCount();
        switch(stage)
        {
            case 1:
            message = "Sorry its my fault\ni forgat to add a health bar and score area\n ";
            break;

            case 2:
            message = "Lets increase your damage. maybe you can succeded this time";
            break;

            case 3:
            message = "I will give you additional jump right 4 times";
            break;

            case 4:
            message = "deneme bir ki";
            break;

            case 5:
            message = "deneme deneme deneme";
            break;
        }

        text.text = message;
    }

    
}
