using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Components {
    public class DateContainer : BaseComponent<Models.Date>, IPointerClickHandler {
        [SerializeField] private Text dateName;
        [SerializeField] private Toggle isCompleted;
        public override void Render () {
            dateName.text = "Day " + data.order.ToString ();
            isCompleted.isOn = data.is_completed;
        }

        public void OnPointerClick (PointerEventData pointerEventData) {
            Navigator.NavigateWithData ("ProcessDetailScreen", data, false);
        }
    }
}