using Models;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Controller.ComponentControllers {

    public class PlanController : ComponentController<Plan>, IPointerClickHandler {

        [SerializeField] private Text planName;
        [SerializeField] private Text planDates;
        [SerializeField] private Text planRates;

        void Start () {
            
        }

        public override void Render () {
            planName.text = StringUtils.Ellipsis(data.name, 28);
            planDates.text = data.dates.Count + " days";
            planRates.text = "4.3";
        }

        public void OnPointerClick(PointerEventData pointerEventData) {
            Navigator.NavigateWithData("PlanProcessScreen", data);
        }
    }
}