using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Components {
    public abstract class BaseComponent : MonoBehaviour {

    }
    
    public abstract class BaseComponent<M> : MonoBehaviour {
        protected M data;
        public abstract void Render ();
        public M GetData () {
            return this.data;
        }
        public void SetData (M data) {
            this.data = data;

            if(data == null) return; 
            Render ();
        }

    }
}