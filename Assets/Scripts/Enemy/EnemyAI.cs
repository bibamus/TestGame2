using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private  float patrolRange = 10f;
        [SerializeField] private  float detectionRange = 5f;
        [SerializeField] private  float moveSpeed = 3f;
        [SerializeField] private  float jumpForce = 1f;
        [SerializeField] private  float obstacleDetectionDistance = 1f;
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private  float changeDirectionProbability = 0.01f; // The probability of changing direction each second (0 to 1)

        [SerializeField] private float groundedCheckDistance = 0.1f;
        [SerializeField] private LayerMask groundLayer;

        private Transform _player;
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private bool _isMovingRight;

        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _startPosition = transform.position;
            _endPosition = _startPosition + Vector2.right * patrolRange;
            _isMovingRight = true;

            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();

            StartCoroutine(CheckDirectionChange());
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, _player.position) <= detectionRange)
            {
                ChasePlayer();
            }
            else
            {
                Patrol();
            }
        }

        private void Patrol()
        {
            float direction = _isMovingRight ? 1 : -1;
            _rigidbody.velocity = new Vector2(direction * moveSpeed, _rigidbody.velocity.y);
            if (ObstacleInFront() && IsGrounded())
            {
                Jump();
            }
        }

        private void ChasePlayer()
        {
            Vector2 direction = _player.position - transform.position;
            _rigidbody.velocity = new Vector2(direction.normalized.x * moveSpeed, _rigidbody.velocity.y);

            if (ObstacleInFront() && IsGrounded())
            {
                Jump();
            }
        }

        private bool ObstacleInFront()
        {
            float direction = _isMovingRight ? 1 : -1;
            // Vector2 origin = new Vector2(transform.position.x, transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * direction, obstacleDetectionDistance,
                obstacleLayer);
            return hit.collider != null;
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        private bool IsGrounded()
        {
            var bounds = _collider.bounds;
            var origin = new Vector2(bounds.center.x, bounds.min.y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down,
                groundedCheckDistance, groundLayer);
            Debug.DrawLine(origin, origin + Vector2.down * groundedCheckDistance, Color.magenta);
            return hit.collider != null;
        }

        private IEnumerator CheckDirectionChange()
        {
            while (true)
            {
                yield return new WaitForSeconds(1); // Wait for 1 second
                if (Random.value <= changeDirectionProbability)
                {
                    _isMovingRight = !_isMovingRight;
                }
            }
        }
    }
}
