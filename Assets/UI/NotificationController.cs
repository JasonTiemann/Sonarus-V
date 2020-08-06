using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationController : MonoBehaviour
{
    public TextMeshProUGUI messageBox;
    public Vector2 leftPos;
    public Vector2 rightPos;
    public float popupSpeed = 2f;
    public float popupTolerance = 0.01f;

    private CanvasGroup alphaController;
    void Start(){
        alphaController = this.GetComponent<CanvasGroup>();
    }

    private float timeDown = 0;
    private bool isMoving = false;
    private float currentAlpha = 0;
    private void Update() {
        if (isMoving) {
            alphaController.alpha = Mathf.MoveTowards(alphaController.alpha, currentAlpha, Time.deltaTime * popupSpeed);

            if (alphaController.alpha == currentAlpha)
                isMoving = false;
        }else if (Time.time > timeDown && alphaController.alpha == 1){
            currentAlpha = 0;
            isMoving = true;
        }
    }

    public void Show(string message, float timeUp=5f) {
        Debug.Log(message);
        messageBox.text = message;
        currentAlpha = 1;
        isMoving = true;
        timeDown = Time.time + timeUp;
    }
}
