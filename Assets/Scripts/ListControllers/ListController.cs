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
        protected RectTransform listRect;
        protected GridLayoutGroup listLayout;
        protected LayoutElement layoutElement;

        protected void Start () {
            listRect = GetComponent<RectTransform> ();
            listLayout = GetComponent<GridLayoutGroup> ();
            
            layoutElement = GetComponent<LayoutElement>();

            listLayout.cellSize = new Vector2 (itemTemplate.rect.width, itemTemplate.rect.height); 
        
            int fixedCount = listLayout.constraintCount;
            listLayout.spacing = new Vector2 (
                layoutElement.minWidth - fixedCount * itemTemplate.rect.width, 
                20
            );

            Render ();
        }

        public void Render () {
            if (data == null) return;
            foreach (var itemData in data) {
                RectTransform item = Instantiate<RectTransform> (itemTemplate);
                item.SetParent (listRect, false);

                C componentController = item.GetComponent<C> ();
                componentController.SetData(itemData);
            }
        }

        public void SetData (List<M> data) {
            this.data = data;
            Render ();
        }
    }
}