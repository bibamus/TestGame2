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

                var found = FindSpawnPosition(out var spawnPosition);
                if (!found) continue;
                
                // Check if the spawn position is inside a block
                Block blockAtSpawnPosition = worldManager.GetBlockAtCoordinates(spawnPosition);

                if (blockAtSpawnPosition == null)
                {
                    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                }

                // Get the surface Y coordinate at the spawn position
            }
        }

        private bool FindSpawnPosition(out Vector2 spawnPosition)
        {
            int attempts = 0;
            do
            {
                spawnPosition = new Vector2(
                    Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                    Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
                );

                spawnPosition += (Vector2) _playerTransform.position;
                attempts++;
                if (attempts > 40)
                {
                    return false;
                }
            } while (Vector2.Distance(spawnPosition, _playerTransform.position) < safeDistanceFromPlayer);

            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_playerTransform.position, spawnAreaSize);
        }
    }
}