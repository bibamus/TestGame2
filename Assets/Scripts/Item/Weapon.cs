using UnityEngine;

namespace Item
{
    public class Weapon : Item
    {
        private float _swingTimer = 0f;
        private float _swingDuration = 0.4f;
        private Vector3 _previousWeaponPosition;
        private float _weaponAngle;


        public void StartSwing()
        {
            gameObject.SetActive(true);
            _swingTimer = 0f;
        }

        public void StopSwing()
        {
            gameObject.SetActive(false);
        }

        public void HandleWeapon(Transform playerTransform, bool facingRight)
        {
            // Only update weapon position if swinging
            if (gameObject.activeSelf && _swingTimer <= _swingDuration)
            {
                const float swingAngle = 120f; // Change swing angle to modify the range of the swing
                const float swingOffset = 30f; // Change swing offset to modify the starting position of the swing

                _swingTimer += Time.deltaTime;
                float swingProgress = _swingTimer / _swingDuration;
                // Invert the swing angle when facing right
                float swingDirection = facingRight ? -1f : 1f;
                float swingAngleOffset = Mathf.Lerp(0, swingAngle * swingDirection, swingProgress);

                const float radius = 1f; // Change radius to modify the swing range
                Vector3 weaponOffset = new Vector3(
                    Mathf.Cos(Mathf.Deg2Rad * (_weaponAngle + swingAngleOffset + swingOffset)) * radius,
                    Mathf.Sin(Mathf.Deg2Rad * (_weaponAngle + swingAngleOffset + swingOffset)) * radius,
                    0
                );
                Vector3 weaponPosition = playerTransform.position + weaponOffset;
                transform.position = weaponPosition;

                // Check for collisions with weapon 
                Collider2D[] hits = Physics2D.OverlapAreaAll(_previousWeaponPosition, weaponPosition);
                foreach (Collider2D hit in hits)
                {
                    // Handle collisions
                }

                _previousWeaponPosition = weaponPosition;
            }
        }
    }
}