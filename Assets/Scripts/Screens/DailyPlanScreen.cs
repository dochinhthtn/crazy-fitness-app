using System.Collections.Generic;
using Controller.ListControllers;
using Controller.TemplateControllers;
using Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;

namespace Screens {
    public class DailyPlanScreen : Screen {
        [SerializeField] private RectTransform allPlansContent;
        [SerializeField] private SimpleSearchForm searchPlansForm;
        [SerializeField] private PlanListController planList;
        private int currentPlanPage = 1;

        [SerializeField] private RectTransform recommendedPlansPlansContent;
        [SerializeField] private SimpleSearchForm searchRecommendedPlansForm;
        [SerializeField] private PlanListController recommendedPlanList;
        private int currentRecommendedPlanPage = 1;

        DailyPlanScreen () {
            screenName = "Daily Plan";
        }

        void Start () {
            ShowAllPlansContent ();
            RenderPlanList ();
            RenderRecommendedPlanList ();
        }

        public void HideAllContent () {
            allPlansContent.gameObject.SetActive (false);
            recommendedPlansPlansContent.gameObject.SetActive (false);
        }

        public void ShowAllPlansContent () {
            HideAllContent ();
            allPlansContent.gameObject.SetActive (true);
        }

        public void ShowRecommendedPlansContent () {
            HideAllContent ();
            recommendedPlansPlansContent.gameObject.SetActive (true);
        }

        public void ShowCurrentPlanContent () {
            Profile profile = App.instance.profile;
            if(profile.currentPlan.id != 0) {
                Navigator.NavigateWithData("ProcessDetailScreen", profile.currentPlan);
            }
        }

        public void LoadPlans (PlanListController planList, string url, int page = 1) {
            planList.ShowLoadingPanel ();
            RestClient.Get<Result<List<Plan>>> (Config.api + url + "?page=" + page.ToString ()).Then ((result) => {
                if (page > 1) {
                    List<Plan> data = planList.GetData ();
                    data.AddRange (result.data);
                    planList.SetData (data);
                } else {
                    planList.SetData (result.data);
                }
            }).Catch ((error) => {
                planList.ShowErrorPanel ();
            });
        }

        public void RenderPlanList () {
            LoadPlans (this.planList, "/plan", currentPlanPage++);
        }

        public void RenderRecommendedPlanList () {
            LoadPlans (this.recommendedPlanList, "/plan", currentRecommendedPlanPage++);
        }

    }
}