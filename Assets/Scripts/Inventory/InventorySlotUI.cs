using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private Text itemCount;

        public void SetSlot(InventorySlot slot)
        {
            if (slot == null)
            {
                itemIcon.enabled = false;
                itemCount.enabled = false;
            }
            else
            {
                itemIcon.enabled = true;
                itemCount.enabled = true;
                itemIcon.sprite = slot.Item.itemData.itemSprite;
                itemCount.text = slot.StackSize.ToString();
            }

        }
    }
}