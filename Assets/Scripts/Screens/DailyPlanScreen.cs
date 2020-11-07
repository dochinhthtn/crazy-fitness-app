using System.Collections.Generic;
using Controller.ListControllers;
using Controller.TemplateControllers;
using Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;

namespace Screens {
    public class DailyPlanScreen : Screen {
        [SerializeField] private Text planLabel;
        [SerializeField] private PlanListController planList;

        // search plans
        [SerializeField] private SimpleSearchForm searchForm;

        void Start () {
            searchForm.simpleSearch = SearchPlans;
            ShowAllPlans();
        }

        public void ShowRecommendedPlans () {
            RenderPlanList("Recommended Plans", "/plan");
        }

        public void ShowAllPlans () {
            RenderPlanList("All Plans", "/plan");
        }

        public void ShowTopPlans () {
            RenderPlanList("Top Plans", "/plan/top");
        }

        public void SearchPlans (string keyword) {
            if (keyword == "") return;
            RenderPlanList ("Result for \"" + keyword + "\"", "/plan/search/" + keyword);
        }

        void RenderPlanList (string label, string url) {
            planLabel.text = label;
            planList.ShowLoadingPanel();
            RestClient.Get<Result<List<Plan>>> (Config.api + url).Then ((result) => {
                planList.SetData (result.data);
            });
        }
    }
}