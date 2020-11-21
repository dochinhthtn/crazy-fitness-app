using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Components {
    public class ExerciseDetailModal : BaseComponent<Models.Exercise> {
        [SerializeField] private Text exerciseNameText;
        [SerializeField] private Text exerciseMuscleInvolvedText;
        [SerializeField] private Text exerciseDifficultyText;
        [SerializeField] private Text exerciseTutorialText;
        [SerializeField] private Animator exerciseAnimator;

        public override void Render () {
            exerciseAnimator.fireEvents = false;
            exerciseAnimator.Play(StringUtils.Slugify (this.data.name));
            exerciseNameText.text = this.data.name;
            exerciseMuscleInvolvedText.text = this.data.muscle_involved;
            exerciseDifficultyText.text = this.data.difficulty.ToString() + "/10";
            exerciseTutorialText.text = this.data.tutorial;
        }

        public void GotIt() {
            Destroy(gameObject);
        }
    }
}