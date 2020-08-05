using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCheck : MonoBehaviour
{
    public Vector2 leftAttackPosition;
    public Vector2 rightAttackPosition;

    internal bool playerInRange;
    private BoxCollider2D attackCollider;
    private void Start()
    {
        attackCollider = this.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            playerInRange = false;
    }

    public void SetAttackPosition(AttackPosition pos)
    {
        switch (pos)
        {
            case AttackPosition.left:
                attackCollider.offset = leftAttackPosition;
                break;
            case AttackPosition.right:
                attackCollider.offset = rightAttackPosition;
                break;
        }
    }
}
