using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EquipmentUI : MonoBehaviour
    {
        public GameObject equipmentPanel;
        public Image weaponSlot;
        public Image pickaxeSlot;

        private PlayerManager _playerManager;

        private void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            gameObject.SetActive(false);
        }

        public void UpdateUI()
        {
            if (_playerManager.Equipment.EquippedWeapon != null)
            {
                weaponSlot.sprite = _playerManager.Equipment.EquippedWeapon.itemData.itemSprite;
                weaponSlot.enabled = true;
            }
            else
            {
                weaponSlot.enabled = false;
            }

            if (_playerManager.Equipment.EquippedPickaxe != null)
            {
                pickaxeSlot.sprite = _playerManager.Equipment.EquippedPickaxe.itemData.itemSprite;
                pickaxeSlot.enabled = true;
            }
            else
            {
                pickaxeSlot.enabled = false;
            }
        }

        public void ToggleEquipment()
        {
            equipmentPanel.SetActive(!equipmentPanel.activeSelf);

            if (equipmentPanel.activeSelf)
            {
                UpdateUI();
            }
        }
    }
}