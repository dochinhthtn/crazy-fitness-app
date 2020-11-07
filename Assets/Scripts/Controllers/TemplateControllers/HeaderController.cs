using UnityEngine;
using UnityEngine.UI;
namespace Controller.TemplateControllers {
    public class HeaderController : TemplateController {
        [SerializeField] private Button sideMenuButton;
        [SerializeField] private SideMenuController sideMenu;
        [SerializeField] private Button backwardButton;

        void Start() {
            if(sideMenuButton != null && sideMenu != null) {
                sideMenuButton.onClick.AddListener(() => {
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