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

        protected void Awake () {
            list = transform.Find("List").gameObject;
            loadingPanel = transform.Find("LoadingPanel").gameObject;
            errorPanel = transform.Find("ErrorPanel").gameObject;
            controlButtons = transform.Find("ControlButtons").gameObject;


        }

        protected void Start() {
            RectTransform listRect = list.GetComponent<RectTransform> ();
            GridLayoutGroup listLayoutGroup = list.GetComponent<GridLayoutGroup> ();
            LayoutElement listLayoutElement = list.GetComponent<LayoutElement> ();

            listLayoutGroup.cellSize = new Vector2 (itemTemplate.rect.width, itemTemplate.rect.height);
            int fixedCount = listLayoutGroup.constraintCount;
            float x = (fixedCount == 1) ? 0 : (listLayoutElement.minWidth - fixedCount * itemTemplate.rect.width) / (fixedCount - 1);
            float y = 20f;
            listLayoutGroup.spacing = new Vector2 (x, y);

            Render (); 
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

        public List<M> GetData() {
            return this.data;
        }

        public void ShowLoadingPanel () {
            errorPanel.SetActive (false);
            loadingPanel.SetActive (true);
        }

        public void HideLoadingPanel () {
            loadingPanel.SetActive (false);
        }

        public void ShowErrorPanel () {
            HideLoadingPanel();
            errorPanel.SetActive (true);
        }

        public void HideErrorPanel () {
            errorPanel.SetActive (false);
        }

    }
}

// protected Transform loadingPanel;
// protected RectTransform listRect;
// protected GridLayoutGroup listLayout;
// protected LayoutElement layoutElement;

// protected void Start () {
//     loadingPanel = transform.Find ("LoadingPanel");

//     listRect = GetComponent<RectTransform> ();
//     listLayout = GetComponent<GridLayoutGroup> ();

//     layoutElement = GetComponent<LayoutElement> ();

//     listLayout.cellSize = new Vector2 (itemTemplate.rect.width, itemTemplate.rect.height);

//     int fixedCount = listLayout.constraintCount;
//     float x = (fixedCount == 1) ? 0 :  (layoutElement.minWidth - fixedCount * itemTemplate.rect.width) / (fixedCount - 1);
//     float y = 20f;
//     listLayout.spacing = new Vector2 (x, y);

//     Render ();
// }

// public void Render () {
//     if (data == null) return;
//     ClearAllItems();
//     HideLoadingPanel ();
//     foreach (var itemData in data) {
//         RectTransform item = Instantiate<RectTransform> (itemTemplate);
//         item.SetParent (listRect, false);
//         C componentController = item.GetComponent<C> ();
//         componentController.SetData (itemData);
//     }
// }

// public void SetData (List<M> data) {
//     this.data = data;
//     Render ();
// }

// public void ShowLoadingPanel () {
//     ClearAllItems();
//     if (loadingPanel != null) loadingPanel.gameObject.SetActive (true);
// }

// public void HideLoadingPanel () {
//     if (loadingPanel != null) loadingPanel.gameObject.SetActive (false);
// }

// public void ClearAllItems () {
//     foreach (Transform item in transform) {
//         if (item.name != "LoadingPanel") {
//             Destroy (item.gameObject);
//         }
//     }
// }