using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.TemplateControllers {

    public class ProfileFormController : TemplateController {

        [SerializeField] private InputField nameInput;
        [SerializeField] private Toggle maleOption;
        [SerializeField] private Toggle femaleOption;
        [SerializeField] private InputField ageInput;
        [SerializeField] private InputField weightInput;
        [SerializeField] private InputField heightInput;

        private Profile profile;

        void Awake () {
            profile = App.instance.profile;
            if (profile == null) profile = new Profile ();
        }

        void Start () {
            AttachInputsListener ();
            SetInputsDefaultValue ();
        }

        void AttachInputsListener () {
            nameInput.onValueChanged.AddListener (delegate {
                InputChangedCallback ("name", nameInput.text);
            });

            maleOption.onValueChanged.AddListener (delegate {
                if (maleOption.isOn) InputChangedCallback ("sex", "male");
            });

            femaleOption.onValueChanged.AddListener (delegate {
                if (femaleOption.isOn) InputChangedCallback ("sex", "female");
            });

            ageInput.onValueChanged.AddListener (delegate {
                InputChangedCallback ("age", Mathf.Abs (int.Parse (ageInput.text)));
            });

            weightInput.onValueChanged.AddListener (delegate {
                InputChangedCallback ("weight", Mathf.Abs (float.Parse (weightInput.text)));
            });

            heightInput.onValueChanged.AddListener (delegate {
                InputChangedCallback ("height", Mathf.Abs (float.Parse (heightInput.text)));
            });
        }

        void SetInputsDefaultValue () {
            nameInput.text = profile.name;

            if (profile.sex == "male") {
                maleOption.isOn = true;
            } else if (profile.sex == "female") {
                femaleOption.isOn = true;
            }

            ageInput.text = profile.age.ToString ();
            weightInput.text = profile.weight.ToString ();
            heightInput.text = profile.height.ToString ();
        }

        void SaveProfile () {
            App.instance.SaveProfile (profile);
        }

        void InputChangedCallback (string info, object value) {
            switch (info) {
                case "name":
                    profile.name = value.ToString ();
                    break;

                case "sex":
                    profile.sex = value.ToString ();
                    break;

                case "age":
                    profile.age = int.Parse (value.ToString ());
                    break;

                case "weight":
                    profile.weight = float.Parse (value.ToString ());
                    break;

                case "height":
                    profile.height = float.Parse (value.ToString ());
                    break;
            }

            SaveProfile ();
        }
    }
}