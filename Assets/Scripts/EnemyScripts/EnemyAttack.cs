using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Vector3 attackOffset;
    public float attackRange = 3f;
    public LayerMask attackMask;
    public int enemyDamage = 25;
    /*public void enemyAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerMove>().PlayerTakeDamage(enemyDamage);
        }
    }
    */

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
