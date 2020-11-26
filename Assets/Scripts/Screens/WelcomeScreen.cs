using UnityEngine.SceneManagement;
namespace Screens {
    public class WelcomeScreen : Screen {
        WelcomeScreen () {
            screenName = "Welcome";
        }

        void Start () {

        }

        public void GetStarted () {
            SceneManager.LoadScene("HomeScreen");
        }
    }
}