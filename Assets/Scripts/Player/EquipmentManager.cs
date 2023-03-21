using Inventory;

namespace Player
{
    public class EquipmentManager
    {
        public Item EquippedWeapon { get; private set; }
        public Item EquippedPickaxe { get; private set; }
        public Item EquippedAxe { get; private set; }
        public Item EquippedHelm { get; private set; }
        public Item EquippedChest { get; private set; }
        public Item EquippedTrinket1 { get; private set; }
        public Item EquippedTrinket2 { get; private set; }

        public Item EquipWeapon(Item weapon)
        {
            Item oldWeapon = EquippedWeapon;
            EquippedWeapon = weapon;
            return oldWeapon;
        }

        public Item EquipPickaxe(Item pickaxe)
        {
            Item oldPickaxe = EquippedPickaxe;
            EquippedPickaxe = pickaxe;
            return oldPickaxe;
        }

        public Item EquipAxe(Item axe)
        {
            Item oldAxe = EquippedAxe;
            EquippedAxe = axe;
            return oldAxe;
        }

        public Item EquipHelm(Item helm)
        {
            Item oldHelm = EquippedHelm;
            EquippedHelm = helm;
            return oldHelm;
        }

        public Item EquipChest(Item chest)
        {
            Item oldChest = EquippedChest;
            EquippedChest = chest;
            return oldChest;
        }

        public Item EquipTrinket1(Item trinket1)
        {
            Item oldTrinket1 = EquippedTrinket1;
            EquippedTrinket1 = trinket1;
            return oldTrinket1;
        }

        public Item EquipTrinket2(Item trinket2)
        {
            Item oldTrinket2 = EquippedTrinket2;
            EquippedTrinket2 = trinket2;
            return oldTrinket2;
        }
    }
}