using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexagon.Tile
{
    public static class TileMap
    {
        //static List<TileProperties> AllTilesList = new List<TileProperties>(); // Column * row
        public static Dictionary<Vector2, AbstractTile> AllTilesMap = new Dictionary<Vector2, AbstractTile>();
    }
}
