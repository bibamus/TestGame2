using UnityEngine;

namespace Player
{
    public class PlayerState
    {
        public int MaxHp { get; private set; }
        public int CurrentHp { get; set; }
        public int MaxMana { get; private set; }
        public int CurrentMana { get; set; }

        public PlayerState(int maxHp, int maxMana)
        {
            MaxHp = maxHp;
            CurrentHp = maxHp;
            MaxMana = maxMana;
            CurrentMana = maxMana;
        }
    }
}