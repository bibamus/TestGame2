using System.Collections;
using Player;
using UnityEngine;
using World;

namespace Items
{
    public class WeaponAction : MonoBehaviour
    {
        private const float SwingDuration = 0.4f;
        private bool _facingRight;
        private Item _item;


        private void Start()
        {
            _item = GetComponent<Item>();
            _item.onUseStart += StartAction;
        }

        public void StartAction(PlayerManager playerManager, WorldManager worldManager, Item item)
        {
            _facingRight = playerManager.PlayerController.FacingRight;
            StartCoroutine(SwingAndDeactivate(item));

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
    }
}