namespace Models {
    [System.Serializable]
    public class Exercise : Model {
        public int id;
        public string name;
        public string count_type;
        public float duration;
        public string tutorial;
        public float calories;
        public int difficulty;
        public int order;
        public int counter;
        public string muscle_involved;
    }
}