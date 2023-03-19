using System;
using UnityEngine;

namespace World
{
    public class WorldGenerator
    {
        public BlockType DirtBlockType { get; set; }
        public BlockType StoneBlockType { get; set; }

        public float Amplitude { get; set; }
        public float Frequency { get; set; }
        public float Scale { get; set; }
        public int DirtBaseHeight { get; set; }
        public int StoneBaseHeight { get; set; }

        public WorldState GenerateWorld(int width, int height)
        {
            int undergroundHeight = Mathf.FloorToInt(height * 0.4f);
            int surfaceHeight = Mathf.FloorToInt(height * 0.7f);
            int skyHeight = Mathf.FloorToInt(height * 0.95f);

            WorldState worldState = new WorldState(width, height, skyHeight, undergroundHeight, surfaceHeight);
            
            

            for (int x = 0; x < width; x++)
            {
                int dirtHeight = Mathf.FloorToInt(surfaceHeight + Amplitude * Mathf.PerlinNoise(x / Scale * Frequency, 0) - Amplitude / 2);

                int stoneHeight = Mathf.FloorToInt(undergroundHeight + Amplitude * Mathf.PerlinNoise(x / Scale * Frequency, 10000) - Amplitude / 2);

                for (int j = 0; j < stoneHeight; j++)
                {
                    worldState.SetBlock(new Vector2Int(x, j), new Block(StoneBlockType));
                }

                for (int j = stoneHeight; j < dirtHeight; j++)
                {
                    worldState.SetBlock(new Vector2Int(x, j), new Block(DirtBlockType));
                }
            }

            for (int x = 0; x < width; x++)
            {
                worldState.SetBlock(new Vector2Int(x, height - 1), new Block(DirtBlockType));
            }
            
            
            Vector2Int spawnPoint = new Vector2Int(width / 2, surfaceHeight);

            Block spawnBlock = worldState.GetBlock(spawnPoint);
            if (spawnBlock != null)
            {
                for (int y = spawnPoint.y; y < height; y++)
                {
                    Block blockAbove = worldState.GetBlock(new Vector2Int(spawnPoint.x, y));
                    if (blockAbove == null)
                    {
                        spawnPoint = new Vector2Int(spawnPoint.x, y);
                        break;
                    }
                }
            }

            worldState.SpawnPoint = spawnPoint;
            
            return worldState;
        }
    }
}