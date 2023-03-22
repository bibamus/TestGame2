using Inventory;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private GameObject inventorySlotPrefab;


        [SerializeField] private PlayerManager playerManager;
        private Inventory.Inventory _inventory;

        private void Start()
        {
            _inventory = playerManager.Inventory;
            gameObject.SetActive(false);

            InventorySlot[] slots = _inventory.GetSlots();

            for (int i = 0; i < slots.Length; i++)
            {
                GameObject slotObj = Instantiate(inventorySlotPrefab, gridLayoutGroup.transform);
                InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();
                slotUI.SetSlot(slots[i]);
            }
        }


        public void ToggleInventory()
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}