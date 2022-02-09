using Hexagon.Interfaces;
using System;
using UnityEngine;
using DG.Tweening;
using Hexagon.Board;
using System.Collections;
using Hexagon.Tile.Neighbor;

namespace Hexagon.Tile
{
    public class HexagonTile : AbstractTile, IInteractable
    {
        public static Action<Vector2, Vector2, Vector2> OnTileInteract;

        public static Action<Vector2> OnTileExplode;

        private bool _isTileInCorrectPosition => (Vector2)transform.localPosition == TileCoordinatePositionHelper.GetLocalPosition(Coordinates);

        public void ClickInteract(Vector2 inputPosition)
        {
            OnTileInteract(transform.position, inputPosition, Coordinates);
        }

        private void Start()
        {
            if (!_isTileInCorrectPosition)
            {
                MoveTileToCoordinatePosition();
            }
        }

        private void MoveTileToCoordinatePosition()
        {
            transform.DOLocalMove(TileCoordinatePositionHelper.GetLocalPosition(Coordinates), _moveDuration);
            CheckDownCoordinate();
        }

        public override void ExplodeTile()
        {
            if (!gameObject) return; 
            Destroy(gameObject);
            OnTileExplode(Coordinates);
            TileMover.Instance.MoveAllTilesInSameRow(Coordinates);
        }

        public override void CheckDownCoordinate()
        {
            Vector2 downTileCoordinate = new Vector2(Coordinates.x, Coordinates.y + 1);
            AbstractTile downTile;
            TileMap.AllTilesMap.TryGetValue(downTileCoordinate, out downTile);
            if(downTile == null)
            {
                MoveToDownCoordinate(downTileCoordinate);
            }
        }

        private void MoveToDownCoordinate(Vector2 downTileCoordinate)
        {
            Vector2 downTilePosition = TileCoordinatePositionHelper.GetLocalPosition(downTileCoordinate);
            transform.DOLocalMove(downTilePosition, _moveDuration).OnComplete(() =>
            {
                TileMap.AllTilesMap.Remove(Coordinates);
                SetProperties(downTileCoordinate);
                TileMover.Instance.MoveAllTilesInSameRow(Coordinates);
            });
        }
    }
 }
