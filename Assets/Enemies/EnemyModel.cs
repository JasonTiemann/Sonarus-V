using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    [Header("Base Vars")]
    public int health;
    public TapeColor enemyColor;
    public float speed = 50f;
    public Vector2 mapChunk;
    public Vector2 aspectRatio = new Vector2(16, 9);
    public EnemyGroupController notifier;
    public EnemyAttackCheck attackBox;
    public float attackWait = 1f;

    internal bool moving = false;
    internal Vector2 moveTo;
    internal bool isDead = false;
    internal Rigidbody2D enemyRB;
    internal Animator animator;
    internal bool isSoothed;
    internal bool isAngered;
    internal Vector2 direction = new Vector2(0, 0);
    internal float lastAttack = 0f;
    internal bool isPaused = false;
    internal float oldSpeed = 0f;

    public virtual void InheretedStart() { }
    public virtual void InheretedUpdate() { }
    public void Start(){
        animator = this.GetComponent<Animator>();
        enemyRB = this.GetComponent<Rigidbody2D>();
        notifier.RegisterEnemy(this, mapChunk);

        InheretedStart();
    }

    public void Update(){
        if (isPaused) return;

        if (moving && !isSoothed && !attackBox.playerInRange) {
            var targetPos = isAngered ? (Vector2)notifier.playerPosition.position : moveTo;

            direction.x = (enemyRB.position.x == targetPos.x) ? 0 : ((enemyRB.position.x > targetPos.x) ? -1 : 1);
            //direction.y = (enemyRB.position.y == targetPos.y) ? 0 : ((enemyRB.position.y > targetPos.y) ? -1 : 1); // Only If we go to 4-Directional

            if (direction.x > 0)
                this.transform.localScale = new Vector2(-1, this.transform.localScale.y);
            else if (direction.x < 0)
                this.transform.localScale = new Vector2(1, this.transform.localScale.y);

            enemyRB.position = Vector2.MoveTowards(enemyRB.position, targetPos, speed * Time.deltaTime);

            if (!isAngered && enemyRB.position.Equals(targetPos)) {
                moving = false;
                animator.SetBool("Walking", false);
            }
        }
        if (!isSoothed && attackBox.playerInRange && Time.time > lastAttack + attackWait) {
            notifier.playerControl.Damage(1);
            lastAttack = Time.time;
        }

        InheretedUpdate();
    }

    internal void MoveTo(Vector2 position, float speed) {
        moving = true;
        var leashedPosition = new Vector2(Mathf.Clamp(position.x, mapChunk.x - (aspectRatio.x/2), mapChunk.x + (aspectRatio.x/2)), Mathf.Clamp(position.y, mapChunk.y - (aspectRatio.y/2), mapChunk.y + (aspectRatio.y/2)));
        moveTo = leashedPosition;
        this.speed = speed;
    }

    public void SetSoothed(bool sootheState) {
        Debug.Log($"{(sootheState ? "" : "Un")}Soothed");
        if (isAngered && sootheState)
            isAngered = false;
        isSoothed = sootheState;
        animator.SetBool("isSoothed", sootheState);
    }

    public void SetAngered(bool angerState) {
        Debug.Log($"{(angerState ? "" : "Un" )}Angered");
        if (isSoothed && isAngered)
            isSoothed = false;

        isAngered = angerState;
        animator.SetBool("isAngered", angerState);
    }

    public void Damage() {
        if (!isDead) {
            health--;
            animator.SetTrigger("Damaged");
            animator.SetInteger("Health", health);

            if (health == 0){
                moving = false;
                isDead = true;
            }
        }
    }

    public void EnemyDie() {
        notifier.DestroyEnemy(this, mapChunk);
        Destroy(this.gameObject);
    }

    public void Pause() {
        isPaused = true;
        oldSpeed = animator.speed;
        animator.speed = 0;
    }

    public void Resume() {
        animator.speed = animator.speed;
        isPaused = false;
    }
}
