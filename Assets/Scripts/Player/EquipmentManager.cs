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

        public void EquipWeapon(Item weapon)
        {
            EquippedWeapon = weapon;
        }

        public void EquipPickaxe(Item pickaxe)
        {
            EquippedPickaxe = pickaxe;
        }

        public void EquipAxe(Item axe)
        {
            EquippedAxe = axe;
        }

        public void EquipHelm(Item helm)
        {
            EquippedHelm = helm;
        }

        public void EquipChest(Item chest)
        {
            EquippedChest = chest;
        }

        public void EquipTrinket1(Item trinket1)
        {
            EquippedTrinket1 = trinket1;
        }

        public void EquipTrinket2(Item trinket2)
        {
            EquippedTrinket2 = trinket2;
        }
    }
}