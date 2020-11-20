using Models;
using UnityEngine;
using UnityEngine.UI;
using Components;
using System.Collections.Generic;
using Proyecto26;

namespace Screens {

    public class HomeScreen : Screen {
        // statistical
        [SerializeField] private Text challengesCompletedText;
        [SerializeField] private Text durationsText;
        [SerializeField] private Text planCompletedText;
        [SerializeField] private Text caloriesConsumedText;

        // no current plan section
        [SerializeField] private RectTransform sectionIfNoCurrentPlan;
        [SerializeField] private Button getPlanButton;
        // has current plan section
        [SerializeField] private RectTransform sectionIfHasCurrentPlan;
        [SerializeField] private Text currentPlanName;
        [SerializeField] private ProcessBar currentPlanProcessBar;
        [SerializeField] private Text currentPlanDatesText;
        // lists
        [SerializeField] private PlanList recommendedPlanList;

        private Profile profile;
        HomeScreen () {
            screenName = "Home";
            
        }

        void Start () {
            profile = App.instance.profile;

            RenderStatSection();
            RenderCurrentPlanSection();
            RenderRecommendedPlanList();
            RenderRecommendedChallengeList();
        }

        public void GetPlans() {
            Navigator.Navigate("DailyPlanScreen");
        }

        public void ContinueCurrentPlan() {
            if(profile.current_plan.id != 0) {
                Navigator.NavigateWithData("PlanProcessScreen", profile.current_plan);
            }
        }

        void RenderStatSection() {
            challengesCompletedText.text = profile.completed_challenges.Count.ToString();
            durationsText.text = StringUtils.SecondsToMinutes((int) profile.current_durations);
            planCompletedText.text = profile.completed_plans.Count.ToString();
            caloriesConsumedText.text = profile.current_calories.ToString();
        }

        void RenderCurrentPlanSection() {
            Plan currentPlan = profile.current_plan;
            sectionIfHasCurrentPlan.gameObject.SetActive (false);
            sectionIfNoCurrentPlan.gameObject.SetActive (false);

            if (currentPlan.id == 0) {
                sectionIfNoCurrentPlan.gameObject.SetActive (true);
            } else {
                int completedDates = currentPlan.completed_dates.Count;
                int currentPlanDates = currentPlan.dates.Count;
                sectionIfHasCurrentPlan.gameObject.SetActive (true);
                currentPlanName.text = currentPlan.name;
                currentPlanProcessBar.percentage = Mathf.Round(completedDates * 100 / currentPlanDates) / 100;
                currentPlanDatesText.text = completedDates.ToString() + "/" + currentPlanDates.ToString() + " days";
            }
        }

        void RenderRecommendedPlanList () {
            RestClient.Get<ServerResponse<List<Plan>>>(App.instance.config.host + "/plan").Then((result) => {
                recommendedPlanList.SetData(result.data);
            }).Catch((error) => {
                Debug.Log(error.Message);
                recommendedPlanList.ShowErrorPanel();
            });
        }

        void RenderRecommendedChallengeList() {
            
        }
    }
}