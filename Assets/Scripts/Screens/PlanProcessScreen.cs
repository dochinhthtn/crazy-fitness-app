using Components;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Screens {
    public class PlanProcessScreen : Screen {
        // Start is called before the first frame update
        [SerializeField] private RectTransform currentPlanInfoSection;
        [SerializeField] private Text currentPlanName;
        [SerializeField] private Text currentPlanDate;
        [SerializeField] private ProcessBar currentProcess;
        [SerializeField] private Button startForTodayButton;

        [SerializeField] private RectTransform planInfoSection;
        [SerializeField] private Text planName;
        [SerializeField] private Text planDates;
        [SerializeField] private Button startPlanButton;

        [SerializeField] private DateList dateList;

        private Plan currentPlan;
        private Plan plan;
        PlanProcessScreen () {
            screenName = "Plan Process";
        }
        void Start () {
            currentPlan = App.instance.profile.current_plan;
            plan = (Plan) Navigator.data;

            currentPlanInfoSection.gameObject.SetActive (false);
            startForTodayButton.gameObject.SetActive (false);

            planInfoSection.gameObject.SetActive (false);
            startPlanButton.gameObject.SetActive (false);

            if (currentPlan != null && plan.id == currentPlan.id) {

                RenderCurrentPlanInfoSection ();
            } else {
                RenderPlanInfoSection ();
            }

            RenderDateList ();
        }

        void RenderDateList () {
            if (currentPlan != null && plan.id == currentPlan.id) {
                dateList.SetData (currentPlan.dates);
            } else {
                dateList.SetData (plan.dates);
            }
        }

        void RenderCurrentPlanInfoSection () {
            currentPlanInfoSection.gameObject.SetActive (true);
            startForTodayButton.gameObject.SetActive (true);
            currentPlanName.text = currentPlan.name;
            float percentage =  Mathf.Round(currentPlan.completed_dates.Count * 100 / currentPlan.dates.Count) / 100;
            currentProcess.percentage = percentage;
        }

        void RenderPlanInfoSection () {
            planInfoSection.gameObject.SetActive (true);
            startPlanButton.gameObject.SetActive (true);
            planName.text = plan.name;
            planDates.text = plan.dates.Count.ToString () + " days";
        }

        public void StartPlan () {
            Profile currentProfile = App.instance.profile;
            if (currentPlan.id != 0) {
                Debug.Log ("Already have a current plan. Are you sure to select this plan instead?");
            } else {
                currentProfile.current_plan = plan;
                Profile.Save (currentProfile);
                Navigator.Reload ();
            }
        }

        public void StartForToday () {
            foreach (Date date in currentPlan.dates) {
                if (!date.is_completed) {
                    Navigator.NavigateWithData ("ProcessDetailScreen", date, true);
                    break;
                }
            }
        }
    }

}