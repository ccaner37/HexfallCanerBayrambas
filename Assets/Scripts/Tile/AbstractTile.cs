using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexagon.Tile
{
    public abstract class AbstractTile : MonoBehaviour
    {
        public Vector2 Coordinates { get; private set; }

        public int Score = 5;

        protected const float _moveDuration = 0.05f;

        public virtual void SetProperties(Vector2 coordinates)
        {
            Coordinates = coordinates;

            // Add to dictionary map
            if (TileMap.AllTilesMap.ContainsKey(coordinates))
            {
                TileMap.AllTilesMap[coordinates] = this;
            }
            else
            {
                TileMap.AllTilesMap.Add(coordinates, this);
            }

            transform.name = $"({coordinates.x}), ({coordinates.y})";
        }

        public virtual void ExplodeTile() { }
        public virtual void CheckDownCoordinate() { }
    }
}
