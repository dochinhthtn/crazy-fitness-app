using UnityEngine.SceneManagement;
class Navigator {
    static public object data;

    static public void Navigate(string screenName) {
        SceneManager.LoadSceneAsync(screenName);
    }

    static public void NavigateWithData(string screenName, object data) {
        Navigator.data = data;
        Navigator.Navigate(screenName);
    }
}