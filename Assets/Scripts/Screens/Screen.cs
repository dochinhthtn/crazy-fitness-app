using UnityEngine;
using UnityEngine.SceneManagement;
namespace Screens {
    public abstract class Screen : MonoBehaviour {
        public string screenName;

        protected void Awake () {
            if(App.instance == null) {
                SceneManager.LoadScene("Manifest");
            }
        }
    }
}