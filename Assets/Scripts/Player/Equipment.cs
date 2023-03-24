using Items;

namespace Player
{
    public class Equipment
    {
        public Item EquippedWeapon { get; set; }
        public Item EquippedPickaxe { get; set; }
        public Item EquippedAxe { get; private set; }
        public Item EquippedHelm { get; private set; }
        public Item EquippedChest { get; private set; }
        public Item EquippedTrinket1 { get; private set; }
        public Item EquippedTrinket2 { get; private set; }
        
    }
}