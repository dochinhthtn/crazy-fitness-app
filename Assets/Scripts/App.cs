using System.Collections.Generic;
using Models;
using Proyecto26;
using UnityEngine;

public class App : MonoBehaviour {
    public static App instance = null;
    public Profile profile;

    private App () {

    }

    void Awake () {
        if (App.instance == null) {
            App.instance = this;
            // GetFakeData ();
            DontDestroyOnLoad (gameObject);
        } else {
            Destroy (gameObject);
        }

    }

    void Start () {
        // ClearProfile();

        LoadProfile ();
        if (profile == null) {
            Navigator.Navigate ("WelcomeScreen");
        } else {
            Navigator.Navigate ("HomeScreen");
        }

    }

    public void LoadProfile () {
        string profileJson = PlayerPrefs.GetString ("profile");
        this.profile = JsonUtility.FromJson<Profile> (profileJson);
    }

    public void SaveProfile (Profile profile) {
        this.profile = profile;
        PlayerPrefs.SetString ("profile", JsonUtility.ToJson (profile));
    }

    public void ClearProfile () {
        profile = null;
        PlayerPrefs.DeleteAll ();
    }

    public void GetFakeData () {
        RestClient.Get<Result<List<Plan>>> (Config.api + "/plan").Then ((result) => {
            List<Plan> plans = result.data;
            PlayerPrefs.SetString ("plans", JsonUtility.ToJson(result));
        });
    }
}