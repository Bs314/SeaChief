using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



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
        Scene scene = SceneManager.GetActiveScene();
        if(scene.buildIndex != 3)
        {
            WriteText(); 
        }

    }

    private void WriteText()
    {
        
        int stage = gameStage.GetDeathCount();
        switch(stage)
        {
            case 1:
            message = "Sorry this is my fault\nI'm creating an area for you to follow the score\nIn the upper left corner\nI also give you 100 HP\n\nUse it wisely !";
            break;

            case 2:
            message = "It's clear that you need some strength\nDon't worry, I'm increasing your damage\nI hope you fight longer than you did last time";
            break;

            case 3:
            message = "Aren't they too many?\nI'm giving you 2 jumps for some more relief\nYou can use it to escape when they pile up around you";
            break;

            case 4:
            message = "Don't panic when enemies surround you\nI said I would help you. Now I'm giving you the DASH skill\nYou can use it as many times as you want\nby pressing the LEFT-ALT or C key\n\nAlso you can jump 3 times";
            break;

            case 5:
            message = "I gave you everything I could\nFor more, I need to take a few more courses\nUntil then, I believe you can reach the desired score\nwith what you have...\n\nYou can jum 4 times now..";
            break;

            default: 
            message = "Come on !!!\nYou can do it";
            break;

        }

        text.text = message;
    }

    
}
