using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI scoreText;


    int score = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int value) 
    {
        score += value;    
    }


}
