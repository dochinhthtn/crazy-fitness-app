using UnityEngine;
public class Cache {
    public delegate object DefaultValue();
    public static T GetItem<T>(string key) {
        string value = PlayerPrefs.GetString(key);
        return JsonUtility.FromJson<T>(value);
    }

    public static T GetItem<T>(string key, DefaultValue defaultValue) {
        if(PlayerPrefs.HasKey(key)) {
            return GetItem<T>(key);
        } else {
            SetItem<T>(key, (T) defaultValue());
            return (T) defaultValue();
        }
    }

    public static void SetItem (string key, object data) {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
    }

    public static void SetItem<T> (string key, T data) {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
    }
}