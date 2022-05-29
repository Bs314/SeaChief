using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

        if(score >= 1000)
        {
            SceneManager.LoadScene("Success");
        }

    }

    public void AddScore(int value) 
    {
        score += value;    
    }


}
