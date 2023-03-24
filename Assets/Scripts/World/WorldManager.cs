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


        public WorldState State { get; private set; }

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
            State = worldGenerator.GenerateWorld(worldWidth, worldHeight);
            UpdateWorldTilemap();
        }

        private void Update()
        {
            UpdateWorldTilemap();
        }

        private void UpdateWorldTilemap()
        {
            for (int x = 0; x < State.Width; x++)
            {
                for (int y = 0; y < State.Height; y++)
                {
                    Block block = State.GetBlock(new Vector2Int(x, y));
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
            return State.SpawnPoint;
        }
        
        public Vector3 GetSpawnPositionWorld()
        {
            // Convert tilemap coordinates to world coordinates
            return worldTilemap.CellToWorld((Vector3Int)State.SpawnPoint) + new Vector3(0.5f, 1f, 0f);
        }

        public Block GetBlockAtCoordinates(Vector2 coordinates)
        {
            var worldToCell = worldTilemap.WorldToCell(coordinates);
            var blockCoords = new Vector2Int(worldToCell.x, worldToCell.y);
            return State.GetBlock(blockCoords);
        }

        public bool HasBlockAtCoordinates(Vector2 coordinates) => GetBlockAtCoordinates(coordinates) != null;
    }
}