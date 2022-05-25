using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject playerCanvas;
    PlayerMovement playerMovement;
    GameStage gameStage;

    public int stageInfo;

    void Start()
    {   
        
        gameStage = FindObjectOfType<GameStage>();
        stageInfo = gameStage.GetDeathCount(); 
        playerMovement = FindObjectOfType<PlayerMovement>();   
        healthBar.maxValue = playerMovement.GetHealth();    
    }

    // Update is called once per frame
    void Update()
    {
        ShowHideBar();
    }

    private void ShowHideBar()
    {
        
        if(stageInfo == 0)
        {
            playerCanvas.SetActive(false);
            
        }
        else
        {
            playerCanvas.SetActive(true);   
            healthBar.value = playerMovement.GetHealth();
        }
    }
}
