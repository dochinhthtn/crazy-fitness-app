using System.Collections;
using System.Collections.Generic;
using Components;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Screens {
    public class DoWorkoutScreen : Screen {
        [SerializeField] private Text currentExerciseName;
        [SerializeField] private Text currentExerciseOrder;
        [SerializeField] private ExerciseAnimator exerciseAnimator;
        [SerializeField] private ProcessBar workoutProcess;
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

            exerciseAnimator.onFinishExercise = (durations, calories) => {
                totalDurations += durations;
                totalCalories += calories;

                Invoke("NextExercise", 3);
            };

            PlayExercise ();
            PlayMusic ();
        }

        public void PlayExercise () {
            RenderWorkoutProcess ();

            exerciseAnimator.SetData (workout.exercises[currentExerciseIndex]);
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


        public void NextExercise () {
            currentExerciseIndex++;
            if (currentExerciseIndex >= workout.exercises.Count) {
                EndWorkout ();
            } else {
                PlayExercise ();
            }
        }

        public void EndWorkout () {
            Profile profile = App.instance.profile;
            profile.current_calories += totalCalories;
            profile.current_durations += totalDurations;

            if (currentDate != null) currentDate.is_completed = true;

            Profile.Save (profile);

            totalDurationsText.text = StringUtils.SecondsToMinutes ((int) totalDurations);
            totalCaloriesText.text = totalCalories.ToString () + " cal";

            endOfWorkoutSection.gameObject.SetActive (true);

        }

        public void Backward () {
            if(currentDate != null) {
                Navigator.tmpData = true;
            } else {
                Navigator.tmpData = workout;
            }
            
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

        public void QuitWorkout() {
            Backward();
        }

        void RenderWorkoutProcess () {
            currentExerciseName.text = workout.exercises[currentExerciseIndex].name;
            currentExerciseOrder.text = (currentExerciseIndex + 1).ToString () + "/" + workout.exercises.Count.ToString ();
            workoutProcess.percentage = Mathf.Round ((currentExerciseIndex + 1) * 100 / workout.exercises.Count) / 100;
        }
    }
}