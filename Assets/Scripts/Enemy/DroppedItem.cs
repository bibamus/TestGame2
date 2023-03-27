using Items;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DroppedItem : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private Item _item;

        private void Start()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        public void SetItem(Item item)
        {
            _item = item;
            if (_spriteRenderer != null && _item != null)
            {
                _spriteRenderer.sprite = _item.ItemSprite;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Player collided with dropped item");
            }
        }
    }
}