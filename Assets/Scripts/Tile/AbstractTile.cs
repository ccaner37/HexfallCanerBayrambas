using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexagon.Tile
{
    public abstract class AbstractTile : MonoBehaviour
    {
        public Vector2 Coordinates { get; private set; }

        public virtual void SetProperties(Vector2 coordinates)
        {
            Coordinates = coordinates;
            transform.name = $"({coordinates.x}), ({coordinates.y})";
        }
    }
}
