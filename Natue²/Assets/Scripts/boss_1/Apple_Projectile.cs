using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple_Projectile : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    private 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision){
      if (collision.gameObject.tag == "Ground")
      {
        //Physics2D.IgnoreCollision(ground.GetComponent<Collider2D>(), GetComponent<Collider2D>());
      }

    }
}
