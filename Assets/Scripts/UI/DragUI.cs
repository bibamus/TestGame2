using Inventory;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class DragUI : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private Text itemCount;

        private Item _item;
        private int _stack;

        public void Set(Item item, int stack)
        {
            _item = item;
            _stack = stack;

            itemIcon.sprite = _item.ItemSprite;
            itemCount.text = _stack.ToString();
        }
    }
}