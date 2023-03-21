using System.Collections;
using Player;
using UnityEngine;
using World;

namespace Inventory
{
    public class PickaxeAction : ItemAction
    {
        private const float SwingDuration = 0.4f;
        private bool _facingRight;
        private WorldManager _worldManager;
        private Camera _camera;
        private PlayerManager _playerManager;

        private void Start()
        {
            _camera = Camera.main;
            _worldManager = FindObjectOfType<WorldManager>();
            _playerManager = FindObjectOfType<PlayerManager>();
        }

        public override void StartAction(bool facingRight)
        {
            _facingRight = facingRight;
            gameObject.SetActive(true);
            const float swingAngle = 120f;
            const float swingOffset = 30f;
            const float radius = 1f;
            StartCoroutine(SwingableItem.SwingCoroutine(_facingRight, SwingDuration, transform, transform.parent,
                swingAngle, swingOffset, radius));
            StartCoroutine(MineBlockCoroutine());
        }

        public override void StopAction()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator MineBlockCoroutine()
        {
            // Wait for the swing animation to reach the point where the pickaxe should hit the block
            yield return new WaitForSeconds(SwingDuration / 2);

            // Get mouse position in world coordinates
            Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int tilePosition = _worldManager.worldTilemap.WorldToCell(mouseWorldPos);

            Block block = _worldManager._worldState.GetBlock(new Vector2Int(tilePosition.x, tilePosition.y));

            if (block != null)
            {
                _worldManager._worldState.RemoveBlock(new Vector2Int(tilePosition.x, tilePosition.y));
                // Get the corresponding item for the mined block directly from the BlockType
                Item blockItem = block.Type.BlockItem;
                // Add the mined item to the player's inventory
                // _playerManager.Inventory.AddItem(blockItem, 1);
                _worldManager._worldState.RemoveBlock(new Vector2Int(tilePosition.x, tilePosition.y));
            }
        }
    }
}