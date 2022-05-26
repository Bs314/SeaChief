using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float xThrowPower = 10f;
    [SerializeField] float yThrowPower = 10f;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] Animator animator;
    [SerializeField] int scoreValue = 20;
    [SerializeField] int health = 100;
    [SerializeField] int damage = 10;
    [SerializeField] float hitBack = 1f;
    [SerializeField] ParticleSystem particle;
    

    Rigidbody2D rb;
    ShakeCamera shakeCamera;
    ScoreKeeper scoreKeeper;

    float xPos;
    float playerXPos;
    int direction = 0;
    bool isTouchedEnemy = false;
    bool isShaked = false;
    bool isEnemyAlive = true;
    bool isPlayerLive;


    void Start()
    {
        shakeCamera = FindObjectOfType<ShakeCamera>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        ThrowTheEnemy();
    }

    private void ThrowTheEnemy()
    {
        rb = GetComponent<Rigidbody2D>();
        float initialXPos = transform.position.x;
        if (initialXPos < 0)
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
        if (isEnemyAlive)
        {
            MoveEnemy();
        }
        else
        {

        }

    }

    void MoveEnemy()
    {
        float movementPower = movementSpeed * Time.deltaTime;
        if (xPos < playerXPos)
        {

            transform.position = transform.position + new Vector3(movementPower * direction, 0, 0);
        }
        else
        {
            transform.position = transform.position + new Vector3(movementPower * direction, 0, 0);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            xPos = transform.position.x;
            playerXPos = other.transform.position.x;

            if (xPos < playerXPos)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        direction = 0;
        //animator.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            if (!isTouchedEnemy && !isShaked)
            {
                shakeCamera.CameraShake();
                isShaked = true;
            }

        }

        if (other.gameObject.tag == "Enemy")
        {
            isTouchedEnemy = true;
        }
    }

    public void Hit(int damage)
    {
        health -= damage;

        if (health < 1)
        {
            if(isEnemyAlive)
            {
                Die();
            }
            
        }

        // hit effect
        particle.Play();
        
        HitEffect();

    }

    private void HitEffect()
    {

        if (xPos < playerXPos)
        {
            rb.velocity = new Vector2(-hitBack, 1.5f);
        }
        else
        {
            rb.velocity = new Vector2(hitBack, 1.5f);
        }
    }

    void Die()
    {
        isEnemyAlive = false;
        animator.SetTrigger("death");
        scoreKeeper.AddScore(scoreValue);
        gameObject.tag = "Dead";
        Destroy(gameObject, 2);

    }

    public int GetDamage()
    {
        return damage;
    }

}
