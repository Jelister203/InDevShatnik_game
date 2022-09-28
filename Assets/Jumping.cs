using UnityEngine;
using System.Collections;

public class Jumping : MonoBehaviour
{
    //переменная для установки макс. скорости персонажа
    public float maxSpeed = 10f; 
    //переменная для определения направления персонажа вправо/влево
    private bool isFacingRight = true;
    //ссылка на компонент анимаций
    private Animator anim;
    //находится ли персонаж на земле или в прыжке?
    private bool isGrounded = false;
    //ссылка на компонент Transform объекта
    //для определения соприкосновения с землей
    public Transform groundCheck;
    //радиус определения соприкосновения с землей
    private float groundRadius = 0.5f;
    //ссылка на слой, представляющий землю
    public LayerMask whatIsGround;

    /// <summary>
    /// Начальная инициализация
    /// </summary>
    private void Start()
    {
    }
    
    /// <summary>
    /// Выполняем действия в методе FixedUpdate, т. к. в компоненте Animator персонажа
    /// выставлено значение Animate Physics = true и анимация синхронизируется с расчетами физики
    /// </summary>
    private void FixedUpdate()
    {
        //определяем, на земле ли персонаж
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); 
        if (!isGrounded)
            return;
        //float move = Input.GetAxis("Horizontal");

        //GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        /*
        if(move > 0 && !isFacingRight)
            Flip();
        else if (move < 0 && isFacingRight)
            Flip();*/
    }

    private void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) 
        {   
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 600));
            
        }
    }

    /// <summary>
    /// Метод для смены направления движения персонажа и его зеркального отражения
    /// </summary>
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}