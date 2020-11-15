using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Controller.ComponentControllers {
    public class DateController : ComponentController<Date>, IPointerClickHandler {
        // Start is called before the first frame update
        [SerializeField] private Text day;
        [SerializeField] private Toggle isCompleted;
        public override void Render () {
            day.text = "Day " + data.order.ToString();
            isCompleted.isOn = data.isCompleted;
        }

        public void OnPointerClick(PointerEventData pointerEventData) {
            Navigator.NavigateWithData("ProcessDetailScreen", data, false);
        }
    }
}