using System.Collections;
using System.Collections.Generic;
using Components;
using Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;

namespace Screens {
    public class ChallengeScreen : Screen {
        [SerializeField] private InputField minuteInput;
        [SerializeField] private ExerciseList exerciseList;
        [SerializeField] private Text myWorkoutNameText;
        [SerializeField] private Button startMyWorkoutButton;
        private Workout myWorkout;

        void Start () {
            try {
                var workout = (Workout) Navigator.tmpData;
                if(workout != null) {
                    Profile profile = App.instance.profile;
                    profile.completed_challenges.Add(workout);
                    profile.Save();
                }

            } catch (System.Exception) {

            }
        }
        public void GenerateRandomChallenge () {
            exerciseList.Clear();
            startMyWorkoutButton.gameObject.SetActive(false);
            exerciseList.ShowLoadingPanel();
            RestClient.Get<ServerResponse<Workout>> (App.instance.config.host + "/workout/random/" + minuteInput.text).Then ((serverResponse) => {
                myWorkout = serverResponse.data;
                exerciseList.SetData (myWorkout.exercises);
                myWorkoutNameText.text = myWorkout.name;
                startMyWorkoutButton.gameObject.SetActive (true);
            }).Catch ((error) => {
                exerciseList.ShowErrorPanel ();
            });
        }

        public void ReadyForWorkout () {
            Navigator.NavigateWithData ("DoWorkoutScreen", myWorkout);
        }
    }
}