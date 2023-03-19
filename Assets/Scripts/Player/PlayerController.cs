using UnityEngine;
using World;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        public float speed = 5f;
        public float jumpForce = 5f;

        private Rigidbody2D _rigidbody;
        private bool _isGrounded;

        [SerializeField] private WorldManager worldManager;
        private BoxCollider2D _boxCollider2D;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _boxCollider2D = GetComponent<BoxCollider2D>();
            transform.position = worldManager.GetSpawnPositionWorld() + GetSpawnPositionOffset();
        }

        public Vector3 GetSpawnPositionOffset()
        {
            if (_boxCollider2D != null)
            {
                // Calculate the offset based on the collider size
                float offsetY = _boxCollider2D.size.y * 0.5f + 0.1f;
                return new Vector3(0, offsetY, 0);
            }

            // If no collider is found, return a default offset
            return new Vector3(0, 1, 0);
        }

        void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            Vector2 movement = new Vector2(horizontal * speed, _rigidbody.velocity.y);
            _rigidbody.velocity = movement;

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        // void OnCollisionEnter2D(Collision2D collision)
        // {
        //     if (collision.collider.CompareTag("Ground"))
        //     {
        //         _isGrounded = true;
        //     }
        // }
        //
        // void OnCollisionExit2D(Collision2D collision)
        // {
        //     if (collision.collider.CompareTag("Ground"))
        //     {
        //         _isGrounded = false;
        //     }
        // }
    }
}