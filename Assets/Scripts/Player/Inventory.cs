using System.Collections.Generic;
using Item;
using UnityEditor;

namespace Player
{
    public class Inventory
    {
        private List<Item.Item> _items;

        public Inventory()
        {
            _items = new List<Item.Item>();
        }

        public void AddItem(Item.Item item)
        {
            _items.Add(item);
        }

        public void RemoveItem(Item.Item item)
        {
            _items.Remove(item);
        }

        public bool HasItem(Item.Item item)
        {
            return _items.Contains(item);
        }

        public List<Item.Item> GetItems()
        {
            return _items;
        }
    }
}