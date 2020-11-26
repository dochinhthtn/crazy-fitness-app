using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Screens {

    public class LoadingScreen : Screen {
        // Start is called before the first frame update
        LoadingScreen() {
            screenName = "Loading";
        }

        void Start () {
            Invoke("StartLoad", 1f);
        }

        void StartLoad() {
            StartCoroutine(LoadScreenAsync(Navigator.GetCurrent().screenName));
        }

        IEnumerator LoadScreenAsync(string screenName) {
            AsyncOperation loadScreenOperation = SceneManager.LoadSceneAsync(screenName);
            
            while (!loadScreenOperation.isDone) {
                yield return null;
            }
        }
    }
}