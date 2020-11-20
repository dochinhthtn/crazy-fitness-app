
using UnityEngine;
using UnityEngine.UI;

namespace Components {
    public class SimpleSearchForm : BaseComponent {
        [SerializeField] private InputField keyword;
        [SerializeField] private Button submit;
        public delegate void SimpleSearch(string keyword);
        [SerializeField]
        public SimpleSearch onSearch {
            set {
                submit.onClick.RemoveAllListeners();
                submit.onClick.AddListener(() => {
                    value(keyword.text);
                });
            }
        }
    }
}