using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    PlayerHealth playerHealth;
    ScoreKeeper scoreKeeper;
    Dash dash;

    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();   
        dash = FindObjectOfType<Dash>(); 
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

   
    void Update()
    {
        CheatProcess();
    }

    private void CheatProcess()
    {
        //add score
        if (Input.GetKeyDown(KeyCode.O))
        {
            scoreKeeper.AddScore(200);
        }
        // eneabled dash ability
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (dash.enabled)
            {
                dash.enabled = false;
            }
            else
            {
                dash.enabled = true;
            }

        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            playerHealth.TakeDamage(100);
        }
    }
}
