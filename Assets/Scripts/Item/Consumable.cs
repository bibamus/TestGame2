using UnityEngine;

namespace Item
{
    public class Consumable : Item
    {
        public void Consume(Player.PlayerState playerState)
        {
            // add logic on consume
            Destroy(gameObject);
        }
    }
}