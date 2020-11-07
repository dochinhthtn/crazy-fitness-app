using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controller.ListControllers;
using Models;

namespace Screens {
    public class ProcessDetailScreen : Screen {
        [SerializeField] private Text title;
        [SerializeField] private Text workoutName;
        [SerializeField] private ExerciseListController exerciseList;
        [SerializeField] private RectTransform mealSection;

        private Date date;
        public ProcessDetailScreen() {
            screenName = "Process Detail";
        }

        void Start () {
            date = (Date) Navigator.data;

            RenderCurrentDate();
            RenderExerciseList();
            RenderMeal();
        }

        public void StartWorkout() {

        }

        void RenderCurrentDate() {
            title.text = "Day " + date.order.ToString();
        }

        void RenderExerciseList () {
            workoutName.text = date.workout.name;
            exerciseList.SetData(date.workout.exercises);
            Debug.Log(date.workout.exercises.Count);
        }

        void RenderMeal () {

        }
    }
}