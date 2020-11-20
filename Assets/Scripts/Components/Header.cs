using UnityEngine;
using UnityEngine.UI;
namespace Components {
    public class Header : BaseComponent {
        [SerializeField] private Button toggleMenuButton;
        [SerializeField] private SideMenu sideMenu;
        [SerializeField] private Button backwardButton;

        void Start () {
            if(toggleMenuButton != null && sideMenu != null) {
                toggleMenuButton.onClick.AddListener(() => {
                    sideMenu.Toggle();
                });
            }

            if(backwardButton != null) {
                backwardButton.onClick.AddListener(() => {
                    Navigator.Backward();
                });
            }
        }

    }
}