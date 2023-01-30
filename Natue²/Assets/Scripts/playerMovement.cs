using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    public bool grounded;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
   
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed, body.velocity.y);
       
        
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3 ((float)-0.25, (float)0.25, (float)0.25);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3((float)0.25, (float)0.25, (float)0.25);
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
