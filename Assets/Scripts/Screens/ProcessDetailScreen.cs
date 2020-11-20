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

            bool isCurrentDate = (bool) Navigator.tmpData;
            if (!isCurrentDate) {
                beginWorkoutButton.onClick.RemoveAllListeners();
                beginWorkoutButton.image.color = new Color32(200, 200, 200, 255);
            }

            RenderCurrentDate ();
            RenderExerciseList ();
            RenderMeal ();
        }

        public void BeginWorkout () {
            Navigator.NavigateWithData("DoWorkoutScreen", date.workout, date);
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