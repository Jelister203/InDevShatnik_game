using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    
    public Animator anim;
    public int MaxHealth = 100;
    private int currenthealth;
    void Start()
    {
        currenthealth = MaxHealth;
       
    }

    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        anim.SetTrigger("Hurt");
        if (currenthealth <=0)
        {
            anim.SetBool("IsDead", true);
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
        }
    }

    

   
}
