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


        [FormerlySerializedAs("playerManager")] [SerializeField] private PlayerEntity playerEntity;

        private void Start()
        {
            foreach (var slot in playerEntity.Inventory.GetSlots())
            {
                GameObject slotObj = Instantiate(inventorySlotPrefab, gridLayoutGroup.transform);
                InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();
                slotUI.SetSlot(slot);
            }
        }



    }
}