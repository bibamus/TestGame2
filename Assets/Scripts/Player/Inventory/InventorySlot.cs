using System;
using Items;

namespace Player.Inventory
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
}