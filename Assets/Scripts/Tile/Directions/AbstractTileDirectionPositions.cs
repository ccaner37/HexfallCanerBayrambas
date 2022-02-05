using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexagon.Tile
{
    public abstract class AbstractTileDirectionPositions : MonoBehaviour
    {
        protected const float Y_RADIUS = 0.37f;
        protected const float X_RADIUS = 0.37f;

        protected Vector2 _east = new Vector2(X_RADIUS, 0);
        protected Vector2 _west = new Vector2(-X_RADIUS, 0);
        protected Vector2 _northEast = new Vector2(X_RADIUS * 0.7f, Y_RADIUS);
        protected Vector2 _northWest = new Vector2(-X_RADIUS * 0.7f, Y_RADIUS);
        protected Vector2 _southEast = new Vector2(X_RADIUS * 0.7f, -Y_RADIUS);
        protected Vector2 _southWest = new Vector2(-X_RADIUS * 0.7f, -Y_RADIUS);
    }
}
