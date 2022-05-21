using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementFactor = 0.1f;
    [SerializeField] float jumpPower = 1f;
    [SerializeField] int jumpCounter = 1;
    [SerializeField] int howManyJump = 4;

    
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }


    void Update()
    {
        MovementHorizontal();
        MovementJump();
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

    private void MovementHorizontal()
    {
        float movement = Input.GetAxis("Horizontal");
        
        float movementPower = movement * movementFactor * Time.deltaTime;
        
        transform.position = transform.position + new Vector3(movementPower, 0, 0);
    }

   

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground")
        {
            jumpCounter = howManyJump;
        }
    }
}
