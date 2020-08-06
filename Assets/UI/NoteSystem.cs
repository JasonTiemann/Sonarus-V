using System;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteSystem : MonoBehaviour
{
    public PauseController pauser;
    public TextMeshProUGUI textBox;
    public Button closeButton;
    public Vector2 upPosition;
    public Vector2 downPosition;
    public float moveSpeed;
    public float movementTolerance = 0.01f;
    public string headerTags;
    public string bodyTags;

    private RectTransform position;
    private bool isMoving = false;
    private Vector2 movingTo;
    void Start(){
        position = this.GetComponent<RectTransform>();
        movingTo = upPosition;

        closeButton.onClick.AddListener(HideNote);
    }

    private bool isMousedOver = false;
    void Update(){
        if (isMoving && !isMousedOver) {
            if (Math.Abs(position.localPosition.x - movingTo.x) + Math.Abs(position.localPosition.y - movingTo.y) < movementTolerance)
                isMoving = false;
            else {
                position.localPosition = Vector2.LerpUnclamped(position.localPosition, movingTo, Time.deltaTime * moveSpeed);
            }
        }
    }


    public void ShowNote(string msg) {
        var msgSplit = msg.Split('\n').ToList();
        var formattedMessage = headerTags;
        formattedMessage += msgSplit[0];
        msgSplit.RemoveAt(0);
        formattedMessage += bodyTags;
        Debug.Log(msgSplit[0]);
        formattedMessage += "\n" + string.Join("\n", msgSplit);
        textBox.text = formattedMessage;
        movingTo = downPosition;
        isMoving = true;

        pauser.Pause();
    }

    public void HideNote() {
        textBox.text = "";
        movingTo = upPosition;
        isMoving = true;

        pauser.Resume();
    }

    private void OnMouseEnter() {
        isMousedOver = true;
        Debug.Log("MouseIN");
    }

    private void OnMouseExit() {
        isMousedOver = false;
    }


}
