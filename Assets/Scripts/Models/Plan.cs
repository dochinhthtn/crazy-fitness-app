using System.Collections.Generic;
namespace Models {
    [System.Serializable]
    public class Plan : Model {
        public int id;
        public string name;
        public List<Date> dates;
        public List<Date> completedDates {
            get {
                return dates.FindAll((Date date) => {
                    return date.isCompleted;
                });
            }
        }
    }
}