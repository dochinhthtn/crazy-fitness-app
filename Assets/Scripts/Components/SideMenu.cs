using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Components {

    public class SideMenu : BaseComponent {
        [SerializeField] private RectTransform[] menuItems;
        [SerializeField] private int currentItemIndex = 0;
        void Start () {
            Render ();
        }

        public void Render () {
            for (int i = 0; i < menuItems.Length; i++) {
                Image img = menuItems[i].GetComponent<Image> ();
                if (currentItemIndex == i) {
                    img.color = new Color32 (115, 159, 61, 255);
                } else {
                    img.color = Color.white;
                }
            }
        }

        public void Hide () {
            gameObject.SetActive (false);
        }

        public void Show () {
            gameObject.SetActive (true);
        }

        public void Toggle () {
            gameObject.SetActive (!gameObject.activeSelf);
        }

        public void OnItemSelected (int index) {
            currentItemIndex = index;
            Render ();
        }

        public void AddItemSelectable () {
            for (int i = 0; i < menuItems.Length; i++) {
                int index = i;
                menuItems[index].GetComponent<Button> ().onClick.AddListener (() => OnItemSelected (index));
            }
        }
    }
}