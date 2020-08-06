using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedTape : MonoBehaviour
{
    public tapeSpawner tape;
    public NotificationController notice;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            tape.SetTapeIsEnabled(true);
            notice.Show($"You found a {tape.tapeColor.ToString()} tape!", 3f);
            Destroy(this.gameObject);
        }
    }
}
