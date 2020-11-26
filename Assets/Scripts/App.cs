using System.Collections.Generic;
using Models;
using Proyecto26;
using UnityEngine;

public class App : MonoBehaviour {
    public static App instance = null;
    public Profile profile;
    public Config config;
    public bool clearAll = false;

    private App () {

    }

    void Awake () {
        if (App.instance == null) {
            App.instance = this;
            DontDestroyOnLoad (gameObject);
        } else {
            Destroy (gameObject);
        }
    }

    void Start () {
        if (clearAll) {
            Profile.Clear ();
            return;
        }
        
        this.config = Config.Load();
        this.profile = Profile.Load ();

        if (profile == null) {
            profile = new Profile();
            profile.name = "Your name";
            Navigator.Navigate ("WelcomeScreen");
        } else {
            Navigator.Navigate ("HomeScreen");
        }

    }

    void Update () {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Navigator.Backward();
        }
    }
}