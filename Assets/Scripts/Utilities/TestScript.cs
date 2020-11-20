using System;
using UnityEngine;
using UnityEngine.Events;

public class TestScript : MonoBehaviour {
    // Start is called before the first frame update
    public UnityEvent myEvent;
    void Start () {
        myEvent.Invoke();

    }

}