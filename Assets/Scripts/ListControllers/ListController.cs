using System.Collections.Generic;
using Controller.ComponentControllers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Controller.ListControllers {

    public abstract class ListController<M, C> : MonoBehaviour
    where M : Model
    where C : ComponentController<M> {
        protected List<M> data;
        [SerializeField] protected RectTransform itemTemplate;
        protected GameObject list;
        protected GameObject loadingPanel;
        protected GameObject errorPanel;
        protected GameObject controlButtons;

        // data loader
        [SerializeField] string url;
        [SerializeField] int currentPage;
        

        protected void Awake () {
            this.list = transform.Find ("List").gameObject;
            this.loadingPanel = transform.Find ("LoadingPanel").gameObject;
            this.errorPanel = transform.Find ("ErrorPanel").gameObject;
            controlButtons = transform.Find ("ControlButtons").gameObject;

            RectTransform listRect = list.GetComponent<RectTransform> ();
            GridLayoutGroup listLayoutGroup = list.GetComponent<GridLayoutGroup> ();
            LayoutElement listLayoutElement = list.GetComponent<LayoutElement> ();

            listLayoutGroup.cellSize = new Vector2 (itemTemplate.rect.width, itemTemplate.rect.height);
            int fixedCount = listLayoutGroup.constraintCount;
            float x = (fixedCount == 1) ? 0 : (listLayoutElement.minWidth - fixedCount * itemTemplate.rect.width) / (fixedCount - 1);
            float y = 20f;
            listLayoutGroup.spacing = new Vector2 (x, y);

            Render();

        }

        public void Render () {
            if (data == null) return;
            ClearList ();
            HideLoadingPanel ();

            foreach (var itemData in data) {
                RectTransform item = Instantiate<RectTransform> (itemTemplate);
                item.SetParent (list.transform, false);
                C componentController = item.GetComponent<C> ();
                componentController.SetData (itemData);
            }
        }

        public void ClearList () {
            foreach (Transform item in list.transform) {
                Destroy (item.gameObject);
            }
        }

        public void SetData (List<M> data) {
            this.data = data;
            Render ();
        }

        public List<M> GetData () {
            return this.data;
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