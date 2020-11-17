using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.TemplateControllers {
    public class ExerciseAnimatorController : TemplateController {
        [SerializeField] private ProcessBarController exerciseProcessBar;
        private Image image;
        private Animator animator;
        private AudioSource audioSource;
        public delegate void FinishExercise (float duration = 0, float calories = 0);
        public FinishExercise onFinishExercise;
        /* 
            0 - get in starting position
            1 - one
            2 - two
            3 - three
            4 - rest
            5- 10 reps
            6 - 20 reps
            7 - 30 reps
            8 - 40 reps
            9 - end of exercise
         */
        [SerializeField] private AudioClip[] audioClips;
        private Exercise exercise;
        private int count;
        void Start () {
            animator = GetComponent<Animator> ();
            audioSource = GetComponent<AudioSource> ();
            image = GetComponent<Image> ();
        }

        public void PlayExercise (Exercise exercise) {
            SetExercise (exercise);
            StartCoroutine (Play ());
        }

        public void SetExercise (Exercise exercise) {
            this.exercise = exercise;
            count = (exercise.countType == "repetition") ? 0 : exercise.counter;
        }

        public IEnumerator Play () {
            if (StringUtils.Slugify (exercise.name) == "rest") {
                audioSource.PlayOneShot (audioClips[4]);
            } else {
                audioSource.PlayOneShot (audioClips[0]);
            }

            string state = "BaseLayer." + StringUtils.Slugify (exercise.name);
            animator.fireEvents = false;
            animator.Play (state, 0);

            image.color = new Color32 (200, 200, 200, 200);
            yield return new WaitForSeconds (5.616f);

            animator.fireEvents = true;
            animator.Play (state, 0);

            image.color = new Color32 (255, 255, 255, 255);;
        }

        public void Resume () {
            animator.speed = 1;
        }

        public void Pause () {
            animator.speed = 0;
        }

        public void Stop () {

        }

        void CountUp (int amount) {
            count += amount;
            if (exerciseProcessBar != null) {
                exerciseProcessBar.text = count.ToString () + "/" + exercise.counter.ToString ();
                exerciseProcessBar.percentage = Mathf.Round (count * 100 / exercise.counter) / 100;
            }

            if (count >= exercise.counter) {
                EndOfExercise ();
            }

        }

        void CountDown (int amount) {
            count -= amount;

            if (exerciseProcessBar != null) {
                exerciseProcessBar.text = StringUtils.SecondsToMinutes (count);
                exerciseProcessBar.percentage = Mathf.Round ((exercise.counter - count) * 100 / exercise.counter) / 100;
            }

            if (count <= 0) {
                EndOfExercise ();
            }

        }

        void EndOfExercise () {
            audioSource.PlayOneShot (audioClips[9]);
            animator.Play ("BaseLayer.end-of-exercise", 0);
            if (exerciseProcessBar != null) {
                exerciseProcessBar.text = "";
                exerciseProcessBar.percentage = 0f;
            }
            onFinishExercise (
                exercise.counter,
                exercise.counter * exercise.calories
            );
        }

        public void Count (int amount) {
            if (exercise.countType == "repetition") {
                CountUp (amount);
            } else if (exercise.countType == "countdown") {
                CountDown (amount);
            }
        }

        public void One () {
            audioSource.PlayOneShot (audioClips[1]);
        }

        public void Two () {
            audioSource.PlayOneShot (audioClips[2]);
        }
    }
}