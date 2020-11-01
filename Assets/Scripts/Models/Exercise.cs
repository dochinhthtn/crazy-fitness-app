namespace Models {
    [System.Serializable]
    public class Exercise : Model {
        public int id;
        public string name;
        public string countType;
        public float duration;
        public string tutorial;
        public float calories;
        public int difficulty;
        public int order;
        public int counter;
    }
}