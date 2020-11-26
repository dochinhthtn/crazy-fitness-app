using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class History {
    public string screenName;
    public object data;

    public History (string screenName, object data = null) {
        this.screenName = screenName;
        this.data = data;
    }
}
public class Navigator {
    static public object tmpData;
    static public object data {
        get {
            if (histories.Count == 0) return null;
            History first = histories.Peek ();
            return first.data;
        }
    }
    static public Stack<History> histories = new Stack<History> ();

    
    static public History GetCurrent() {
        return histories.Peek();
    }

    static public void Navigate (string screenName) {
        NavigateWithData (screenName, null, null);
    }

    static public void NavigateWithData (string screenName, object data, object tmpData = null) {
        Navigator.tmpData = tmpData;
        histories.Push (new History (screenName, data));
        SceneManager.LoadScene ("LoadingScreen");
    }

    static public void Backward () {
        histories.Pop ();

        if(histories.Count == 0) return;

        // History first = histories.Peek ();
        SceneManager.LoadScene ("LoadingScreen");
    }

    static public void Reload () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }
}