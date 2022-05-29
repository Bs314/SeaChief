using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Dash : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    [SerializeField] float dashTime;
    [SerializeField] float startDashTime = 5f;
    [SerializeField] float dashSpeed = 0.5f;
    [SerializeField] AudioClip dashSFX;
    [SerializeField] ParticleSystem dashParticle;
    

    Animator animator;
    
    bool isDashing = false;
   
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
       
        dashTime = startDashTime;
    }

    void Update()
    {
        ProcessDash();
    }

    private void ProcessDash()
    {
        if (!isDashing)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.C))
            {
                dashParticle.Play();
                animator.SetTrigger("dash");
                AudioSource.PlayClipAtPoint(dashSFX, transform.position);
                isDashing = true;
                rb.gravityScale = 0;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                rb.gravityScale = 1;
                isDashing = false;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;

            }
            else
            {
                dashTime -= Time.deltaTime;
                float dash = transform.localScale.x * dashSpeed;
                rb.velocity = new Vector2(dash, 0);
            }
        }
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }
}
