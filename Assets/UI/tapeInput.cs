using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tapeInput : MonoBehaviour
{
    public tapedeckController tapedeck;

    public Sprite emptyTape;
    public Sprite redTape;
    public Sprite greenTape;
    public Sprite blueTape;

    public TapeColor currentTape = TapeColor.empty;
    private Image image;
    void Start()
    {
        image = this.GetComponent<Image>();
    }

    public void EjectTape(){
        image.sprite = emptyTape;
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Tape"){
            var tapeDrag = col.gameObject.GetComponent<tapeDrag>();
            currentTape = tapeDrag.tapeColor;

            switch (currentTape){
                case TapeColor.red: 
                    image.sprite =  redTape;
                    break;
                case TapeColor.green: 
                    image.sprite =  greenTape;
                    break;
                case TapeColor.blue: 
                    image.sprite =  blueTape;
                    break;
            }
            tapeDrag.DestroyTape();
            tapedeck.LoadTape(currentTape);
        }
    }
}
