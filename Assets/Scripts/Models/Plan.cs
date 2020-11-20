using System.Collections.Generic;
namespace Models {
    [System.Serializable]
    public class Plan : Model {
        public int id;
        public string name;
        public List<Date> dates;
        public List<Date> completed_dates {
            get {
                return dates.FindAll((Date date) => {
                    return date.is_completed;
                });
            }
        }
    }
}