using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Components {

    public class ProfileForm : BaseComponent<Models.Profile> {

        [SerializeField] private InputField nameInput;
        [SerializeField] private Toggle maleOption;
        [SerializeField] private Toggle femaleOption;
        [SerializeField] private InputField ageInput;
        [SerializeField] private InputField weightInput;
        [SerializeField] private InputField heightInput;

        void Start () {
            Profile profile = (App.instance.profile != null) ? App.instance.profile : new Profile();
            SetData (profile);

            AttachInputsListener ();
        }

        public override void Render () {
            nameInput.text = data.name;
            if (data.sex == "male") {
                maleOption.isOn = true;
            } else if (data.sex == "female") {
                femaleOption.isOn = true;
            }

            ageInput.text = data.age.ToString ();
            weightInput.text = data.weight.ToString ();
            heightInput.text = data.height.ToString ();
        }

        void InputChangedCallback (string info, object value) {
            switch (info) {
                case "name":
                    data.name = value.ToString ();
                    break;

                case "sex":
                    data.sex = value.ToString ();
                    break;

                case "age":
                    data.age = int.Parse (value.ToString ());
                    break;

                case "weight":
                    data.weight = float.Parse (value.ToString ());
                    break;

                case "height":
                    data.height = float.Parse (value.ToString ());
                    break;
            }

            Profile.Save (data);

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

    }

}