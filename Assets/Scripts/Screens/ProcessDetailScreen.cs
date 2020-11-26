using System.Collections;
using System.Collections.Generic;
using Components;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Screens {
    public class ProcessDetailScreen : Screen {
        [SerializeField] private Text title;
        [SerializeField] private Text workoutName;
        [SerializeField] private ExerciseList exerciseList;
        [SerializeField] private Text mealDetailText;
        [SerializeField] private Button beginWorkoutButton;

        private Date date;
        ProcessDetailScreen () {
            screenName = "Process Detail";
        }

        void Start () {
            date = (Date) Navigator.data;

            Plan currentPlan = App.instance.profile.current_plan;
            if(currentPlan != null && currentPlan.id != 0) {
                Date currentDate = currentPlan.dates.Find((_date) => {
                    return _date.is_completed == false;
                });

                Debug.Log("aaa");

                if(currentDate != date) {
                    beginWorkoutButton.interactable = false;
                }
            } else {
                beginWorkoutButton.interactable = false;
            }
            // bool isCurrentDate = (bool) Navigator.tmpData;
            // if () {
            //     beginWorkoutButton.interactable = false;
            // }

            RenderCurrentDate ();
            RenderExerciseList ();
            RenderMeal ();
        }

        public void BeginWorkout () {
            Navigator.NavigateWithData("DoWorkoutScreen", date);
        }

        void RenderCurrentDate () {
            title.text = "Day " + date.order.ToString ();
        }

        void RenderExerciseList () {
            workoutName.text = date.workout.name;
            exerciseList.SetData (date.workout.exercises);
        }

        void RenderMeal () {
            mealDetailText.text = date.meal.description;
        }
    }
}