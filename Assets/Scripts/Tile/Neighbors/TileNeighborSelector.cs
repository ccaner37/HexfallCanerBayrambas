using System.Collections.Generic;
using UnityEngine;
using Hexagon.Enums;
using Hexagon.Board;

namespace Hexagon.Tile.Neighbor
{
    public class TileNeighborSelector
    {
        private static Dictionary<TileNeighborDirections, int> _neighborDirectionDictionary = new Dictionary<TileNeighborDirections, int>()
        {
            {TileNeighborDirections.SE, 0 },
            {TileNeighborDirections.NE, 1 },
            {TileNeighborDirections.N, 2 },  // Numbers presented here are based on _evenCols and _rowCols arrays order
            {TileNeighborDirections.NW, 3 },
            {TileNeighborDirections.SW, 4 },
            {TileNeighborDirections.S, 5 },
        };

        private static Vector2[] _evenCols = new Vector2[6]
        {
            new Vector2(1, 0), new Vector2(1, -1), new Vector2(0, -1),
            new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(0, +1)
        };

        private static Vector2[] _rowCols = new Vector2[6]
        {
            new Vector2(1, 1), new Vector2(1, 0), new Vector2(0, -1),
            new Vector2(-1, 0), new Vector2(-1, +1), new Vector2(0, +1)
        };

        private static Vector2[][] _neighborComboList = new Vector2[][] { _evenCols, _rowCols };

        public static List<AbstractTile> SelectedSwapTiles = new List<AbstractTile>();

        public static void SelectNeighborsIntoList(Vector2 selectedTileCoordinate, CornerDirections direction, List<AbstractTile> list)
        {
            // This is for even or row. Variable will be 0 if selectedTileCoordinate.x is even, returns 1 for odd (Bitwise operator)
            int parity = (int)selectedTileCoordinate.x & 1;

            list.Clear();

            AbstractTile tile;
            TileMap.AllTilesMap.TryGetValue(selectedTileCoordinate, out tile);
            list.Add(tile);

            switch (direction)
            {
                case CornerDirections.E:
                    SelectNeighbors(selectedTileCoordinate, parity, list,
                    TileNeighborDirections.NE, TileNeighborDirections.SE);
                    break;
                case CornerDirections.W:
                    SelectNeighbors(selectedTileCoordinate, parity, list,
                    TileNeighborDirections.NW, TileNeighborDirections.SW);
                    break;
                case CornerDirections.NE:
                    SelectNeighbors(selectedTileCoordinate, parity, list,
                    TileNeighborDirections.N, TileNeighborDirections.NE);
                    break;
                case CornerDirections.NW:
                    SelectNeighbors(selectedTileCoordinate, parity, list,
                    TileNeighborDirections.N, TileNeighborDirections.NW);
                    break;
                case CornerDirections.SE:
                    SelectNeighbors(selectedTileCoordinate, parity, list,
                    TileNeighborDirections.S, TileNeighborDirections.SE);
                    break;
                case CornerDirections.SW:
                    SelectNeighbors(selectedTileCoordinate, parity, list,
                    TileNeighborDirections.S, TileNeighborDirections.SW);
                    break;
                default:
                    break;
            }
        }

        private static void SelectNeighbors(Vector2 selectedTileCoordinate, int parity, List<AbstractTile> list, TileNeighborDirections firstDirection, TileNeighborDirections secondDirection)
        {
            var firstNeighbor = _neighborComboList[parity][_neighborDirectionDictionary[firstDirection]];
            var secondNeighbor = _neighborComboList[parity][_neighborDirectionDictionary[secondDirection]];

            AbstractTile first;
            TileMap.AllTilesMap.TryGetValue(selectedTileCoordinate + firstNeighbor, out first);
            list.Add(first);

            AbstractTile second;
            TileMap.AllTilesMap.TryGetValue(selectedTileCoordinate + secondNeighbor, out second);
            list.Add(second);
        }

        public static void ClearSelectedTilesList(List<AbstractTile> list)
        {
            list.Clear();
        }
    }
}
