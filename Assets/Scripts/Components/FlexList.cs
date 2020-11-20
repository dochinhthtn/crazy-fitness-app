using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Components {
    public class FlexList<M, C> : BaseComponent<List<M>> 
        where M : Models.Model 
        where C : BaseComponent<M> {
        [SerializeField] protected RectTransform itemTemplate;
        protected GameObject list;
        protected GameObject loadingPanel;
        protected GameObject errorPanel;
        protected GameObject controlButtons;

        protected void Awake () {
            list = transform.Find ("List").gameObject;
            loadingPanel = transform.Find ("LoadingPanel").gameObject;
            errorPanel = transform.Find ("ErrorPanel").gameObject;
            controlButtons = transform.Find ("ControlButtons").gameObject;

            RectTransform listRect = list.GetComponent<RectTransform> ();
            GridLayoutGroup listLayoutGroup = list.GetComponent<GridLayoutGroup> ();
            LayoutElement listLayoutElement = list.GetComponent<LayoutElement> ();

            listLayoutGroup.cellSize = new Vector2 (itemTemplate.rect.width, itemTemplate.rect.height);
            int fixedCount = listLayoutGroup.constraintCount;
            float x = (fixedCount == 1) ? 0 : (listLayoutElement.minWidth - fixedCount * itemTemplate.rect.width) / (fixedCount - 1);
            float y = 20f;
            listLayoutGroup.spacing = new Vector2 (x, y);
        }

        public override void Render () {
            if (data == null) return;
            Clear ();
            HideLoadingPanel ();

            foreach (var itemData in data) {
                RectTransform item = Instantiate<RectTransform> (itemTemplate);
                item.SetParent (list.transform, false);
                C componentController = item.GetComponent<C> ();
                componentController.SetData (itemData);
            }
        }

        public void Clear () {
            foreach (Transform item in list.transform) {
                Destroy (item.gameObject);
            }
        }

        public void ShowLoadingPanel () {
            try {
                errorPanel.SetActive (false);
                loadingPanel.SetActive (true);

            } catch (System.Exception) {
                //
            }
        }

        public void HideLoadingPanel () {
            loadingPanel.SetActive (false);
        }

        public void ShowErrorPanel () {
            HideLoadingPanel ();
            errorPanel.SetActive (true);
        }

        public void HideErrorPanel () {
            errorPanel.SetActive (false);
        }
    }
}