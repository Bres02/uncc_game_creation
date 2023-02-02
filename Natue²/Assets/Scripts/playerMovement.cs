using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    public float upGravity;
    public float downGravity;
    private Rigidbody2D body;
    private bool grounded;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
   
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, body.velocity.y);
        
        //flips the model when direction changes
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3 ((float)-0.25, (float)0.25, (float)0.25);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3((float)0.25, (float)0.25, (float)0.25);
        }
        // calls the jump function
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        // changes the player's gravity based on its current vertical velocity
        if (body.velocity.y > 0)
        {
            body.gravityScale = upGravity;
        }
        else
        {
            body.gravityScale = downGravity;
        }
    }
    // controls for player jumping
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // makes sure the player is touching the ground
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
