using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject playerCanvas;
    [SerializeField] GameObject player;
    PlayerHealth playerHealth;
    GameStage gameStage;

    public int stageInfo;

    void Start()
    {   
        
        gameStage = FindObjectOfType<GameStage>();
        stageInfo = gameStage.GetDeathCount(); 
        playerHealth = player.GetComponent<PlayerHealth>();
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
        }
    }
    
    void Update()
    {
        healthBar.value = playerHealth.GetHealth();   
    }

    
}
