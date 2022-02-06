using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexagon.Tile.Direction;

namespace Hexagon.Tile.Neighbor
{
    public static class TileNeighborSelector
    {
        private static Dictionary<TileNeighborDirections, int> _neighborDirectionDictionary = new Dictionary<TileNeighborDirections, int>()
        {
            {TileNeighborDirections.SE, 0 },
            {TileNeighborDirections.NE, 1 },
            {TileNeighborDirections.N, 2 },
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

        public static List<AbstractTile> _selectedTiles = new List<AbstractTile>();

        public static void SelectNeighbors(Vector2 selectedTileCoordinate, CornerDirections direction)
        {
            // This is for even or row. Variable will be 0 if selectedTileCoordinate.x is even, returns 1 for odd (Bitwise operator)
            int parity = (int)selectedTileCoordinate.x & 1;

            _selectedTiles.Clear();
            _selectedTiles.Add(TileMap.AllTilesMap[selectedTileCoordinate]);

            switch (direction)
            {
                case CornerDirections.E:
                    SelectNeighbors(selectedTileCoordinate, parity, 
                    TileNeighborDirections.SE, TileNeighborDirections.NE);
                    break;
                case CornerDirections.W:
                    SelectNeighbors(selectedTileCoordinate, parity,
                    TileNeighborDirections.SW, TileNeighborDirections.NW);
                    break;
                case CornerDirections.NE:
                    SelectNeighbors(selectedTileCoordinate, parity,
                    TileNeighborDirections.N, TileNeighborDirections.NE);
                    break;
                case CornerDirections.NW:
                    SelectNeighbors(selectedTileCoordinate, parity,
                    TileNeighborDirections.N, TileNeighborDirections.NW);
                    break;
                case CornerDirections.SE:
                    SelectNeighbors(selectedTileCoordinate, parity,
                    TileNeighborDirections.S, TileNeighborDirections.SE);
                    break;
                case CornerDirections.SW:
                    SelectNeighbors(selectedTileCoordinate, parity,
                    TileNeighborDirections.S, TileNeighborDirections.SW);
                    break;
                default:
                    break;
            }
        }

        private static void SelectNeighbors(Vector2 selectedTileCoordinate, int parity, TileNeighborDirections firstDirection, TileNeighborDirections secondDirection)
        {
            var firstNeighbor = _neighborComboList[parity][_neighborDirectionDictionary[firstDirection]];
            var secondNeighbor = _neighborComboList[parity][_neighborDirectionDictionary[secondDirection]];

            _selectedTiles.Add(TileMap.AllTilesMap[selectedTileCoordinate + firstNeighbor]);
            _selectedTiles.Add(TileMap.AllTilesMap[selectedTileCoordinate + secondNeighbor]);
        }
    }
}
