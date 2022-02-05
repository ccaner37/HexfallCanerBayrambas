using Hexagon.Interfaces;
using Hexagon.Tile.Direction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexagon.Tile
{
    public class HexagonTile : AbstractTile, IInteractable
    {
        public static Action<Vector2, Vector2, Vector2> OnTileInteract;
        public void ClickInteract(Vector2 inputPosition)
        {
            OnTileInteract(transform.position, inputPosition, Coordinates);
        }
    }
}
