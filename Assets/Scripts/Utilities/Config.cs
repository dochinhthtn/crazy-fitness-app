using UnityEngine;
[System.Serializable]
public class Config {
    public string host;
    public string originalData;
    public string driver;

    public static Config Load() {
        TextAsset result = (TextAsset) Resources.Load("config");
        return JsonUtility.FromJson<Config>(result.text);
    }

    
}
