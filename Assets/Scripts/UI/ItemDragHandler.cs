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
        private Canvas _canvas;
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

        private void Start()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void StartDrag(Item item, int stack)
        {
            if (item == null || stack == 0 || _draggedItemObject != null) return;

            _stack = stack;
            _item = item;
            _draggedItemObject = Instantiate(itemDragPrefab, _canvas.transform);
            _draggedItemRectTransform = _draggedItemObject.GetComponent<RectTransform>();
            DragUI dragUI = _draggedItemObject.GetComponent<DragUI>();
            dragUI.Set(item, stack);
        }

        public void EndDrag()
        {
            if (_draggedItemObject != null)
            {
                Destroy(_draggedItemObject);
                _draggedItemObject = null;
                _draggedItemRectTransform = null;
            }
        }

        private void Update()
        {
            if (_draggedItemObject != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform,
                    Input.mousePosition, _canvas.worldCamera, out var localMousePosition);
                _draggedItemRectTransform.anchoredPosition = localMousePosition;
            }
        }
    }
}