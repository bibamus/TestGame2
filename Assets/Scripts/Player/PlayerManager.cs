using Items;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [DefaultExecutionOrder(-900)]
    public class PlayerManager : MonoBehaviour
    {
        public int startingMaxHp = 100;
        public int startingMaxMana = 100;

        public Item startingWeapon;
        public Item startingPickaxe;


        public Transform itemAnchor;

        private PlayerState _playerState;

        public Equipment Equipment { get; private set; }

        public PlayerController PlayerController { get; private set; }

        public Inventory.Inventory Inventory { get; private set; }
        public HotBar HotBar { get; private set; }

        public Item WeaponObject { get; private set; }
        public Item PickaxeObject { get; private set; }


        private void Start()
        {
            PlayerController = GetComponent<PlayerController>();
            _playerState = new PlayerState(startingMaxHp, startingMaxMana);
            Inventory = new Inventory.Inventory(100);
            HotBar = new HotBar(10);
            Equipment = new Equipment();
            
            EquipWeapon(startingWeapon);
            EquipPickaxe(startingPickaxe);

            HotBar.Actions[0] = new ItemSupplierHotBarAction(() => WeaponObject);
            HotBar.Actions[1] = new ItemSupplierHotBarAction(() => PickaxeObject);

        }

        public Item EquipWeapon(Item weapon)
        {
            Item oldWeapon = Equipment.EquippedWeapon;
            Equipment.EquippedWeapon = weapon;
            if (WeaponObject != null)
            {
                Destroy(WeaponObject,1f);
            }

            WeaponObject = Instantiate(weapon, itemAnchor);
            WeaponObject.gameObject.SetActive(false);

            return oldWeapon;
        }

        public Item EquipPickaxe(Item pickaxe)
        {
            Item oldPickaxe = Equipment.EquippedPickaxe;
            Equipment.EquippedPickaxe = pickaxe;
            if (PickaxeObject != null)
            {
                Destroy(PickaxeObject,1f);
            }
            PickaxeObject = Instantiate(pickaxe, itemAnchor);
            PickaxeObject.gameObject.SetActive(false);

            return oldPickaxe;
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