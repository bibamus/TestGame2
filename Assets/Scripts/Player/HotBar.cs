using Inventory;
using UnityEngine;

namespace Player
{
    public interface HotBarAction
    {
        public void UseStart();
        public void UseEnd();

        public Sprite Sprite();
    }

    public class ItemHotBarAction : HotBarAction
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

    public class HotBar
    {
        public HotBarAction[] Actions { get; }

        public int SelectedActionIndex { get; set; }

        public HotBar(int size)
        {
            Actions = new HotBarAction[size];
        }

        public void SetSelectedHot(int index)
        {
            if (index < 0 || index >= Actions.Length)
            {
                return;
            }

            SelectedActionIndex = index;
        }

        public HotBarAction GetSelectedAction()
        {
            return Actions[SelectedActionIndex];
        }
    }
}