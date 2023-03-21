using Inventory;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public int startingMaxHp = 100;
        public int startingMaxMana = 100;

        public Item startingWeapon;
        public Item startingPickaxe;


        public Transform itemAnchor;
        private Inventory.Inventory _inventory;

        private PlayerState _playerState;

        public EquipmentManager Equipment { get; private set; }

        public PlayerController PlayerController { get; private set; }


        private void Start()
        {
            PlayerController = GetComponent<PlayerController>();
            _playerState = new PlayerState(startingMaxHp, startingMaxMana);
            _inventory = new Inventory.Inventory(100);
            Equipment = new EquipmentManager();

            var weapon = Instantiate(startingWeapon.gameObject, itemAnchor).GetComponent<Item>();
            weapon.gameObject.SetActive(false);
            Equipment.EquipWeapon(weapon);

            var pickaxe = Instantiate(startingPickaxe.gameObject, itemAnchor).GetComponent<Item>();
            pickaxe.gameObject.SetActive(false);
            Equipment.EquipPickaxe(pickaxe);
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