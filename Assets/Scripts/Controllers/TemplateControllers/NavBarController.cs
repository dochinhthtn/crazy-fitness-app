using UnityEngine;
using UnityEngine.UI;

namespace Controller.TemplateControllers {
    public class NavBarController : TemplateController {
        [SerializeField] private Button homeNav;
        [SerializeField] private Button planNav;
        [SerializeField] private Button challengeNav;
        [SerializeField] private Button tutorialNav;

        void Awake () {
            homeNav.onClick.AddListener (() => {
                Navigator.Navigate ("HomeScreen");
            });

            planNav.onClick.AddListener (() => {
                Navigator.Navigate ("DailyPlanScreen");
            });

            challengeNav.onClick.AddListener (() => {
                Navigator.Navigate ("ChallengeScreen");
            });

            tutorialNav.onClick.AddListener (() => {
                Navigator.Navigate ("TutorialScreen");
            });
        }

    }

}