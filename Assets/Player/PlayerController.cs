using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour {
    public int speed = 5;
    public float attackPause = .5f;
    public PlayerAttackCheck attackCheck;


    private Rigidbody2D player;
    private Animator animator;
    private Vector2 direction = new Vector2(0, 0);
    void Start() {
        player = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    private bool isPaused = false;
    private bool isWalking = false;
    private float lastAttack = 0f;
    private bool facingLeft = false;
    private float animatorSpeed = 0;
    void Update() {
        if (isPaused) return;

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");


        if (direction.x > 0 && facingLeft) {
            facingLeft = false;
            animator.SetBool("FacingLeft", false);
            attackCheck.SetAttackPosition(AttackPosition.right);
        } else if (direction.x < 0 && !facingLeft) {
            facingLeft = true;
            animator.SetBool("FacingLeft", true);
            attackCheck.SetAttackPosition(AttackPosition.left);
        }


        if ((direction.y != 0 || direction.x != 0) != isWalking) {
            isWalking = !isWalking;
            animator.SetBool("isWalking", isWalking);
        }

        if (Input.GetAxis("Attack") == 1 && lastAttack + attackPause < Time.time) {
            animator.SetTrigger("Attack");
            lastAttack = Time.time;
        }
    }

    private void FixedUpdate() {
        if (isPaused) return;

        player.velocity = direction * speed * Time.deltaTime;
    }

    public void PlayerAttack() {
        if (attackCheck.enemyInRange) {
            attackCheck.enemy.Damage();
        }
    }

    public void Damage(int damageAmount) {
        Debug.Log("Player Damaged");
        animator.SetTrigger("Damaged");
    }

    public void Pause() {
        isPaused = true;
        animatorSpeed = animator.speed;
        animator.speed = 0;
        player.velocity = Vector2.zero;
    }

    public void Resume() {
        animator.speed = animatorSpeed;
        isPaused = false;
    }
}
