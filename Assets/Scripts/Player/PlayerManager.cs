using Inventory;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public int maxHp = 100;
        public int maxMana = 100;

        public Item equippedWeapon;
        public Item equippedPickaxe;

        public Inventory.Inventory Inventory;

        public Transform itemAnchor;

        public PlayerState State { get; private set; }

        private void Start()
        {
            State = new PlayerState(maxHp, maxMana);
            Inventory = new Inventory.Inventory(100);
            EquipWeapon(equippedWeapon);
            EquipPickaxe(equippedPickaxe);
        }

        public void EquipWeapon(Item weapon)
        {
            equippedWeapon = Instantiate(weapon.gameObject, itemAnchor).GetComponent<Item>();
            equippedWeapon.gameObject.SetActive(false);
        }

        public void EquipPickaxe(Item pickaxe)
        {
            equippedPickaxe = Instantiate(pickaxe.gameObject, itemAnchor).GetComponent<Item>();
            equippedPickaxe.gameObject.SetActive(false);
        }
    }
}