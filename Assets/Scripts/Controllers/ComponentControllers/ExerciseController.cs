using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.ComponentControllers {
    public class ExerciseController : ComponentController<Exercise> {
        [SerializeField] private Text exerciseName;
        [SerializeField] private Text exerciseCounter;
        [SerializeField] private Animator exerciseAnimator;

        public override void Render () {
            this.exerciseName.text = this.data.name;
            exerciseAnimator.fireEvents = false;
            exerciseAnimator.Play(StringUtils.Slugify(this.data.name));
            if(this.data.countType.Equals("repetition")) {
                this.exerciseCounter.text = "x" + this.data.counter.ToString();
            } else {
                this.exerciseCounter.text = StringUtils.SecondsToMinutes(this.data.counter);
            }
        }
    }
}