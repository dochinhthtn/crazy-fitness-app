using UnityEngine;
using Models;

namespace Controller.ComponentControllers {
    public abstract class ComponentController<T> : MonoBehaviour {
        protected T data;

        protected void Start() {
            if (data == null) return;
            Render ();
        }
        public void SetData(T data) {
            this.data = data;
            Render();
        }
        public abstract void Render();
    }
}