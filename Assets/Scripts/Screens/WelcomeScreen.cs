using UnityEngine;
using UnityEngine.UI;
using Models;
namespace Screens {
    public class WelcomeScreen : Screen {
        [SerializeField] private Button getStarted;

        WelcomeScreen () {
            screenName = "Welcome";
        }

        void Start () {
            getStarted.onClick.AddListener(() => {
                Navigator.Navigate("HomeScreen");
            });
        }

        void GetStarted () {
            Profile profile = App.instance.profile;

        }
    }
}