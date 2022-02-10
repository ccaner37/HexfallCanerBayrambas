using System;
using UnityEngine;
using DG.Tweening;
using Hexagon.Board;

namespace Hexagon.Tile
{
    public abstract class AbstractTile : MonoBehaviour
    {
        public Vector2 Coordinates { get; private set; }

        public int Score = 5;

        public static Action<Vector2, Vector2, Vector2> OnTileInteract;

        public static Action<Vector2> OnTileExplode;

        public static Action<Vector2> OnBoardChanged;

        protected const float _moveDuration = 0.05f;

        protected bool _isTileInCorrectPosition => (Vector2)transform.localPosition == TileCoordinatePositionHelper.GetLocalPosition(Coordinates);

        //DoTween Effect Variables
        protected bool _isShaking;
        protected float _shakeDuration = 0.2f;
        protected float _shakeStrength = 0.02f;
        protected int _shakeVibrato = 1;
        protected int _shakeRandomness = 5;

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
            bool isInDictionary = TileMap.AllTilesMap.ContainsKey(coordinates);
            if (isInDictionary)
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
            Destroy(gameObject);
            OnTileExplode(Coordinates);
            OnBoardChanged(Coordinates);
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
            transform.DOLocalMove(downTilePosition, _moveDuration)
                .OnComplete(() =>DoShakeEffect());

            TileMap.AllTilesMap.Remove(Coordinates);
            SetProperties(downTileCoordinate);
            OnBoardChanged(Coordinates);
        }

        private void DoShakeEffect()
        {
            if (_isShaking) return;
            _isShaking = true;

            transform.DOShakeScale(_shakeDuration, _shakeStrength, _shakeVibrato, _shakeRandomness)
                .OnComplete(() => _isShaking = false);
        }
    }
}
