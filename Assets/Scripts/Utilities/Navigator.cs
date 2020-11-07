using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class History {
    public string screenName;
    public object data;

    public History(string screenName, object data = null) {
        this.screenName = screenName;
        this.data = data;
    }
}
public static class Navigator {
    static public object data {
        get {
            if (histories.Count == 0) return null;
            History first = histories.Peek();
            return first.data;
        }
    }
    static public Stack<History> histories = new Stack<History>();
    static public void Navigate(string screenName) {
        NavigateWithData(screenName, null);        
    }

    static public void NavigateWithData(string screenName, object data) {
        histories.Push(new History(screenName, data));
        SceneManager.LoadSceneAsync(screenName);
    }

    static public void Backward() {
        histories.Pop();
        History first = histories.Peek();
        SceneManager.LoadScene(first.screenName);
    }

    static public void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}