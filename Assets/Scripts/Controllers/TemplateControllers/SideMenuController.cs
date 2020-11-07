using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controller.TemplateControllers {
    public class SideMenuController : TemplateController {
        [SerializeField] private RectTransform menu;
        private bool active = false;
        [SerializeField] private int currentItemIndex = 0;

        void Start () {
            gameObject.SetActive (active);

            AddItemListener ();
            Render ();
        }

        public void Toggle () {
            active = !active;
            gameObject.SetActive (active);
        }

        public void Show () {
            active = true;
            gameObject.SetActive (true);
        }

        public void Hide () {
            active = false;
            gameObject.SetActive (false);
        }

        void Render () {
            for (int i = 0; i < menu.childCount; i++) {
                Image img = menu.GetChild (i).GetComponent<Image> ();
                if (currentItemIndex == i) {
                    img.color = new Color32 (115, 159, 61, 255);
                } else {
                    img.color = Color.white;
                }
            }
        }

        void SelectOption (int itemIndex) {
            currentItemIndex = itemIndex;
            Render ();
        }

        void AddItemListener () {
            for (int i = 0; i < menu.childCount; i++) {
                int tmp = i;
                menu.GetChild (tmp).GetComponent<Button> ().onClick.AddListener (() => SelectOption (tmp));
            }
        }

    }
}