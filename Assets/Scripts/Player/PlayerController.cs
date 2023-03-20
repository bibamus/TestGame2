using UnityEngine;
using World;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5f;
        public float jumpForce = 5f;
        public LayerMask groundLayer;

        private Rigidbody2D _rigidbody;
        private bool _isGrounded;
        private bool _facingRight = true;

        [SerializeField] private WorldManager worldManager;
        private CapsuleCollider2D _collider;

        private float _groundCheckDistance = 0.2f;

        private Vector3 _previousWeaponPosition;
        private float _weaponAngle;

        private float _swingTimer = 0f;
        private float _swingDuration = 0.4f;


        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();
            transform.position = worldManager.GetSpawnPositionWorld() + GetSpawnPositionOffset();
        }

        private Vector3 GetSpawnPositionOffset()
        {
            float offsetY = _collider.size.y * 0.5f + 0.1f;
            return new Vector3(0, offsetY, 0);
        }

        private void Update()
        {
            UpdateGroundedState();
            UpdateMovement();
            CheckForJump();
            UpdateFacingDirection();
            HandleWeapon();
        }

        private void HandleWeapon()
        {
            Transform weaponTransform = null;
            if (GetComponent<PlayerState>().weaponInstance != null)
            {
                weaponTransform = GetComponent<PlayerState>().weaponInstance.transform;
            }

            if (Input.GetMouseButtonDown(0))
            {
                // Show weapon sprite
                ShowWeapon(true);

                // Set initial angle for the weapon
                _weaponAngle = _facingRight ? 270f : 90f;

                // Reset swing timer
                _swingTimer = 0f;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                // Hide weapon sprite
                ShowWeapon(false);
            }

            // Update weapon trail and weapon position
            if (weaponTransform != null && _swingTimer <= _swingDuration)
            {
                float swingAngle = 120f; // Change swing angle to modify the range of the swing
                float swingOffset = 30f; // Change swing offset to modify the starting position of the swing

                _swingTimer += Time.deltaTime;
                float swingProgress = _swingTimer / _swingDuration;
                float swingAngleOffset = Mathf.Lerp(0, swingAngle, swingProgress);
                float radius = 1f; // Change radius to modify the swing range
                Vector3 weaponOffset = new Vector3(
                    Mathf.Cos(Mathf.Deg2Rad * (_weaponAngle + swingAngleOffset + swingOffset)) * radius,
                    Mathf.Sin(Mathf.Deg2Rad * (_weaponAngle + swingAngleOffset + swingOffset)) * radius,
                    0
                );
                Vector3 weaponPosition = transform.position + weaponOffset;
                weaponTransform.position = weaponPosition;

                // Rotate weapon sprite
                float rotationZ = -_weaponAngle + (_facingRight ? 90f : 270f);
                weaponTransform.rotation = Quaternion.Euler(0, 0, rotationZ);

                // Check for collisions with weapon trail
                Collider2D[] hits = Physics2D.OverlapAreaAll(_previousWeaponPosition, weaponPosition);
                foreach (Collider2D hit in hits)
                {
                    // Handle collision with hit object
                    // ...
                }

                _previousWeaponPosition = weaponPosition;
            }
        }


        private void ShowWeapon(bool show)
        {
            PlayerState playerState = GetComponent<PlayerState>();
            if (playerState.weaponInstance != null)
            {
                playerState.weaponInstance.SetActive(show);
            }
        }


        private void UpdateGroundedState()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,
                _collider.size.y / 2 + _groundCheckDistance, groundLayer);
            _isGrounded = hit.collider != null;
        }

        private void UpdateMovement()
        {
            float horizontal = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(horizontal * speed, _rigidbody.velocity.y);
            _rigidbody.velocity = movement;
        }

        private void CheckForJump()
        {
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        private void UpdateFacingDirection()
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (horizontal > 0 && !_facingRight)
            {
                Flip();
            }
            else if (horizontal < 0 && _facingRight)
            {
                Flip();
            }
        }

        private void Flip()
        {
            _facingRight = !_facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}