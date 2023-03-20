using UnityEngine;

namespace Inventory
{
    public class WeaponAction : ItemAction
    {
        private const float SwingDuration = 0.4f;
        private bool _facingRight;

        public override void StartAction(bool facingRight)
        {
            _facingRight = facingRight;
            gameObject.SetActive(true);
            const float swingAngle = 120f;
            const float swingOffset = 30f;
            const float radius = 1f;
            StartCoroutine(SwingableItem.SwingCoroutine(_facingRight, SwingDuration, transform, transform.parent,
                swingAngle, swingOffset, radius));
        }

        public override void StopAction()
        {
            gameObject.SetActive(false);
        }
    }
}