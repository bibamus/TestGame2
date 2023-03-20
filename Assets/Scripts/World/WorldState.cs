using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace World
{
    public enum LayerType
    {
        Underworld,
        Underground,
        Surface,
        Sky
    }

    public class WorldState
    {
        public int Width { get; }
        public int Height { get; }


        private int UndergroundHeight { get; }
        private int SurfaceHeight { get; }
        private int SkyHeight { get; }

        private readonly Block[,] _blocks;

        public Vector2Int SpawnPoint { get; set; }


        public WorldState(int width, int height, int skyHeight, int undergroundHeight, int surfaceHeight)
        {
            Width = width;
            Height = height;

            _blocks = new Block[Width, Height];

            SkyHeight = skyHeight;
            UndergroundHeight = undergroundHeight;
            SurfaceHeight = surfaceHeight;
        }


        public Block GetBlock(Vector2Int position)
        {
            return IsValidPosition(position) ? _blocks[position.x, position.y] : null;
        }

        public void SetBlock(Vector2Int position, Block block)
        {
            if (IsValidPosition(position))
            {
                _blocks[position.x, position.y] = block;
            }
        }

        public void RemoveBlock(Vector2Int position)
        {
            if (IsValidPosition(position))
            {
                _blocks[position.x, position.y] = null;
            }
        }

        private bool IsValidPosition(Vector2Int position)
        {
            return position.x >= 0 && position.x < Width &&
                   position.y >= 0 && position.y < Height;
        }

        public LayerType GetLayerForCoordinate(int y)
        {
            if (y < 0 || y >= Height)
            {
                throw new ArgumentOutOfRangeException(nameof(y), $"The provided y-coordinate ({y}) is out of range.");
            }

            if (y > SkyHeight)
            {
                return LayerType.Sky;
            }

            if (y > SurfaceHeight)
            {
                return LayerType.Surface;
            }

            if (y > UndergroundHeight)
            {
                return LayerType.Underground;
            }

            return LayerType.Underworld;
        }
    }
}