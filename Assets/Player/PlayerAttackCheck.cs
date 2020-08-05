using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackCheck : MonoBehaviour
{
    public Vector2 leftAttackPosition;
    public Vector2 rightAttackPosition;

    internal bool enemyInRange;
    internal EnemyModel enemy;
    private BoxCollider2D attackCollider;
    private void Start(){
        attackCollider = this.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemyInRange = true;
            enemy = col.gameObject.GetComponent<EnemyModel>();


        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemyInRange = false;
            enemy = null;
        }
    }

    public void SetAttackPosition(AttackPosition pos) {
        switch (pos) {
            case AttackPosition.left:
                attackCollider.offset = leftAttackPosition;
                break;
            case AttackPosition.right:
                attackCollider.offset = rightAttackPosition;
                break;
        }
    }
}
