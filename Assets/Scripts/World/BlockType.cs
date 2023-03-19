using UnityEngine;
using UnityEngine.Tilemaps;

namespace World
{
    [CreateAssetMenu(fileName = "NewBlockType", menuName = "Block Type", order = 1)]
    public class BlockType : ScriptableObject
    {
        [field:SerializeField]
        public string BlockName { get; private set; }
        [field:SerializeField]
        public int Hardness { get; private set; }
        [field:SerializeField]
        public TileBase Tile { get; private set; }
    }
}