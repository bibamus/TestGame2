using System.Collections;
using UnityEngine;
using World;

namespace Enemy
{
    public class Spawner : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public float spawnInterval = 5f;
        public Vector2 spawnAreaSize = new Vector2(10, 5);
        public float safeDistanceFromPlayer = 10f;

        private Transform _playerTransform;
        [SerializeField] private WorldManager worldManager;
        private const int MaxSpawnAttempts = 40;

        private void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);

                if (FindSpawnPosition(out var spawnPosition))
                {
                    if (!worldManager.HasBlockAtCoordinates(spawnPosition))
                    {
                        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    }
                }
            }
        }

        private bool FindSpawnPosition(out Vector3 spawnPosition)
        {
            int attempts = 0;
            do
            {
                spawnPosition = new Vector3(
                    Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                    Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                    0
                );

                spawnPosition += _playerTransform.position;
                attempts++;
                if (attempts > MaxSpawnAttempts)
                {
                    return false;
                }
            } while (!IsSafeSpawnPosition(spawnPosition));
            return true;
        }

        private bool IsSafeSpawnPosition(Vector3 spawnPosition)
        {
            return Vector3.Distance(spawnPosition, _playerTransform.position) >= safeDistanceFromPlayer;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_playerTransform.position, spawnAreaSize);
        }
    }
}
