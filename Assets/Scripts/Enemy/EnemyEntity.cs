using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Enemy
{
    public class EnemyEntity : MonoBehaviour
    {
        public int maxHealth = 100;
        [SerializeField] private DropTable dropTable;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // Handle enemy death here (e.g., play animation, destroy the object, etc.)
            Debug.Log("Enemy died.");

            // Drop an item if there's one from the drop table
            ItemDropper.Instance.DropItem(dropTable);

            Destroy(gameObject);
        }
    }
}