using Item;
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

        private const float GroundCheckDistance = 0.2f;

        private PlayerState _playerState;


        void Start()
        {
            _playerState = GetComponent<PlayerState>();
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
            if (_playerState.equippedWeapon != null)
            {
                HandleWeaponInput();
            }

            if (_playerState.equippedPickaxe != null)
            {
                HandlePickaxeInput();
            }
        }

        private void HandlePickaxeInput()
        {
            Pickaxe pickaxe = _playerState.equippedPickaxe;

            if (Input.GetMouseButtonDown(1))
            {
                pickaxe.StartSwing(_facingRight);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                pickaxe.StopSwing();
            }
        }

        private void HandleWeaponInput()
        {
            Weapon weapon = _playerState.equippedWeapon;

            if (Input.GetMouseButtonDown(0))
            {
                weapon.StartSwing(_facingRight);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                weapon.StopSwing();
            }
        }


        private void UpdateGroundedState()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down,
                _collider.size.y / 2 + GroundCheckDistance, groundLayer);
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