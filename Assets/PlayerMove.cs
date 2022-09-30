using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        realspeed = speed;
    }

    void Update()
    {
        Walk();
        Reflect();
        Jump();
        Run();
        CheckingGround();
        
    }
    void CheckingGround()
    {
        OnGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
        anim.SetBool("onGround", OnGround);
    }

    public Vector2 moveVector;
    public bool faceRight = true;
    public int speed = 3;
    public int fastspeed = 6;
    private int realspeed;

    void Walk()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
        rb.velocity = new Vector2(moveVector.x * realspeed, rb.velocity.y);
    }

    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            realspeed = fastspeed;
        }
        else
        {
            realspeed = speed;
        }
    }

    public float jumpForce = 210f;
    private bool jumpControl;
    private int jumpIteration = 0;
    public int jumpValueIteration = 60;

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (OnGround) { jumpControl = true; }
        }
        else { jumpControl = false; }

        if (jumpControl)
        {
            if (jumpIteration++ < jumpValueIteration)
            {
                rb.AddForce(Vector2.up * jumpForce / jumpIteration);
            }
        }
        else { jumpIteration = 0; }
    }

    public bool OnGround;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;


}




