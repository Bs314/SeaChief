using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementFactor = 0.1f;
    [SerializeField] float jumpPower = 1f;
    [SerializeField] int jumpCounter = 1;
    [SerializeField] int howManyJump = 4;
    [SerializeField] int health = 1;
    [SerializeField] int damage = 10;
    [SerializeField] Animator animator;
    [Header("Sound")]
    [SerializeField] AudioClip swosh;
    [SerializeField] AudioClip hurt;

    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayer;


    GameStage gameStage;
    BoxCollider2D boxCollider2D;
    Rigidbody2D rb;
    PlayerMovement playerMovement;
    Dash dash;

    float attackRate = 4f;
    float nextAttackTime = 0f;
    bool isPlayerLive = true;
    int stageInfo;
    void Start()
    {
        gameStage = FindObjectOfType<GameStage>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponentInChildren<BoxCollider2D>();
        playerMovement = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
        StageUpdates();

    }

    private void StageUpdates()
    {
        stageInfo = gameStage.GetDeathCount();
        switch (stageInfo)
        {
            case 0:
                dash.enabled = false;
                break;
                
            case 1:
                // increase health
                SetHealth(100);
                dash.enabled = false;

                break;

            case 2:
                SetHealth(100);
                SetDamage(50);
                dash.enabled = false;

                break;

            case 3:
                SetHealth(100);
                SetDamage(50);
                SetJump(2);
                dash.enabled = false;
                break;

            case 4:
                SetHealth(100);
                SetDamage(50);
                SetJump(3);
                dash.enabled = true;
                
                break;

            case 5:
                SetHealth(100);
                SetDamage(50);
                SetJump(4);
                break;

            default:
                break;
        }
    }

    void Update()
    {
        MovementHorizontal();
        MovementJump();
        JumpAnimation();
        PlayerAttack();

    }

    private void PlayerAttack()
    {
        if (Time.time > nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }

        }

    }

    private void Attack()
    {
        // play attack animation
        animator.SetTrigger("attack");
        AudioSource.PlayClipAtPoint(swosh, transform.position);
        // detect all enemy in range
        Invoke("Hit", 0.2f);
    }

    private void Hit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponentInParent<EnemyMovement>().Hit(damage);

        }
    }

    public void SetDamage(int value)
    {
        damage = value;
    }


    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void MovementJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {


            if (jumpCounter > 0)
            {
                //rb.AddForce(new Vector2(0,jumpPower));
                rb.velocity = new Vector2(0, jumpPower);
                jumpCounter -= 1;
            }

        }

    }

    private void JumpAnimation()
    {
        if (!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("grounded", false);

            float verticalVelocity = rb.velocity.y;

            if (verticalVelocity > 0)
            {
                animator.SetBool("isVerticalVelocityPositive", true);
            }
            else
            {
                animator.SetBool("isVerticalVelocityPositive", false);
            }
        }
        else
        {
            animator.SetBool("grounded", true);
        }
    }

    private void MovementHorizontal()
    {
        float movement = Input.GetAxis("Horizontal");
        FlipSprite(movement);
        float movementPower = movement * movementFactor * Time.deltaTime;

        //rb.velocity = new Vector2(movementPower, 0);
        transform.position = transform.position + new Vector3(movementPower, 0, 0);

        if (movement != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }

    private void FlipSprite(float value)
    {
        bool isPlayerHasHorizontalSpeed = Mathf.Abs(value) > Mathf.Epsilon;

        if (isPlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(value), 1f);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {

            jumpCounter = howManyJump;
        }

        if (other.gameObject.tag == "Enemy")
        {
            //die or take damage
            int damage = other.gameObject.GetComponent<EnemyMovement>().GetDamage();
            TakeDamage(damage);
            if (health < 1)
            {
                Die();
            }
            else
            {
                animator.SetTrigger("hurt");
            }

        }

    }

    void SetJump(int value)
    {
        howManyJump = value;
    }

    private void TakeDamage(int damage)
    {

        HurtKick();
        health -= damage;
    }

    private void HurtKick()
    {
        if (isPlayerLive)
        {
            AudioSource.PlayClipAtPoint(hurt, transform.position);
            rb.velocity = new Vector2(-transform.localScale.x * 2, 2.5f);
        }

    }

    private void Die()
    {
        if (isPlayerLive)
        {
            gameStage.IncDeathCount();
            isPlayerLive = false;
            animator.SetTrigger("die");
            playerMovement.enabled = false;
            //rb.gravityScale = 0;
            //boxCollider2D.enabled = false;
            StartCoroutine(LoadDeathMenu());
        }


    }

    IEnumerator LoadDeathMenu()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("DeathScene");

    }

    public bool getPlayerLiveStatus()
    {
        return isPlayerLive;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int setValue)
    {
        health = setValue;
    }
}
