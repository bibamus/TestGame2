using UnityEngine;

namespace Player
{
    public class PlayerState
    {
        public int MaxHp { get; private set; }
        public int CurrentHp { get; private set; }
        public int MaxMana { get; private set; }
        public int CurrentMana { get; private set; }

        public PlayerState(int maxHp, int maxMana)
        {
            MaxHp = maxHp;
            CurrentHp = maxHp;
            MaxMana = maxMana;
            CurrentMana = maxMana;
        }

        public void TakeDamage(int damage)
        {
            CurrentHp -= damage;
            if (CurrentHp <= 0)
            {
                Die();
            }
        }

        public void Heal(int health)
        {
            CurrentHp = Mathf.Clamp(CurrentHp + health, 0, MaxHp);
        }

        public void UseMana(int manaCost)
        {
            CurrentMana = Mathf.Clamp(CurrentMana - manaCost, 0, MaxMana);
        }

        public void RestoreMana(int mana)
        {
            CurrentMana = Mathf.Clamp(CurrentMana + mana, 0, MaxMana);
        }

        private void Die()
        {
            // Handle player death, e.g., respawn or show game over screen
        }
    }
}