using System.ComponentModel;
using UnityEngine;

namespace Inventory
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "new ItemData", menuName = "ItemData/ItemData", order = 1)]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public int itemId;
        public Sprite itemSprite;
        public ItemType itemType;
        public int maxStackSize;
    }
}