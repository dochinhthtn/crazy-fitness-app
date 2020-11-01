using Models;
using UnityEngine;
using UnityEngine.UI;
using Controller.ListControllers;
using System.Collections.Generic;

namespace Screens {

    public class HomeScreen : Screen {
        [SerializeField] private RectTransform sectionIfNoCurrentPlan;
        [SerializeField] private RectTransform sectionIfHasCurrentPlan;
        [SerializeField] private Text currentPlanName;
        [SerializeField] private RectTransform currentProcessBar;
        [SerializeField] private RectTransform currentCompletedDates;
        [SerializeField] private PlanListController recommendedPlanList;

        private Plan currentPlan;

        HomeScreen () {
            screenName = "Home";
        }

        void Awake () {
            currentPlan = App.instance.profile.currentPlan;
        }

        void Start () {
            ShowCurrentPlanProcess();
            LoadRecommendedPlans();
            LoadRecommendedChallenges();
        }

        void ShowCurrentPlanProcess() {
            sectionIfHasCurrentPlan.gameObject.SetActive (false);
            sectionIfNoCurrentPlan.gameObject.SetActive (false);

            if (currentPlan.id == 0) {
                sectionIfNoCurrentPlan.gameObject.SetActive (true);
            } else {
                sectionIfHasCurrentPlan.gameObject.SetActive (true);
                currentPlanName.text = currentPlan.name;
            }
        }

        void LoadRecommendedPlans () {
            Result<List<Plan>> result = JsonUtility.FromJson<Result<List<Plan>>>(PlayerPrefs.GetString("plans"));
            recommendedPlanList.SetData(result.data);
        }

        void LoadRecommendedChallenges() {

        }
    }
}