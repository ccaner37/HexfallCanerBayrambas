using Hexagon.Tile.Neighbor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexagon.Tile.Direction
{
    public struct DirectionsWithPositions
    {
        public Vector2 Position;
        public CornerDirections Direction;
        public DirectionsWithPositions(Vector2 directionPos, Vector2 tilePos, CornerDirections direction)
        {
            this.Position = directionPos + tilePos;
            this.Direction = direction;
        }
    }

    public class TileDirectionSelector : AbstractTileDirectionPositions
    {
        private DirectionsWithPositions[] _directionsWithPositionsArray;

        private DirectionsWithPositions _selectedDirectionAndPosition;

        private TileDirectionSpriteController _tileDirectionSpriteController;

        private void OnEnable() => HexagonTile.OnTileInteract += HandleTileInteract;
        private void OnDisable() => HexagonTile.OnTileInteract -= HandleTileInteract;

        private void Start() => _tileDirectionSpriteController = GetComponent<TileDirectionSpriteController>();

        private void HandleTileInteract(Vector2 selectedTilePosition, Vector2 inputPosition, Vector2 selectedTileCoordinate)
        {
            SetDirectionPositions(selectedTilePosition);
            SelectTileCorner(inputPosition);
            TileNeighborSelector.SelectNeighborsIntoList(selectedTileCoordinate, _selectedDirectionAndPosition.Direction, TileNeighborSelector.SelectedSwapTiles);
        }

        private void SetDirectionPositions(Vector2 tilePosition)
        {
            _directionsWithPositionsArray = new DirectionsWithPositions[]
            {
                new DirectionsWithPositions(_east, tilePosition, CornerDirections.E),
                new DirectionsWithPositions(_west, tilePosition, CornerDirections.W),
                new DirectionsWithPositions(_northEast, tilePosition, CornerDirections.NE),
                new DirectionsWithPositions(_northWest, tilePosition, CornerDirections.NW),
                new DirectionsWithPositions(_southEast, tilePosition, CornerDirections.SE),
                new DirectionsWithPositions(_southWest, tilePosition, CornerDirections.SW),
            };
        }

        private void SelectTileCorner(Vector2 inputPosition)
        {
            float distance = Mathf.Infinity;
            for (int i = 0; i < _directionsWithPositionsArray.Length; i++)
            {
                var directionsWithPositions = _directionsWithPositionsArray[i];
                if (Vector2.Distance(inputPosition, directionsWithPositions.Position) < distance)
                {
                    distance = Vector2.Distance(inputPosition, directionsWithPositions.Position);
                    _selectedDirectionAndPosition = directionsWithPositions;
                }
            }
            _tileDirectionSpriteController.PlaceCircleSprite(_selectedDirectionAndPosition);
        }
    }
}
