using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        WallCheckRadiusUp = WallCheckUp.GetComponent<CircleCollider2D>().radius;
        WallCheckRadiusDown = WallCheckDown.GetComponent<CircleCollider2D>().radius;
        gravityDef = rb.gravityScale;
        realspeed = speed;
    }

    void Update()
    {
        Walk();
        Reflect();
        Jump();
        Run();
        CheckingGround();
        CheckingWall();
        MoveOnWall();
        WallJump();
        Dash();
        MaxSpeed();
        //Menu();
        MilliAttack();
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
        if (blockMoveX)
        {
            moveVector.x = 0;
        }
        else
        {
            moveVector.x = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveVector.x * realspeed, rb.velocity.y);
        }
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
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
        if ((!blockMoveX)&&Input.GetKey(KeyCode.Space))
        {
            if (!onWall && OnGround  ) { jumpControl = true; }
        }
        else { jumpControl = false; }

        if (jumpControl)
        {
            if (jumpIteration++ < jumpValueIteration)
            {
                anim.StopPlayback();
                anim.Play("Jump");
                rb.AddForce(Vector2.up * jumpForce / jumpIteration);
            }
        }
        else { jumpIteration = 0; }
    }

    public bool OnGround;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;


    public int DashImpulse = 10000;
    void Dash()
    {
        if ((!blockMoveX)&&(!onWall) && (Input.GetKeyDown(KeyCode.LeftControl)))
        {
            anim.StopPlayback();
            anim.Play("Dash");

            rb.velocity = new Vector2(0, 0);
            if (!faceRight) { rb.AddForce(Vector2.left * DashImpulse ); }
            else { rb.AddForce(Vector2.right * DashImpulse ); } 
        }
    }

    public bool onWall;
    public LayerMask Wall;
    public Transform WallCheckUp;
    public Transform WallCheckDown;
    public float WallCheckRayDistance = 1f;
    private float WallCheckRadiusUp;
    private float WallCheckRadiusDown;

    void CheckingWall()
    {
        onWall = (Physics2D.OverlapCircle(WallCheckUp.position, WallCheckRadiusUp, Wall) && Physics2D.OverlapCircle(WallCheckDown.position, WallCheckRadiusDown, Wall));
        anim.SetBool("onWall", onWall);
    }

    public float upDownSpeed = 4f;
    public float slideSpeed = 0;
    private float gravityDef;
    void MoveOnWall()
    {
        if (onWall && !OnGround)
        {
            moveVector.y = Input.GetAxisRaw("Vertical");
            anim.SetFloat("UpDown", moveVector.y);
            if (!blockMoveX)
            {
                anim.StopPlayback();
                anim.Play("UpDown");

                if (moveVector.y == 0)
                { 
                    rb.gravityScale = 0;
                    rb.velocity = new Vector2(0, slideSpeed);
                }
            }
            
            if (moveVector.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveVector.y * upDownSpeed / 2);
            }
            else if (moveVector.y < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveVector.y * upDownSpeed);
            }
            
        }
        else if (!OnGround && !onWall) { rb.gravityScale = gravityDef; }
    }

    private bool blockMoveX;
    public float jumpWallTime = 0.5f;
    private float timerJumpWall;
    public Vector2 jumpAngle = new Vector2(3.5f, 10);
    void WallJump()
    {
        if (onWall && !OnGround && Input.GetKeyDown(KeyCode.Space))
        {
            blockMoveX = true;
            moveVector.x = 0;
            anim.StopPlayback();
            anim.Play("Jump"); //������ Jump ����� WallJump
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
            rb.gravityScale = gravityDef;
            rb.velocity = new Vector2(0, 0);
            rb.velocity = new Vector2(transform.localScale.x * jumpAngle.x, jumpAngle.y);
        }
        if (blockMoveX && (timerJumpWall += Time.deltaTime) >= jumpWallTime)
        {
            if (onWall || OnGround || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetAxisRaw("Horizontal") != 0)
            {
                blockMoveX = false;
                timerJumpWall = 0;
            }
        }
    }
    
    public float PlayerSpeed;
    public int SpeedLimit = 10;
    public int SpeedLimiter = 2;

    void MaxSpeed()
    {
        PlayerSpeed = rb.velocity.magnitude;
        if (!blockMoveX)
        {
            if (PlayerSpeed > SpeedLimit) { rb.velocity = new Vector2(moveVector.x / SpeedLimiter, rb.velocity.y / SpeedLimiter); }
        }
            
    }
    /*
    void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    */

    public Transform MilliAttackArea;
    public float MilliRange = 0.5f;
    public LayerMask enemyLayers;
    public int AttackDamage = 40;
    void MilliAttack()
    {
        if (OnGround&& !onWall && Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("MilliAttack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(MilliAttackArea.position, MilliRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<enemy>().TakeDamage(AttackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (MilliAttackArea == null) return;
        Gizmos.DrawWireSphere(MilliAttackArea.position, MilliRange);
    }
}





