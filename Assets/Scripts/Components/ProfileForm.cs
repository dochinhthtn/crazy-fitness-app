using Models;
using UnityEngine;
using UnityEngine.UI;
using System;

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

        void AttachInputsListener () {
            try {
                nameInput.onValueChanged.AddListener ((value) => {
                    data.name = value;
                });

                maleOption.onValueChanged.AddListener ((value) => {
                    if (maleOption.isOn) data.sex = "male";
                });

                femaleOption.onValueChanged.AddListener ((value) => {
                    if (femaleOption.isOn) data.sex = "female";
                });

                ageInput.onValueChanged.AddListener ((value) => {
                    Int32.TryParse(value, out data.age);
                });

                weightInput.onValueChanged.AddListener ((value) => {
                    float.TryParse(value, out data.weight);
                });

                heightInput.onValueChanged.AddListener ((value) => {
                    float.TryParse(value, out data.height);
                });

            } catch (System.Exception e) {
                Debug.Log (e.Message);
            }
        }

        public void SaveProfile() {
            Profile.Save(data);
        }

    }

}