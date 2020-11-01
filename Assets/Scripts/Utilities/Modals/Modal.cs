using UnityEngine;
using UnityEngine.UI;
public class Modal : MonoBehaviour {
    [SerializeField] private Button close;
    protected string title;

    void Awake () {
        close.onClick.AddListener(delegate {
            Close();
        });
    }

    public void Open() {
        gameObject.SetActive(true);
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}