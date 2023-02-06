using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple_Thrower : MonoBehaviour
{
    [SerializeField] private GameObject apple;
    public float min = 2f;
    public float max = 3f;

    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.x;
        max = transform.position.x + 20;
        InvokeRepeating("ThrowApple",2.0f,4.0f);// throws an apple every x seconds
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);//moves back and forth
    }
    void ThrowApple()
    {
        Instantiate(apple, transform.position, Quaternion.identity);// creates the apple
    }


}
