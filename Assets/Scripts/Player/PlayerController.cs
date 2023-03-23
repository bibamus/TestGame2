using System;
using UI;
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

        private PlayerManager _playerManager;

        public bool FacingRight => _facingRight;

        private InventoryUI _inventoryUI;
        private EquipmentUI _equipmentUI;

        private bool _isJumpRequested;

        void Start()
        {
            _playerManager = GetComponent<PlayerManager>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();
            transform.position = worldManager.GetSpawnPositionWorld() + GetSpawnPositionOffset();
            _inventoryUI = FindObjectOfType<InventoryUI>(true);
            _equipmentUI = FindObjectOfType<EquipmentUI>(true);
        }

        private Vector3 GetSpawnPositionOffset()
        {
            float offsetY = _collider.size.y * 0.5f + 0.1f;
            return new Vector3(0, offsetY, 0);
        }

        private void FixedUpdate()
        {
            UpdateGroundedState();
            UpdateMovement();
            CheckForJump();
            UpdateFacingDirection();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                _isJumpRequested = true;
            }

            HandleMouseWheelInput();
            HandleHotBarInput();

            if (Input.GetKeyDown(KeyCode.R))
            {
                _inventoryUI.ToggleInventory();
                _equipmentUI.ToggleEquipment();
            }
        }

        private void HandleHotBarInput()
        {
            if (_playerManager.HotBar.GetSelectedAction() != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _playerManager.HotBar.GetSelectedAction().UseStart();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _playerManager.HotBar.GetSelectedAction().UseEnd();
                }
            }
        }

        private void HandleMouseWheelInput()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                int newIndex = _playerManager.HotBar.SelectedActionIndex + (scroll > 0 ? -1 : 1);
                int actionsLength = _playerManager.HotBar.Actions.Length;

                newIndex = (newIndex + actionsLength) % actionsLength; // Wrap around the index
                _playerManager.HotBar.SetSelectedHot(newIndex);
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
            if (_isJumpRequested && _isGrounded)
            {
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            _isJumpRequested = false;
        }

        private void UpdateFacingDirection()
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (horizontal > 0 && !FacingRight)
            {
                Flip();
            }
            else if (horizontal < 0 && FacingRight)
            {
                Flip();
            }
        }

        private void Flip()
        {
            _facingRight = !FacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}