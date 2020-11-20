using UnityEngine;

namespace Components {
    public class NavBar : BaseComponent {
        public void NavigateTo(string screenName) {
            Navigator.Navigate(screenName);
        }
    }
}