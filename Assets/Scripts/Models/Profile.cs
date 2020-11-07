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
        public List<Plan> completedPlans;
        public List<Challenge> completedChallenges;
        public float currentCalories;
        public float currentDurations;
        public Plan currentPlan = null;
    }
}