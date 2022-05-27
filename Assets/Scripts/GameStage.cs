using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStage : MonoBehaviour
{
    
    [SerializeField] int deathCount = 0;
    
    bool dieBeforeScoreLimits = false;

    void Awake() {
        int numGameStage = FindObjectsOfType<GameStage>().Length;
        if(numGameStage>1)
        {
            Destroy(gameObject);
        }  
        else
        {
            DontDestroyOnLoad(gameObject);
        }  
    }


    void Start()
    {
        
    }

    
    void Update()
    {

    }

    public int GetDeathCount()
    {
        return deathCount;
    }

    public void IncDeathCount()
    {
        if(deathCount<6)deathCount++;
    }

    public void SetDieBeforeScoreLimits(bool setValue)
    {
        dieBeforeScoreLimits = setValue;
    }

    public bool GetDieBeforeScoreLimits()
    {
        return dieBeforeScoreLimits;
    }

}
