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
    static public void Navigate (string screenName) {
        NavigateWithData (screenName, null, null);
    }

    static public void NavigateWithData (string screenName, object data, object tmpData = null) {

        Navigator.tmpData = tmpData;
        histories.Push (new History (screenName, data));
        SceneManager.LoadScene (screenName);
        // loadingTrasition.SetActive(false);
    }

    static public void Backward () {
        histories.Pop ();
        History first = histories.Peek ();
        SceneManager.LoadScene (first.screenName);
    }

    static public void Reload () {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }

    static public IEnumerator LoadAsync (string name) {
        GameObject loadingSection = GameObject.Find ("LoadingSection");
        AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync (name);

        while (!loadSceneOperation.isDone) {
            if (loadingSection != null) loadingSection.SetActive (true);
            yield return null;
        }

    }
}