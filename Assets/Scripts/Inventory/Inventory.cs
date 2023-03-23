using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Inventory
{
    public class InventorySlot
    {
        public event Action OnStateChanged;

        public Item Item { get; set; }
        public int StackSize { get; set; }

        public InventorySlot(Item item, int stackSize)
        {
            Item = item;
            StackSize = stackSize;
        }

        public void Add(int count)
        {
            StackSize += count;
            OnStateChanged?.Invoke();
        }

        public void Remove(int count)
        {
            if (StackSize >= count)
            {
                StackSize -= count;
            }

            if (StackSize == 0)
            {
                Item = null;
            }

            OnStateChanged?.Invoke();
        }

        public void RemoveAll()
        {
            StackSize = 0;
            Item = null;
            OnStateChanged?.Invoke();
        }

        public void Add(Item item, int stack)
        {
            if (Item == null)
            {
                Item = item;
                StackSize = stack;
                OnStateChanged?.Invoke();
            }
        }

        public bool CanAcceptItem(Item item, int count)
        {
            return Item == item && StackSize + count < Item.MaxStackSize;
        }
    }


    public class Inventory
    {
        public event Action OnInventorySlotChanged;

        private readonly InventorySlot[] _slots;
        private readonly int _maxSize;

        public Inventory(int maxSize)
        {
            _slots = new InventorySlot[maxSize];
            for (int i = 0; i < _slots.Length; i++)
            {
                _slots[i] = new InventorySlot(null, 0);
                _slots[i].OnStateChanged += () => OnInventorySlotChanged?.Invoke();
            }

            _maxSize = maxSize;
        }

        public void AddItem(Item item, int count = 1)
        {
            // If item is null, do nothing and return
            if (item == null) return;

            // Try to find an existing slot containing the same item
            var inventorySlot = _slots.FirstOrDefault(s => s != null && s.CanAcceptItem(item, count));
            if (inventorySlot != null)
            {
                inventorySlot.Add(count);
            }
            else
            {
                // If no existing slot is found, try to find an empty slot
                var availableSlot = _slots.FirstOrDefault(s => s != null && s.Item == null);
                availableSlot?.Add(item, count);
            }
        }

        public void RemoveItem(Item item, int count = 1)
        {
            var inventorySlot = _slots.FirstOrDefault(s => s != null && s.Item == item);
            if (inventorySlot != null)
            {
                inventorySlot.Remove(count);
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