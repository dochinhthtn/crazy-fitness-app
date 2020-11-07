using Controller.ListControllers;
using Controller.TemplateControllers;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Screens {
    public class PlanProcessScreen : Screen {
        // Start is called before the first frame update
        [SerializeField] private RectTransform currentPlanInfoSection;
        [SerializeField] private Text currentPlanName;
        [SerializeField] private ProcessBarController currentProcess;
        [SerializeField] private Button startForTodayButton;
        [SerializeField] private RectTransform planInfoSection;
        [SerializeField] private Text planName;
        [SerializeField] private Button startPlanButton;
        [SerializeField] private DateListController dateList;

        private Plan currentPlan;
        private Plan plan;
        void Start () {

            currentPlan = App.instance.profile.currentPlan;

            plan = (Plan) Navigator.data;

            currentPlanInfoSection.gameObject.SetActive (false);
            planInfoSection.gameObject.SetActive (false);

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
            currentPlanName.text = currentPlan.name;
            currentProcess.percentage = currentPlan.completedDates.Count * 100 / currentPlan.dates.Count;
            startForTodayButton.onClick.AddListener (StartForToday);
        }

        void RenderPlanInfoSection () {
            planInfoSection.gameObject.SetActive (true);
            planName.text = plan.name;
            startPlanButton.onClick.AddListener (StartPlan);
        }

        void StartPlan () {
            Profile currentProfile = App.instance.profile;
            if (currentPlan.id != 0) {
                Debug.Log ("Already have a current plan. Are you sure to select this plan instead?");
            } else {
                Debug.Log ("start new plan");
                currentProfile.currentPlan = plan;
                App.instance.SaveProfile (currentProfile);
                Navigator.Reload();
            }
        }

        void StartForToday () {

        }
    }

}