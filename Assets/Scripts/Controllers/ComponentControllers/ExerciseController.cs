using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.ComponentControllers {
    public class ExerciseController : ComponentController<Exercise> {
        [SerializeField] private Text exerciseName;
        [SerializeField] private Text exerciseCounter;

        public override void Render () {
            Debug.Log(this.data.name);
            this.exerciseName.text = this.data.name;
            if(this.data.countType == "repitition") {
                this.exerciseCounter.text = "x" + this.data.counter.ToString();
            } else {
                int timer = this.data.counter;
                int minutes = (int) Mathf.Floor(timer/60);
                int seconds = timer - minutes * 60;

                this.exerciseCounter.text = minutes.ToString().PadLeft(2, '0') + ":" + seconds.ToString().PadLeft(2, '0');
            }
        }
    }
}