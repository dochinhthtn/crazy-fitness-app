using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.ComponentControllers {
    public class ExerciseController : ComponentController<Exercise> {
        [SerializeField] private Text exerciseName;
        [SerializeField] private Text id;
        [SerializeField] private Text difficulty;

        public override void Render () {
            this.exerciseName.text = this.data.name;
            this.id.text = this.data.id.ToString();
            this.difficulty.text = this.data.difficulty.ToString();
        }
    }
}