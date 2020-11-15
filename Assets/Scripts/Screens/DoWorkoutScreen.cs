﻿using System.Collections;
using System.Collections.Generic;
using Controller.TemplateControllers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Screens {
    public class DoWorkoutScreen : Screen {
        [SerializeField] private Text currentExerciseName;
        [SerializeField] private Text currentExerciseOrder;
        [SerializeField] private ExerciseAnimatorController exerciseAnimatorController;
        [SerializeField] private ProcessBarController workoutProcess;
        [SerializeField] private Animator nextExerciseAnimator;
        [SerializeField] private AudioSource musicSpeaker;
        [SerializeField] private AudioClip[] musics;
        [SerializeField] private RectTransform endOfWorkoutSection;
        [SerializeField] private Text totalDurationsText;
        [SerializeField] private Text totalCaloriesText;
        [SerializeField] private Button pauseWorkoutButton;
        [SerializeField] private Button resumeWorkoutButton;

        private Workout workout;
        private Date currentDate;
        private float totalDurations;
        private float totalCalories;
        private bool isPaused;
        [SerializeField] private int currentExerciseIndex;
        DoWorkoutScreen () {
            screenName = "Do Workout";
        }

        void Start () {
            workout = (Workout) Navigator.data;
            currentDate = (Date) Navigator.tmpData;

            currentExerciseIndex = 0;

            totalCalories = 0f;
            totalDurations = 0f;

            isPaused = false;

            nextExerciseAnimator.fireEvents = false;

            exerciseAnimatorController.onFinishExercise = (durations, calories) => {
                totalDurations += durations;
                totalCalories += calories;

                Debug.Log (totalDurations);
                Debug.Log (totalCalories);

                StartCoroutine (NextExercise ());
            };

            PlayExercise ();
            PlayMusic ();
        }

        public void PlayExercise () {
            RenderWorkoutProcess ();

            exerciseAnimatorController.PlayExercise (workout.exercises[currentExerciseIndex]);
            if(currentExerciseIndex + 1 < workout.exercises.Count) {
                nextExerciseAnimator.Play ("BaseLayer." + StringUtils.Slugify (workout.exercises[currentExerciseIndex + 1].name), 0);
            }
            
        }

        public void PlayMusic () {
            int randomIndex = Random.Range (0, 2);
            musicSpeaker.clip = musics[randomIndex];
            musicSpeaker.Play();
            // musicSpeaker.PlayOneShot (musics[randomIndex]);
        }


        public IEnumerator NextExercise () {
            yield return new WaitForSeconds (3);
            currentExerciseIndex++;
            if (currentExerciseIndex >= workout.exercises.Count) {
                EndWorkout ();
            } else {
                PlayExercise ();
            }
        }

        public void EndWorkout () {
            Profile profile = App.instance.profile;
            profile.currentCalories += totalCalories;
            profile.currentDurations += totalDurations;

            if (currentDate != null) currentDate.isCompleted = true;

            App.instance.SaveProfile (profile);

            totalDurationsText.text = StringUtils.SecondsToMinutes ((int) totalDurations);
            totalCaloriesText.text = totalCalories.ToString () + " cal";

            endOfWorkoutSection.gameObject.SetActive (true);

        }

        public void Backward () {
            Navigator.Backward ();
        }

        public void PauseWorkout() {
            Time.timeScale = 0;
            pauseWorkoutButton.gameObject.SetActive(false);
            resumeWorkoutButton.gameObject.SetActive(true);
            musicSpeaker.Stop();
        }

        public void ResumeWorkout() {
            Time.timeScale = 1;
            pauseWorkoutButton.gameObject.SetActive(true);
            resumeWorkoutButton.gameObject.SetActive(false);
            musicSpeaker.Play();
        }

        void RenderWorkoutProcess () {
            currentExerciseName.text = workout.exercises[currentExerciseIndex].name;
            currentExerciseOrder.text = (currentExerciseIndex + 1).ToString () + "/" + workout.exercises.Count.ToString ();
            workoutProcess.percentage = Mathf.Round ((currentExerciseIndex + 1) * 100 / workout.exercises.Count) / 100;
        }
    }
}