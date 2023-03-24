using System;
using Player;
using UnityEngine;
using World;

namespace Items
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Item : MonoBehaviour
    {
        [SerializeField] private string itemName;
        [SerializeField] private int itemId;
        [SerializeField] private Sprite itemSprite;
        [SerializeField] private ItemType itemType;
        [SerializeField] private int maxStackSize = 1;

        public event Action<PlayerEntity, WorldManager, Item> onUseStart;
        public event Action<PlayerEntity, WorldManager, Item> onUseEnd;
        private PlayerEntity _playerEntity;
        private WorldManager _worldManager;

        public Sprite ItemSprite => itemSprite;

        public ItemType ItemType => itemType;

        public int MaxStackSize => maxStackSize;


        private void Awake()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = ItemSprite;
            _playerEntity = FindObjectOfType<PlayerEntity>();
            _worldManager = FindObjectOfType<WorldManager>();
        }

        public void UseStart()
        {
            gameObject.SetActive(true);
            onUseStart?.Invoke(_playerEntity, _worldManager, this);
        }

        public void UseEnd()
        {
            onUseEnd?.Invoke(_playerEntity, _worldManager, this);
            gameObject.SetActive(false);
        }
    }
}