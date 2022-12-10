using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelister_movement : MonoBehaviour
{

    public float speed_X = 0f;
    public float movingSpeed = 1f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {   
            speed_X = -0.01f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            speed_X = 0.01f;
        }
        
        //transform.Translate(speed_X, speed_Y, 0);

        Vector3 target = new Vector3() 
        {
            x=transform.position.x+speed_X, y=transform.position.y, z=transform.position.z
        };

        Vector3 pos = Vector3.Lerp(transform.position, target, movingSpeed * Time.deltaTime);
        transform.position = target;
        speed_X=0f;
    }
}