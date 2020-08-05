using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapeDrag : MonoBehaviour
{
    public TapeColor tapeColor;
    public tapeSpawner originator;
    
    private Transform selfTransform;
    void Start()
    {
        selfTransform = this.GetComponent<Transform>();
    }


    void Update(){
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selfTransform.position = new Vector2(mousePos.x, mousePos.y);
    }

    void OnMouseDown(){
        originator.SetFullTape();
        DestroyTape();
    }

    public void DestroyTape(){
        Destroy(this.gameObject);
    }
}
