using Models;
using UnityEngine;
using UnityEngine.UI;
using Controller.ListControllers;
using System.Collections.Generic;
using Controller.TemplateControllers;
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
        [SerializeField] private ProcessBarController currentProcessBar;
        [SerializeField] private Text currentPlanDates;
        [SerializeField] private Button continuePlanButton;
        // lists
        [SerializeField] private PlanListController recommendedPlanList;

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
            if(profile.currentPlan.id != 0) {
                Navigator.NavigateWithData("PlanProcessScreen", profile.currentPlan);
            }
        }

        void RenderStatSection() {
            challengesCompletedText.text = profile.completedChallenges.Count.ToString();
            durationsText.text = profile.currentDurations.ToString();
            planCompletedText.text = profile.completedPlans.Count.ToString();
            caloriesConsumedText.text = profile.currentCalories.ToString();
        }

        void RenderCurrentPlanSection() {
            Plan currentPlan = profile.currentPlan;
            sectionIfHasCurrentPlan.gameObject.SetActive (false);
            sectionIfNoCurrentPlan.gameObject.SetActive (false);

            if (currentPlan.id == 0) {
                sectionIfNoCurrentPlan.gameObject.SetActive (true);
            } else {
                sectionIfHasCurrentPlan.gameObject.SetActive (true);
                currentPlanName.text = currentPlan.name;
                currentPlanDates.text = currentPlan.completedDates.Count.ToString() + "/" + currentPlan.dates.Count.ToString() + " days";
            }
        }

        void RenderRecommendedPlanList () {
            RestClient.Get<Result<List<Plan>>>(Config.api + "/plan").Then((result) => {
                recommendedPlanList.SetData(result.data);
            });
        }

        void RenderRecommendedChallengeList() {

        }
    }
}