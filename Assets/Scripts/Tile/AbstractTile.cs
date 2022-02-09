using DG.Tweening;
using Hexagon.Board;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexagon.Tile
{
    public abstract class AbstractTile : MonoBehaviour
    {
        public Vector2 Coordinates { get; private set; }

        public int Score = 5;

        protected const float _moveDuration = 0.05f;

        public static Action<Vector2, Vector2, Vector2> OnTileInteract;

        public static Action<Vector2> OnTileExplode;

        private bool _isTileInCorrectPosition => (Vector2)transform.localPosition == TileCoordinatePositionHelper.GetLocalPosition(Coordinates);

        private void Start()
        {
            if (!_isTileInCorrectPosition)
            {
                MoveTileToCoordinatePosition();
            }
        }

        public virtual void SetProperties(Vector2 coordinates)
        {
            Coordinates = coordinates;

            // Add to dictionary map
            if (TileMap.AllTilesMap.ContainsKey(coordinates))
            {
                TileMap.AllTilesMap[coordinates] = this;
            }
            else
            {
                TileMap.AllTilesMap.Add(coordinates, this);
            }

            transform.name = $"({coordinates.x}), ({coordinates.y})";
        }

        public virtual void ClickInteract(Vector2 inputPosition)
        {
            OnTileInteract(transform.position, inputPosition, Coordinates);
        }

        public virtual void ExplodeTile() 
        {
            if (!gameObject) return;
            Destroy(gameObject);
            OnTileExplode(Coordinates);
            TileMover.Instance.MoveAllTilesInSameRow(Coordinates);
        }

        private void MoveTileToCoordinatePosition()
        {
            transform.DOLocalMove(TileCoordinatePositionHelper.GetLocalPosition(Coordinates), _moveDuration);
            CheckDownCoordinate();
        }

        public virtual void CheckDownCoordinate() 
        {
            Vector2 downTileCoordinate = new Vector2(Coordinates.x, Coordinates.y + 1);
            AbstractTile downTile;
            TileMap.AllTilesMap.TryGetValue(downTileCoordinate, out downTile);
            if (downTile == null)
            {
                MoveToDownCoordinate(downTileCoordinate);
            }
        }

        public virtual void MoveToDownCoordinate(Vector2 downTileCoordinate)
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
