using System.Collections.Generic;

namespace Models {
    [System.Serializable]
    public class Workout : Model {
        public int id;
        public string name;
        public string description;
        public List<Exercise> exercises;
    }
}