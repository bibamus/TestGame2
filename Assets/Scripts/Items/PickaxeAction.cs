using System.Collections;
using Player;
using UnityEngine;
using World;

namespace Items
{
    public class PickaxeAction : MonoBehaviour
    {
        private const float SwingDuration = 0.4f;
        private bool _facingRight;
        private Camera _camera;
        private Item _item;

        private void Start()
        {
            _camera = Camera.main;
            _item = GetComponent<Item>();
            _item.onUseStart += StartAction;

        }
        public void StartAction(PlayerManager playerManager, WorldManager worldManager, Item item)
        {
            _facingRight = playerManager.PlayerController.FacingRight;
            StartCoroutine(SwingAndDeactivate(item));
            StartCoroutine(MineBlockCoroutine(worldManager, playerManager));
        }

        
        private IEnumerator SwingAndDeactivate(Item item)
        {
            const float swingAngle = 120f;
            const float swingOffset = 30f;
            const float radius = 1f;
            yield return StartCoroutine(SwingAction.SwingCoroutine(_facingRight, SwingDuration, transform,
                transform.parent,
                swingAngle, swingOffset, radius));
            item.UseEnd();
        }

        private IEnumerator MineBlockCoroutine(WorldManager worldManager, PlayerManager playerManager)
        {
            // Wait for the swing animation to reach the point where the pickaxe should hit the block
            yield return new WaitForSeconds(SwingDuration / 2);

            // Get mouse position in world coordinates
            Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int tilePosition = worldManager.worldTilemap.WorldToCell(mouseWorldPos);

            Block block = worldManager.State.GetBlock(new Vector2Int(tilePosition.x, tilePosition.y));

            if (block != null)
            {
                // Get the corresponding item for the mined block directly from the BlockType
                Item blockItem = block.Type.BlockItem;
                // Add the mined item to the player's inventory
                playerManager.Inventory.AddItem(blockItem, 1);
                worldManager.State.RemoveBlock(new Vector2Int(tilePosition.x, tilePosition.y));
            }
        }
    }
}