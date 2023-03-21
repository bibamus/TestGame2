using UnityEngine;

namespace Inventory
{
    public class ConsumableAction : MonoBehaviour
    {
        public void StartAction(bool facingRight)
        {
            // No action to be performed when consumable item is used
        }

        public void StopAction()
        {
            // No action to be performed when consumable item is used
        }

        public void Consume(Player.PlayerManager playerManager)
        {
            // Add logic on consume
            Destroy(gameObject);
        }
    }
}