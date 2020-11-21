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
            Profile profile = (App.instance.profile != null) ? App.instance.profile : new Profile ();
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

        void InputChangedCallback (string info, string value) {

            try {
                switch (info) {
                    case "name":
                        data.name = value;
                        break;

                    case "sex":
                        data.sex = value;
                        break;

                    case "age":
                        data.age = (int) Mathf.Abs(int.Parse(value));
                        break;

                    case "weight":
                        data.weight = Mathf.Abs(float.Parse(value));
                        break;

                    case "height":
                        data.height = Mathf.Abs(float.Parse(value));
                        break;
                }

                Profile.Save (data);
            } catch (System.Exception e) {
                Debug.Log(e.Message);
            }

        }

        void AttachInputsListener () {
            try {
                nameInput.onEndEdit.AddListener (delegate {
                    InputChangedCallback ("name", nameInput.text);
                });

                maleOption.onValueChanged.AddListener (delegate {
                    if (maleOption.isOn) InputChangedCallback ("sex", "male");
                });

                femaleOption.onValueChanged.AddListener (delegate {
                    if (femaleOption.isOn) InputChangedCallback ("sex", "female");
                });

                ageInput.onEndEdit.AddListener (delegate {
                    InputChangedCallback ("age", ageInput.text);
                });

                weightInput.onEndEdit.AddListener (delegate {
                    InputChangedCallback ("weight", weightInput.text);
                });

                heightInput.onEndEdit.AddListener (delegate {
                    InputChangedCallback ("height", heightInput.text);
                });
            } catch (System.Exception e) {
                Debug.Log(e.Message);
            }

        }

    }

}