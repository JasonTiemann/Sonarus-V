using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RedEnemy : EnemyModel
{
    [Header("RedEnemy-Specific Vars")]
    public List<Vector2> positions;
    public float pauseFor = 1f;

    private int currentPosition = 0;
    private float moveTime = 0f;
    //public override void Start() {
    //    base.Start();
    //}
    private bool hasWaited = false;
    public override void Update(){
        base.Update();

        if (!moving) {
            if (!hasWaited){
                moveTime = Time.time + pauseFor;
                hasWaited = true;
            }else if (Time.time > moveTime){
                currentPosition = (currentPosition + 1) % positions.Count;
                MoveTo(positions[currentPosition], speed);
                hasWaited = false;
            }
        }
    }
}
