using System.Collections;
using UnityEngine;

namespace Item
{
    public class Weapon : Item
    {
        private const float SwingDuration = 0.4f;
        private Transform _playerTransform;
        private bool _facingRight;

        public void StartSwing(Transform playerTransform, bool facingRight)
        {
            _playerTransform = playerTransform;
            _facingRight = facingRight;
            gameObject.SetActive(true);
            StartCoroutine(HandleWeaponCoroutine());
        }

        public void StopSwing()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator HandleWeaponCoroutine()
        {
            const float swingAngle = 120f;
            const float swingOffset = 30f;
            const float radius = 1f;
            var swingTimer = 0f;
            float _weaponAngle = 0;

            while (swingTimer <= SwingDuration)
            {
                swingTimer += Time.deltaTime;
                float swingProgress = swingTimer / SwingDuration;
                float swingDirection = _facingRight ? -1f : 1f;
                float swingAngleOffset = Mathf.Lerp(0, swingAngle * swingDirection, swingProgress);

                Vector3 weaponOffset = new Vector3(
                    Mathf.Cos(Mathf.Deg2Rad * (_weaponAngle + swingAngleOffset + swingOffset)) * radius,
                    Mathf.Sin(Mathf.Deg2Rad * (_weaponAngle + swingAngleOffset + swingOffset)) * radius,
                    0
                );
                Vector3 weaponPosition = _playerTransform.position + weaponOffset;
                transform.position = weaponPosition;

                yield return null;
            }

            StopSwing();
        }
    }
}