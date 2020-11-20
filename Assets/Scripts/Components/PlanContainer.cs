using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Components {
    public class PlanContainer : BaseComponent<Models.Plan>, IPointerClickHandler {
        [SerializeField] private Text planName;
        [SerializeField] private Text planDates;
        [SerializeField] private Text planRates;
        // [SerializeField] 
        public override void Render () {
            planName.text = StringUtils.Ellipsis(data.name, 24);
            planDates.text = data.dates.Count + " days";
            planRates.text = "4.3";
        }

        public void OnPointerClick(PointerEventData pointerEventData) {
            Navigator.NavigateWithData("PlanProcessScreen", data);
        }
    }
}