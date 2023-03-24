using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyAI : MonoBehaviour
    {
        public float patrolRange = 10f;
        public float detectionRange = 5f;
        public float moveSpeed = 3f;

        private Transform _player;
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        private bool _isMovingRight;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _startPosition = transform.position;
            _endPosition = _startPosition + Vector2.right * patrolRange;
            _isMovingRight = true;

            _rigidbody = GetComponent<Rigidbody2D>();
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

            if (_isMovingRight && transform.position.x >= _endPosition.x)
            {
                _isMovingRight = false;
            }
            else if (!_isMovingRight && transform.position.x <= _startPosition.x)
            {
                _isMovingRight = true;
            }
        }

        private void ChasePlayer()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

            Vector2 direction = _player.position - transform.position;
            _rigidbody.velocity = direction.normalized * moveSpeed;
        }
    }
}