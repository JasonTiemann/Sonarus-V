using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedNote : MonoBehaviour
{
    public NoteSystem note;
    [TextArea(2, 25)]
    public string text;


    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            note.ShowNote(text);
            Destroy(this.gameObject);
        }
    }
}
