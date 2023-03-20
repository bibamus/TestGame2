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

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<CapsuleCollider2D>();
            transform.position = worldManager.GetSpawnPositionWorld() + GetSpawnPositionOffset();
        }

        public Vector3 GetSpawnPositionOffset()
        {
            float offsetY = _collider.size.y * 0.5f + 0.1f;
            return new Vector3(0, offsetY, 0);
        }

        void Update()
        {
            UpdateGroundedState();
            UpdateMovement();
            CheckForJump();
            UpdateFacingDirection();
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
