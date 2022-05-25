using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStage : MonoBehaviour
{
    
    [SerializeField] int deathCount = 0;
    

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
        deathCount++;
    }

}
