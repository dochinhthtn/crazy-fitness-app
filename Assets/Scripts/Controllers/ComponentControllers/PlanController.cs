using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.ComponentControllers {

    public class PlanController : ComponentController<Plan> {

        [SerializeField] private Text planName;
        [SerializeField] private Text planDates;
        [SerializeField] private Text planRates;

        public override void Render () {
            planName.text = EllipsisText.Make(data.name, 30);
            planDates.text = data.dates.Count + " days";
            planRates.text = "4.3";
        }
    }
}