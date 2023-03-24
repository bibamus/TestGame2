using System;
using Items;
using UnityEngine;

namespace Player
{
    public interface IHotBarAction
    {
        public void UseStart();
        public void UseEnd();

        public Sprite Sprite();
    }

    public class ItemHotBarAction : IHotBarAction
    {
        private Item _item;

        public ItemHotBarAction(Item item)
        {
            _item = item;
        }

        public void UseStart()
        {
            _item.UseStart();
        }

        public void UseEnd()
        {
            _item.UseEnd();
        }

        public Sprite Sprite()
        {
            return _item.ItemSprite;
        }
    }

    public class ItemSupplierHotBarAction : IHotBarAction
    {
        private readonly Func<Item> _supplier;

        public ItemSupplierHotBarAction(Func<Item> supplier)
        {
            _supplier = supplier;
        }

        public void UseStart()
        {
            _supplier.Invoke().UseStart();
        }

        public void UseEnd()
        {
           _supplier.Invoke().UseEnd();
        }

        public Sprite Sprite()
        {
            return _supplier.Invoke().ItemSprite;
        }
    }

    public class HotBar
    {
        public IHotBarAction[] Actions { get; }

        public int SelectedActionIndex { get; set; }

        public HotBar(int size)
        {
            Actions = new IHotBarAction[size];
        }

        public void SetSelectedHot(int index)
        {
            if (index < 0 || index >= Actions.Length)
            {
                return;
            }

            SelectedActionIndex = index;
        }

        public IHotBarAction GetSelectedAction()
        {
            return Actions[SelectedActionIndex];
        }
    }
}