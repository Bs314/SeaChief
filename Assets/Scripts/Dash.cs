using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Dash : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;
    
    [SerializeField] float dashSpeed = 5f;
    [SerializeField] float dashTime = 0.5f;

    Animator animator;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ProcessDash();
        }
        
    }

    private void ProcessDash()
    {
        float dashTimeold = dashTime;
        animator.SetTrigger("dash");
        rb.gravityScale = 0;
        while(dashTime>0)
        {

            dashTime -= Time.deltaTime;
            
            float dash = transform.localScale.x * dashSpeed;
            //transform.Translate(new Vector3(dash,0,0));
            rb.velocity = new Vector2(dash,0);
            
            
        }
        while(rb.velocity.x!=0)
        {
            
        }
        rb.gravityScale = 1;
        dashTime = dashTimeold;
        
    }
}
