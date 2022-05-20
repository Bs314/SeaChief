using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    
    [SerializeField] int jumpCounterSet = 1;
    PlayerMovement playerMovement;
    

    void Start()
    {
            
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            playerMovement.jumpCounterSet(jumpCounterSet);
        }
    }
}
