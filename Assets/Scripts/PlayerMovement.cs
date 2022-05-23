using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementFactor = 0.1f;
    [SerializeField] float jumpPower = 1f;
    [SerializeField] int jumpCounter = 1;
    [SerializeField] int howManyJump = 4;
    [SerializeField] Animator animator;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayer;

    BoxCollider2D boxCollider2D;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponentInChildren<BoxCollider2D>();
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
        
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // play attack animation
        animator.SetTrigger("attack");
        // detect all enemy in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);   

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponentInParent<EnemyMovement>().Hit(50);
            Debug.Log("we hit " + enemy.name);
        }
    }

    private void OnDrawGizmos() {
        if(attackPoint==null)return;

        Gizmos.DrawWireSphere(attackPoint.position,attackRange);    
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

    }
}
