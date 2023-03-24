using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "DropTable", menuName = "Drop Table", order = 0)]
public class DropTable : ScriptableObject
{
    [System.Serializable]
    public class DropItem
    {
        public Item item;
        [Range(0, 1)] public float probability;
    }

    public List<DropItem> dropItems;

    private void OnValidate()
    {
        NormalizeDropItemProbabilities();
    }

    private void NormalizeDropItemProbabilities()
    {
        float totalProbability = dropItems.Sum(dropItem => dropItem.probability);

        if (totalProbability > 1)
        {
            foreach (DropItem dropItem in dropItems)
            {
                dropItem.probability /= totalProbability;
            }
        }
    }

    public Item GetRandomItem()
    {
        float randomValue = Random.Range(0f, 1f);
        float cumulativeProbability = 0f;

        foreach (DropItem dropItem in dropItems)
        {
            cumulativeProbability += dropItem.probability;
            if (randomValue <= cumulativeProbability)
            {
                return dropItem.item;
            }
        }

        return null; // No item was chosen, can happen if probabilities don't sum up to 1
    }
}