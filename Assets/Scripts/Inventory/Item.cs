using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Item : MonoBehaviour
    {
        public ItemData itemData;
        private ItemAction _itemAction;

        private void Awake()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = itemData.itemSprite;
            _itemAction = GetComponent<ItemAction>();
        }

        public void StartAction(PlayerController playerController)
        {
            if (_itemAction != null)
                _itemAction.StartAction(playerController.FacingRight);
        }

        public void StopAction()
        {
            if (_itemAction != null) _itemAction.StopAction();
        }
    }
}