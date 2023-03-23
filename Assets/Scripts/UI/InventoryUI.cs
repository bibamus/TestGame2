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

        private void Start()
        {
            foreach (var slot in playerManager.Inventory.GetSlots())
            {
                GameObject slotObj = Instantiate(inventorySlotPrefab, gridLayoutGroup.transform);
                InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();
                slotUI.SetSlot(slot);
            }
        }



    }
}