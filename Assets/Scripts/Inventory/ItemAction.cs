using UnityEngine;

namespace Inventory
{
    public abstract class ItemAction : MonoBehaviour
    {
        public abstract void StartAction(bool facingRight);
        public abstract void StopAction();
    }

    public class ConsumableAction : ItemAction
    {
        public override void StartAction(bool facingRight)
        {
            // No action to be performed when consumable item is used
        }

        public override void StopAction()
        {
            // No action to be performed when consumable item is used
        }

        public void Consume(Player.PlayerState playerState)
        {
            // Add logic on consume
            Destroy(gameObject);
        }
    }
}