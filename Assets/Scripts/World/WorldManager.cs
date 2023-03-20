using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace World
{
    [DefaultExecutionOrder(-1000)]
    public class WorldManager : MonoBehaviour
    {
        public int worldWidth = 1000;
        public int worldHeight = 400;

        public BlockType dirtBlockType;
        public BlockType stoneBlockType;
        public BlockType sandBlockType;

        public Tilemap worldTilemap;

        public float scale = 10.0f;
        public float amplitude = 50.0f;
        public float frequency = 0.1f;


        public WorldState _worldState;

        private void Start()
        {
            worldTilemap.ClearAllTiles();
            WorldGenerator worldGenerator = new WorldGenerator
            {
                DirtBlockType = dirtBlockType,
                StoneBlockType = stoneBlockType,
                Scale = scale,
                Amplitude = amplitude,
                Frequency = frequency,
                DirtBaseHeight = Mathf.FloorToInt(worldHeight * 0.7f),
                StoneBaseHeight = Mathf.FloorToInt(worldHeight * 0.4f)
            };
            _worldState = worldGenerator.GenerateWorld(worldWidth, worldHeight);
            UpdateWorldTilemap();
        }

        private void Update()
        {
            UpdateWorldTilemap();
        }

        private void UpdateWorldTilemap()
        {
            for (int x = 0; x < _worldState.Width; x++)
            {
                for (int y = 0; y < _worldState.Height; y++)
                {
                    Block block = _worldState.GetBlock(new Vector2Int(x, y));
                    if (block != null)
                    {
                        worldTilemap.SetTile(new Vector3Int(x, y, 0), block.Type.Tile);
                    }
                    else
                    {
                        worldTilemap.SetTile(new Vector3Int(x, y, 0), null);
                    }
                }
            }
        }

        public Vector2 GetSpawnPosition()
        {
            return _worldState.SpawnPoint;
        }
        
        public Vector3 GetSpawnPositionWorld()
        {
            // Convert tilemap coordinates to world coordinates
            return worldTilemap.CellToWorld((Vector3Int)_worldState.SpawnPoint) + new Vector3(0.5f, 1f, 0f);
        }
    }
}