using System.Collections;
using UnityEngine;

namespace Items
{
    public class SwingAction : MonoBehaviour
    {
        
        public static IEnumerator SwingCoroutine(bool facingRight, float swingDuration, Transform transform,
            Transform anchor, float swingAngle, float swingOffset, float radius)
        {
            float swingTimer = 0f;
            const float weaponAngle = 0;

            while (swingTimer <= swingDuration)
            {
                swingTimer += Time.deltaTime;
                float swingProgress = swingTimer / swingDuration;
                float swingDirection = facingRight ? -1f : 1f;
                float swingAngleOffset = Mathf.Lerp(0, swingAngle, swingProgress);

                Vector3 weaponOffset = new Vector3(
                    Mathf.Cos(Mathf.Deg2Rad * (weaponAngle + swingAngleOffset + swingOffset)) * radius,
                    Mathf.Sin(Mathf.Deg2Rad * (weaponAngle + swingAngleOffset + swingOffset)) * radius,
                    0
                );
                weaponOffset.x *= swingDirection;
                Vector3 weaponPosition = anchor.position + weaponOffset;
                transform.position = weaponPosition;

                yield return null;
            }
        }
    }
}