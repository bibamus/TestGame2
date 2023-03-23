using Inventory;
using UnityEngine;

namespace UI
{
    public class ItemDragHandler : MonoBehaviour
    {
        public static ItemDragHandler Instance { get; private set; }

        public GameObject itemDragPrefab;

        private GameObject _draggedItemObject;
        private RectTransform _draggedItemRectTransform;
        [SerializeField] private Canvas canvas;
        private int _stack;
        private Item _item;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void StartDrag(Item item, int stack = 1)
        {
            if (item == null || stack == 0 || _draggedItemObject != null) return;

            _stack = stack;
            _item = item;
            _draggedItemObject = Instantiate(itemDragPrefab, canvas.transform);
            _draggedItemRectTransform = _draggedItemObject.GetComponent<RectTransform>();
            DragUI dragUI = _draggedItemObject.GetComponent<DragUI>();
            dragUI.Set(item, stack);
        }

        public void EndDrag()
        {
            if (_draggedItemObject != null)
            {
                _item = null;
                _stack = 0;
                Destroy(_draggedItemObject);
                _draggedItemObject = null;
                _draggedItemRectTransform = null;
            }
        }

        public bool IsDragging()
        {
            return _item != null;
        }

        public Item GetItem()
        {
            return _item;
        }

        public int GetStackSize()
        {
            return _stack;
        }

        private void Update()
        {
            if (_draggedItemObject != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                    Input.mousePosition, canvas.worldCamera, out var localMousePosition);
                _draggedItemRectTransform.anchoredPosition = localMousePosition;
            }
        }
    }
}