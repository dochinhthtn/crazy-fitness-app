using UnityEngine;
using UnityEngine.UI;

namespace Components {
    public class ProcessBar : BaseComponent {
        [SerializeField] private Image loadingBar;
        [SerializeField] private Text loadingText;
        [SerializeField] private float p = 0f;
        [SerializeField] private string t = "";
        public float percentage {
            get {
                return this.p;
            }

            set {
                if (p < 0f) {
                    p = 0;
                } else if (p > 1) {
                    p = 1;
                } else {
                    p = value;
                }
                Render ();
            }
        }

        public string text {
            get {
                return this.t;
            }

            set {
                this.t = value;
            }
        }

        void Start () {
            Render ();
        }

        void Render () {
            loadingBar.fillAmount = p;
            t = (t == "") ? Mathf.Round (p * 100f).ToString () + "%" : t;
            loadingText.text = t;
        }
    }
}