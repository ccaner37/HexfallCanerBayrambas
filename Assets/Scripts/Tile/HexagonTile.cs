using Hexagon.Interfaces;
using System;
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
