using UnityEngine;

namespace Screens {
    public abstract class Screen : MonoBehaviour {
        public string screenName;

        protected void Awake () {
            if(App.instance == null) {
                Navigator.Navigate("Manifest");
            }
        }
    }
}