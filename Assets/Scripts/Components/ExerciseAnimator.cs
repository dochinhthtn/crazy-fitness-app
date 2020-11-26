using System.Collections;
using System.Collections.Generic;
using Models;
using UnityEngine;
using UnityEngine.UI;
namespace Components {

    public class ExerciseAnimator : BaseComponent<Exercise> {
        [SerializeField] private ProcessBar processBar;
        private Animator animator;
        private AudioSource audioSource;

        [SerializeField] private AudioClip[] audioClips;
        /* 
            0 - get in starting position
            1 - rest
            2 - end of exercise
            3 - one
            4 - two
            5 - three
            6 - 10 reps
            7 - 20 reps
            8 - 30 reps
            9 - 40 reps
         */
        public delegate void FinishExercise (float duration = 0, float calories = 0);
        public FinishExercise onFinishExercise;
        private int currentCount;

        void Awake () {
            animator = GetComponent<Animator> ();
            audioSource = GetComponent<AudioSource> ();
        }

        public override void Render () {
            currentCount = (data.count_type == "repetition") ? 0 : data.counter;
            Play();

        }

        public void Play () {
            Ready ();
            if (data.name != "Rest") {
                Invoke ("Go", 5.616f);
            } else {
                Go ();
            }
        }

        void Ready () {
            if (data.name == "Rest") {
                audioSource.PlayOneShot (audioClips[1]);
            } else {
                audioSource.PlayOneShot (audioClips[0]);
            }
            
            Count(0);
            animator.fireEvents = false;
            animator.Play ("BaseLayer." + StringUtils.Slugify (data.name), 0);
            animator.speed = 0;

        }

        void Go () {
            animator.fireEvents = true;
            animator.speed = 1;
        }

        public void Resume () {
            animator.speed = 1;
        }

        public void Pause () {
            animator.speed = 0;
        }

        public void One () {
            audioSource.PlayOneShot (audioClips[3]);
        }

        public void Two () {
            audioSource.PlayOneShot (audioClips[4]);
        }

        void CountUp (int amount) {
            currentCount += amount;
            if (processBar != null) {
                processBar.text = currentCount.ToString () + "/" + data.counter.ToString ();
                processBar.percentage = Mathf.Round (currentCount * 100 / data.counter) / 100;
            }

            if (currentCount >= data.counter) {
                EndOfExercise ();
            }

        }

        void CountDown (int amount) {
            Debug.Log(currentCount);
            currentCount -= amount;

            if (processBar != null) {
                processBar.text = StringUtils.SecondsToMinutes (currentCount);
                processBar.percentage = Mathf.Round ((data.counter - currentCount) * 100 / data.counter) / 100;
            }

            if (currentCount <= 0) {
                EndOfExercise ();
            }
        }

        public void Count (int amount) {
            if (data.count_type == "repetition") {
                CountUp (amount);
            } else if (data.count_type == "countdown") {
                CountDown (amount);
            }
        }

        void EndOfExercise () {
            if (data.name != "Rest") {
                audioSource.PlayOneShot (audioClips[2]);
            }

            animator.Play ("BaseLayer.end-of-exercise", 0);
            if (processBar != null) {
                processBar.text = "";
                processBar.percentage = 0f;
            }
            onFinishExercise (
                data.counter,
                data.counter * data.calories
            );
        }

    }
}