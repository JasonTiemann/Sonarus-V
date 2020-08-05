using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tapeSpawner : MonoBehaviour
{
    public bool tapeEnabled = false;
    public TapeColor tapeColor;
    public GameObject tapeObj;
    public Sprite emptyTape;
    public Sprite fullTape;
    
    private Image selfImage;
    void Start()
    {
        selfImage = this.GetComponent<Image>();
        selfImage.sprite = tapeEnabled ? fullTape : emptyTape;
    }

    void OnMouseDown(){
        if (tapeEnabled){
            var spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var newTape = Instantiate(tapeObj, new Vector2(spawnPos.x, spawnPos.y), Quaternion.identity);
            var newTapeScript = newTape.GetComponent<tapeDrag>();
            newTapeScript.originator = this;

            SetEmptyTape();
        }
    }

    public void SetTapeIsEnabled(bool enabled){
        if (tapeEnabled != enabled){
            tapeEnabled = enabled;

            if (tapeEnabled) SetFullTape();
            else SetEmptyTape();
        }
    }

    public void SetEmptyTape(){
        selfImage.sprite = emptyTape;
    }


    public void SetFullTape(){
        if (tapeEnabled){
            selfImage.sprite = fullTape;
        }
    }
}
