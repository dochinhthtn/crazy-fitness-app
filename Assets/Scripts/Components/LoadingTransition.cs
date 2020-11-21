using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components {
    public class LoadingTransition : BaseComponent {
        void Awake () {
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
        }
    }
}