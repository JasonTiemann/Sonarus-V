using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform player;
    public Vector2 aspectRatio = new Vector2(16,9);
    public float speed = 100f;

    public Vector2 currentMapChunk = new Vector2(0,0);

    private bool isMoving = false;
    void Update(){
        if (isMoving){
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(currentMapChunk.x * aspectRatio.x, currentMapChunk.y * aspectRatio.y, -10), speed * Time.deltaTime);

            if (this.transform.position.x == currentMapChunk.x * aspectRatio.x && this.transform.position.y == currentMapChunk.y * aspectRatio.y)
                isMoving = false;

        }
        else {
            isMoving = true;
            if (player.position.x > this.transform.position.x + (aspectRatio.x / 2f))
                currentMapChunk.x++;
            else if (player.position.x < this.transform.position.x - (aspectRatio.x / 2f))
                currentMapChunk.x--;
            else if (player.position.y > this.transform.position.y + (aspectRatio.y / 2f))
                currentMapChunk.y++;
            else if (player.position.y < this.transform.position.y - (aspectRatio.y / 2f))
                currentMapChunk.y--;
            else
                isMoving = false;
        }

    }
}
