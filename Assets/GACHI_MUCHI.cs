using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GACHI_MUCHI : MonoBehaviour
{

    float speed_X;
    float speed_Y;
    public float speed = 1f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            speed_Y = 0.01f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            speed_Y = -0.01f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            speed_X = -0.01f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            speed_X = 0.01f;
        }
        transform.Translate(speed_X, speed_Y, 0);
        speed_X = 0f;
        speed_Y = 0f;
    }
}
