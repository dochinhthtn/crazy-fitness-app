using UnityEngine;
using UnityEngine.UI;
public class Alert : Modal {
    [SerializeField] private Text messageBoard;

    public void SetContent (string content) {
        messageBoard.text = content;
    }
}