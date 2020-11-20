using System.Collections.Generic;
using UnityEngine;
namespace Models {
    [System.Serializable]
    public class Profile : Model {
        public int id;
        public string name;
        public int age;
        public string sex;
        public float weight;
        public float height;

        public float calories_consumed;
        public List<Plan> completed_plans;
        public List<Workout> completed_challenges;
        public float current_calories;
        public float current_durations;
        public Plan current_plan = null;

        public Profile () {
            completed_plans = new List<Plan> ();
            completed_challenges = new List<Workout> ();
        }

        public void Save() {
            Profile.Save(this);
        }

        public static Profile Save (Profile profile) {
            PlayerPrefs.SetString ("profile", JsonUtility.ToJson (profile));
            return profile;
        }

        public static Profile Load () {
            string profileJson = PlayerPrefs.GetString ("profile");
            return JsonUtility.FromJson<Profile> (profileJson);
        }

        public static void Clear () {
            PlayerPrefs.DeleteAll ();
        }
    }
}