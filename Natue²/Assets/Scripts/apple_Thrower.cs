using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apple_Thrower : MonoBehaviour
{
    public float min = 2f;
    public float max = 3f;
    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        min = transform.position.x;
        max = transform.position.x + 20;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            ThrowApple();
        }

        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);
    }
    void ThrowApple()
    {

    }
}
