using Inventory;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public int maxHp = 100;
        public int maxMana = 100;


        public Item startingWeapon;
        public Item startingPickaxe;


        public Transform itemAnchor;
        private Inventory.Inventory _inventory;

        private PlayerState _playerState;
        private EquipmentManager _equipment;

        public EquipmentManager Equipment => _equipment;


        private void Start()
        {
            _playerState = new PlayerState(maxHp, maxMana);
            _inventory = new Inventory.Inventory(100);
            _equipment = new EquipmentManager();

            var weapon = Instantiate(startingWeapon.gameObject, itemAnchor).GetComponent<Item>();
            weapon.gameObject.SetActive(false);
            _equipment.EquipWeapon(weapon);

            var pickaxe = Instantiate(startingPickaxe.gameObject, itemAnchor).GetComponent<Item>();
            pickaxe.gameObject.SetActive(false);
            _equipment.EquipPickaxe(pickaxe);
        }


        public void TakeDamage(int damage)
        {
            _playerState.CurrentHp -= damage;
            if (_playerState.CurrentHp <= 0)
            {
                Die();
            }
        }

        public void Heal(int health)
        {
            _playerState.CurrentHp = Mathf.Clamp(_playerState.CurrentHp + health, 0, _playerState.MaxHp);
        }

        public void UseMana(int manaCost)
        {
            _playerState.CurrentMana = Mathf.Clamp(_playerState.CurrentMana - manaCost, 0, _playerState.MaxMana);
        }

        public void RestoreMana(int mana)
        {
            _playerState.CurrentMana = Mathf.Clamp(_playerState.CurrentMana + mana, 0, _playerState.MaxMana);
        }

        private void Die()
        {
            // Handle player death, e.g., respawn or show game over screen
        }
    }
}