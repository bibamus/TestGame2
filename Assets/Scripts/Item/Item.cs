using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Item
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Item : MonoBehaviour
    {
        public ItemData itemData;

        private void Start()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = itemData.itemSprite;
        }
    }
}