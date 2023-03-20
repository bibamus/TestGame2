using Item;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerState : MonoBehaviour
    {
        public int maxHp = 100;
        public int currentHp;
        public int maxMana = 100;
        public int currentMana;

        public Item.Item equippedWeapon;

        public Inventory Inventory;
        
        public GameObject weaponInstance;


        // [SerializeField] private WeaponData stick;

        private void Start()
        {
            currentHp = maxHp;
            currentMana = maxMana;
            Inventory = new Inventory();
            EquipWeapon(equippedWeapon);
        }

        public void TakeDamage(int damage)
        {
            currentHp -= damage;
            if (currentHp <= 0)
            {
                Die();
            }
        }
        
        public void EquipWeapon(Item.Item weapon)
        {
            if (weaponInstance != null)
            {
                Destroy(weaponInstance);
            }

            equippedWeapon = weapon;
            weaponInstance = Instantiate(equippedWeapon.gameObject, transform);
            weaponInstance.SetActive(false);
        }

        public void Heal(int health)
        {
            currentHp = Mathf.Clamp(currentHp + health, 0, maxHp);
        }

        public void UseMana(int manaCost)
        {
            currentMana = Mathf.Clamp(currentMana - manaCost, 0, maxMana);
        }

        public void RestoreMana(int mana)
        {
            currentMana = Mathf.Clamp(currentMana + mana, 0, maxMana);
        }

        private void Die()
        {
            // Handle player death, e.g., respawn or show game over screen
        }
    }
}