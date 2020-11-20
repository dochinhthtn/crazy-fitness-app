namespace Models {
    [System.Serializable]
    public class Date : Model {
        public int id;
        public int order;
        public Workout workout;
        public Meal meal;
        public bool is_completed = false;
    }
}