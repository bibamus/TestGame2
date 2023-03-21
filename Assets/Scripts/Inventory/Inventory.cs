using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory
{
    public class InventorySlot
    {
        public Item Item { get; }
        public int StackSize { get; private set; }

        public InventorySlot(Item item, int stackSize)
        {
            Item = item;
            StackSize = stackSize;
        }

        public void Add(int count)
        {
            StackSize += count;
        }

        public void Remove(int count)
        {
            if (StackSize >= count)
            {
                StackSize -= count;
            }
        }
    }

    public class Inventory
    {
        private readonly InventorySlot[] _slots;
        private readonly int _maxSize;

        public Inventory(int maxSize)
        {
            _slots = new InventorySlot[maxSize];
            _maxSize = maxSize;
        }

        public void AddItem(Item item, int count = 1)
        {
            if(item == null) return;
            var inventorySlot = _slots.FirstOrDefault(s => s != null && s.Item == item);
            if (inventorySlot != null)
            {
                inventorySlot.Add(count);
            }
            else
            {
                var availableSlotIndex = Array.FindIndex(_slots, s => s == null);
                if (availableSlotIndex >= 0)
                {
                    _slots[availableSlotIndex] = new InventorySlot(item, count);
                }
            }
        }

        public void RemoveItem(Item item, int count = 1)
        {
            var inventorySlot = _slots.FirstOrDefault(s => s != null && s.Item == item);
            if (inventorySlot != null)
            {
                inventorySlot.Remove(count);
                if (inventorySlot.StackSize == 0)
                {
                    // Free the slot by setting it to null
                    int index = Array.IndexOf(_slots, inventorySlot);
                    _slots[index] = null;
                }
            }
        }

        public bool HasItem(Item item, int count = 1)
        {
            var inventorySlot = _slots.FirstOrDefault(s => s != null && s.Item == item);
            return inventorySlot != null && inventorySlot.StackSize >= count;
        }

        public InventorySlot[] GetSlots()
        {
            return _slots;
        }
    }
}