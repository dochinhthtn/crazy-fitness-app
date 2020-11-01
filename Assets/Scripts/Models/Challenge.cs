using System.Collections.Generic;
namespace Models {
    [System.Serializable]
    public class Challenge : Model {
        public int id;
        public string name;
        public List<Workout> workouts;
    }
}