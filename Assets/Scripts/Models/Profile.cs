using System.Collections.Generic;
namespace Models {
    [System.Serializable]
    public class Profile : Model {
        public int id;
        public string name;
        public int age;
        public string sex;
        public float weight;
        public float height;

        public float caloriesConsumed;
        public List<Plan> finishedPlans;
        public List<Challenge> finishedChallenges;
        public Plan currentPlan = null;
    }
}