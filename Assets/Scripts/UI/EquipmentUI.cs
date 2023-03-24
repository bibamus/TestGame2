using System;
using Items;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class EquipmentUI : MonoBehaviour, IPointerClickHandler
    {
        public GameObject equipmentPanel;
        public Image weaponSlot;
        public Image pickaxeSlot;

        [FormerlySerializedAs("playerManager")] [SerializeField] private PlayerEntity playerEntity;



        private void OnEnable()
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            if (playerEntity.Equipment?.EquippedWeapon != null)
            {
                weaponSlot.sprite = playerEntity.Equipment.EquippedWeapon.ItemSprite;
                weaponSlot.enabled = true;
            }
            else
            {
                weaponSlot.enabled = false;
            }

            if (playerEntity.Equipment?.EquippedPickaxe != null)
            {
                pickaxeSlot.sprite = playerEntity.Equipment.EquippedPickaxe.ItemSprite;
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
                    Item oldWeapon = playerEntity.EquipWeapon(item);
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
                    playerEntity.EquipPickaxe(item);
                    UpdateUI();
                    ItemDragHandler.Instance.EndDrag();
                }
            }
        }

        
    }
}