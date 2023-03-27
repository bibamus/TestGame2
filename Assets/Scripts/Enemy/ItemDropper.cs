using Items;
using UnityEngine;

namespace Enemy
{
    public class ItemDropper : MonoBehaviour
    {
        [SerializeField] private DroppedItem droppedItemPrefab;
        
        public static ItemDropper Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void DropItem(DropTable dropTable)
        {
            Item itemToDrop = dropTable.GetRandomItem();
            if (itemToDrop != null)
            {
                SpawnDroppedItem(itemToDrop);
            }
        }

        private void SpawnDroppedItem(Item itemToDrop)
        {
            DroppedItem droppedItemInstance = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity);
            droppedItemInstance.SetItem(itemToDrop);
        }
    }
}
