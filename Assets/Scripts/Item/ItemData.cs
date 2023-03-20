using UnityEngine;

namespace Item
{
    [System.Serializable]
    public class ItemData : ScriptableObject
    {
        public string itemName;
        public int itemId;
        public Sprite itemSprite;

        public ItemData(string name, int id, Sprite sprite)
        {
            itemName = name;
            itemId = id;
            itemSprite = sprite;
        }
    }
}