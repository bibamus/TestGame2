namespace World
{
    public class Block
    {
        public BlockType Type { get; private set; }

        public Block(BlockType type)
        {
            Type = type;
        }
    }
}