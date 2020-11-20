using System.Collections.Generic;
using Components;
using Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;

namespace Screens {
    public class DailyPlanScreen : Screen {
        [SerializeField] private RectTransform allPlansContent;
        [SerializeField] private SimpleSearchForm searchPlansForm;
        [SerializeField] private PlanList planList;
        private int currentPlanPage = 1;

        [SerializeField] private RectTransform recommendedPlansPlansContent;
        [SerializeField] private SimpleSearchForm searchRecommendedPlansForm;
        [SerializeField] private PlanList recommendedPlanList;
        private int currentRecommendedPlanPage = 1;

        DailyPlanScreen () {
            screenName = "Daily Plan";
        }

        void Start () {
            ShowAllPlansContent ();
        }

        public void HideAllContent () {
            allPlansContent.gameObject.SetActive (false);
            recommendedPlansPlansContent.gameObject.SetActive (false);
        }

        public void ShowAllPlansContent () {
            HideAllContent ();
            allPlansContent.gameObject.SetActive (true);
            LoadPlans(planList, "/plan", 1);
        }

        public void ShowRecommendedPlansContent () {
            HideAllContent ();
            recommendedPlansPlansContent.gameObject.SetActive (true);
            LoadPlans(recommendedPlanList, "/plan", 1);
        }

        public void ShowCurrentPlanContent () {
            Profile profile = App.instance.profile;
            if(profile.current_plan.id != 0) {
                Navigator.NavigateWithData("PlanProcessScreen", profile.current_plan);
            }
        }

        public void LoadPlans (PlanList list, string url, int page = 1) {
            list.ShowLoadingPanel ();
            RestClient.Get<ServerResponse<List<Plan>>> (App.instance.config.host + url + "?page=" + page.ToString ()).Then ((result) => {
                if (page > 1) {
                    List<Plan> data = list.GetData ();
                    data.AddRange (result.data);
                    list.SetData (data);
                } else {
                    list.SetData (result.data);
                }
            }).Catch ((error) => {
                list.ShowErrorPanel ();
            });
        }

        public void LoadMorePlans() {
            LoadPlans(planList, "/plan", ++currentPlanPage);
        }

        public void LoadMoreRecommendedPlans() {
            LoadPlans(recommendedPlanList, "/plan", ++currentRecommendedPlanPage);
        }
    }
}