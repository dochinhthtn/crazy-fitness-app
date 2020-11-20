using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Components {
    public class ExerciseContainer : BaseComponent<Models.Exercise>, IPointerClickHandler {
        [SerializeField] private Text exerciseName;
        [SerializeField] private Text exerciseCounter;
        [SerializeField] private Animator exerciseAnimator;

        public override void Render () {
            this.exerciseName.text = this.data.name;
            exerciseAnimator.fireEvents = false;
            exerciseAnimator.Play (StringUtils.Slugify (this.data.name));
            if (this.data.count_type.Equals ("repetition")) {
                this.exerciseCounter.text = "x" + this.data.counter.ToString ();
            } else {
                this.exerciseCounter.text = StringUtils.SecondsToMinutes (this.data.counter);
            }
        }

        public void OnPointerClick (PointerEventData pointerEventData) {
            Debug.Log("Show tutorial");
        }
    }
}