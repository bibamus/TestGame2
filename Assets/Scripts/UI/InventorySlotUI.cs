using System;
using Inventory;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private Text itemCount;

        private InventorySlot _slot;

        public void SetSlot(InventorySlot slot)
        {
            _slot = slot;

            UpdateSlot();
            _slot.OnStateChanged += UpdateSlot;
        }

        private void OnEnable()
        {
            UpdateSlot();
        }

        private void UpdateSlot()
        {
            if (_slot.Item == null)
            {
                itemIcon.enabled = false;
                itemCount.enabled = false;
            }
            else
            {
                itemIcon.enabled = true;
                itemCount.enabled = true;
                itemIcon.sprite = _slot.Item.itemData.itemSprite;
                itemCount.text = _slot.StackSize.ToString();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Check if it's a left click and there's an item being dragged
            if (eventData.button == PointerEventData.InputButton.Left && ItemDragHandler.Instance.IsDragging())
            {
                // Add the dragged item to the current slot
                var item = ItemDragHandler.Instance.GetItem();
                var stack = ItemDragHandler.Instance.GetStackSize();
                if (_slot.Item == item)
                {
                    _slot.Add(stack);
                }
                else
                {
                    _slot.Add(item, stack);
                }

                // Stop dragging the item
                ItemDragHandler.Instance.EndDrag();

            }

            // Check if it's a left click and the slot is not empty
            else if (eventData.button == PointerEventData.InputButton.Left && _slot != null && _slot.Item != null)
            {
                // Remove the item from the inventory
                PlayerManager playerManager = FindObjectOfType<PlayerManager>();
                var item = _slot.Item;
                var stack = _slot.StackSize;
                _slot.RemoveAll();

                // Start dragging the item with ItemDragHandler
                ItemDragHandler.Instance.StartDrag(item, stack);

            }
        }
    }
}