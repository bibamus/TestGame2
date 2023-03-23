using Inventory;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class EquipmentUI : MonoBehaviour, IPointerClickHandler
    {
        public GameObject equipmentPanel;
        public Image weaponSlot;
        public Image pickaxeSlot;

        private PlayerManager _playerManager;

        private void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
        }

        public void UpdateUI()
        {
            if (_playerManager.Equipment.EquippedWeapon != null)
            {
                weaponSlot.sprite = _playerManager.Equipment.EquippedWeapon.ItemSprite;
                weaponSlot.enabled = true;
            }
            else
            {
                weaponSlot.enabled = false;
            }

            if (_playerManager.Equipment.EquippedPickaxe != null)
            {
                pickaxeSlot.sprite = _playerManager.Equipment.EquippedPickaxe.ItemSprite;
                pickaxeSlot.enabled = true;
            }
            else
            {
                pickaxeSlot.enabled = false;
            }
        }
        

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (ItemDragHandler.Instance.IsDragging())
                {
                    // Handle item drop based on the current pointer position
                    HandleItemDrop(eventData);
                }
            }
        }
        
        private void HandleItemDrop(PointerEventData eventData)
        {
            var item = ItemDragHandler.Instance.GetItem();

            // Check if the pointer is over the weapon or pickaxe slots
            if (RectTransformUtility.RectangleContainsScreenPoint(weaponSlot.rectTransform, eventData.position, eventData.pressEventCamera))
            {
                if (item.ItemType == ItemType.Weapon)
                {
                    Item oldWeapon = _playerManager.EquipWeapon(item);
                    UpdateUI();
                    ItemDragHandler.Instance.EndDrag();
                    if (oldWeapon != null)
                    {
                        ItemDragHandler.Instance.StartDrag(oldWeapon);
                    }
                }
            }
            else if (RectTransformUtility.RectangleContainsScreenPoint(pickaxeSlot.rectTransform, eventData.position, eventData.pressEventCamera))
            {
                if (item.ItemType == ItemType.Pickaxe)
                {
                    _playerManager.EquipPickaxe(item);
                    UpdateUI();
                    ItemDragHandler.Instance.EndDrag();
                }
            }
        }

        
    }
}