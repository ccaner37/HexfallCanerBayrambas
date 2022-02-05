using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexagon.Tile.Direction;

namespace Hexagon.Tile.Neighbor
{
    public static class TileNeighborSelector
    {
        private static List<AbstractTile> _selectedTiles = new List<AbstractTile>();

        public static void SelectNeighbors(Vector2 selectedTileCoordinate, CornerDirections direction)
        {
            // ResetCOlorDebug();
            _selectedTiles.Clear();
            _selectedTiles.Add(TileMap.AllTilesMap[selectedTileCoordinate]);
            switch (direction)
            {
                case CornerDirections.E:
                    SelectEastNeighbors(selectedTileCoordinate);
                    break;
                case CornerDirections.W:
                    break;
                case CornerDirections.NE:
                    break;
                case CornerDirections.NW:
                    break;
                case CornerDirections.SE:
                    break;
                case CornerDirections.SW:
                    break;
                default:
                    break;
            }
        }

        private static void SelectEastNeighbors(Vector2 selectedTileCoordinate)
        {
            var parity = (int)selectedTileCoordinate.x & 1;
            var gel = lists[parity][dict[TileNeighborDirections.SE]];
            var gel2 = lists[parity][dict[TileNeighborDirections.NE]];
            _selectedTiles.Add(TileMap.AllTilesMap[selectedTileCoordinate + gel]);
            _selectedTiles.Add(TileMap.AllTilesMap[selectedTileCoordinate + gel2]);

            for (int i = 0; i < _selectedTiles.Count; i++)
            {
                _selectedTiles[i].transform.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

        private static void ResetCOlorDebug()
        {
            for (int i = 0; i < _selectedTiles.Count; i++)
            {
                if (_selectedTiles[i].transform == null) return;
                _selectedTiles[i].transform.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }

        static Dictionary<TileNeighborDirections, int> dict = new Dictionary<TileNeighborDirections, int>()
        {
            {TileNeighborDirections.SE, 0 },
            {TileNeighborDirections.NE, 1 },
            {TileNeighborDirections.N, 2 },
            {TileNeighborDirections.NW, 3 },
            {TileNeighborDirections.SW, 4 },
            {TileNeighborDirections.S, 5 },
        };

        static Vector2[] evenCols = new Vector2[6] 
        { 
            new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, -1), 
            new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(0, +1) 
        };

        static Vector2[] rowCols = new Vector2[6]
        {
            new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, -1),
            new Vector2(-1, 0), new Vector2(-1, +1), new Vector2(0, +1)
        };

        static Vector2[][] lists = new Vector2[][] {evenCols, rowCols};
    }
}
