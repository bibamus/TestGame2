using System.Collections.Generic;
using UnityEditor;

namespace Player
{
    public class Inventory
    {
        private List<global::Inventory.Item> _items;

        public Inventory()
        {
            _items = new List<global::Inventory.Item>();
        }

        public void AddItem(global::Inventory.Item item)
        {
            _items.Add(item);
        }

        public void RemoveItem(global::Inventory.Item item)
        {
            _items.Remove(item);
        }

        public bool HasItem(global::Inventory.Item item)
        {
            return _items.Contains(item);
        }

        public List<global::Inventory.Item> GetItems()
        {
            return _items;
        }
    }
}