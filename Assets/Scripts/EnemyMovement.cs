using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float xThrowPower = 10f; 
    [SerializeField] float yThrowPower = 10f;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] Animator animator;
    [SerializeField] int health = 100;



    Rigidbody2D rb;
    ShakeCamera shakeCamera;
    float xPos;
    float playerXPos;
    int direction = 0;
    bool isTouchedEnemy = false;
    bool isShaked = false;

    void Start()
    {
        
        shakeCamera = FindObjectOfType<ShakeCamera>();

        ThrowTheEnemy();
    }

    private void ThrowTheEnemy()
    {
        rb = GetComponent<Rigidbody2D>();
        float xPos = transform.position.x;
        if (xPos < 0)
        {
            rb.velocity = new Vector2(xThrowPower, yThrowPower);
        }
        else
        {
            rb.velocity = new Vector2(-xThrowPower, yThrowPower);
        }
    }

    void Update()
    {
        MoveEnemy();   
    }

    void MoveEnemy()
    {
        float movementPower = movementSpeed * Time.deltaTime;
        if(xPos<playerXPos)
            {
                transform.position = transform.position + new Vector3(movementPower*direction,0,0);   
            }
            else
            {
                transform.position = transform.position + new Vector3(movementPower*direction,0,0);    
            }
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player")
        {
            xPos = transform.position.x;
            playerXPos = other.transform.position.x;
            
            if(xPos<playerXPos)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
           // animator.enabled = true;
            
            
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        direction = 0;
        //animator.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground")
        {
            if(!isTouchedEnemy && !isShaked)
            {
                shakeCamera.CameraShake();
                isShaked = true;
            }
            
        }
        
        if(other.gameObject.tag == "Enemy")
        {
            isTouchedEnemy = true;
        }
    }

    public void Hit(int damage)
    {
        health -= damage;

        if(health < 1)
        {
            //Die();
        }
    }

    void Die()
    {

    }


}
