
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Controller.TemplateControllers {
    public class SimpleSearchForm : TemplateController {
        [SerializeField] private InputField keyword;
        [SerializeField] private Button submit;
        public delegate void SimpleSearch(string keyword);
        [SerializeField]
        public SimpleSearch simpleSearch {
            set {
                submit.onClick.RemoveAllListeners();
                submit.onClick.AddListener(() => {
                    value(keyword.text);
                });
            }
        }
    }
}