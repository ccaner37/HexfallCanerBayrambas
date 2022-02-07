using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Hexagon.Tile.Neighbor;

namespace Hexagon.Tile.Swap
{
    public class TileSwapController
    {
        private const float SWAP_DURATION = 0.2f;
        private const float X_SWAP_DURATION = SWAP_DURATION * 0.9f;
        private const float Y_SWAP_DURATION = SWAP_DURATION * 0.8f;

        private static int _swapCount = 3;

        public static bool IsSwapping;

        public static Sequence _swapSequence;

        public static bool stopp;

        public static IEnumerator SwapClockwiseCoroutine()
        {
            if (IsSwapping) yield break;
            IsSwapping = true;

            for (int i = 0; i < _swapCount; i++)
            {
                yield return new WaitForSeconds(SWAP_DURATION);
                for (int j = 0; j < TileNeighborSelector.SelectedSwapTiles.Count; j++)
                {
                    int nextNeighborIndex = j + 1;
                    if (nextNeighborIndex >= 3) nextNeighborIndex = 0;

                    MoveTiles(j, nextNeighborIndex);
                }
            }

            IsSwapping = false;
        }

        public static IEnumerator SwapCounterClockwiseCoroutine()
        {
            if (IsSwapping) yield break;
            IsSwapping = true;

            for (int i = 0; i < _swapCount; i++)
            {
                yield return new WaitForSeconds(SWAP_DURATION);
                for (int j = TileNeighborSelector.SelectedSwapTiles.Count - 1; j >= 0; j--)
                {
                    int nextNeighborIndex = j - 1;
                    if (nextNeighborIndex <= -1) nextNeighborIndex = 2;

                    MoveTiles(j, nextNeighborIndex);
                }
            }

            IsSwapping = false;
        }

        private static void MoveTiles(int currentTileIndex, int nextNeighborIndex)
        {
            if (stopp) return;

            Transform currentTile = TileNeighborSelector.SelectedSwapTiles[currentTileIndex].transform;
            Transform nextNeighbor = TileNeighborSelector.SelectedSwapTiles[nextNeighborIndex].transform;
            Vector2 nextNeighborPosition = nextNeighbor.position;

            _swapSequence = DOTween.Sequence();
            _swapSequence.Join(currentTile.DOMoveX(nextNeighborPosition.x, X_SWAP_DURATION).SetEase(Ease.OutQuad));
            _swapSequence.Join(currentTile.DOMoveY(nextNeighborPosition.y, Y_SWAP_DURATION).SetEase(Ease.InQuad));

            var currentTileProperty = currentTile.GetComponent<AbstractTile>();
            var neighborTileProperty = nextNeighbor.GetComponent<AbstractTile>();

            Vector2 tempCoordinate = neighborTileProperty.Coordinates;

            _swapSequence.OnComplete(() =>
            currentTileProperty.SetProperties(neighborTileProperty.Coordinates)).OnComplete(() =>
            {
                currentTileProperty.SetProperties(tempCoordinate);
                TileMap.AllTilesMap[currentTileProperty.Coordinates] = currentTileProperty;
                TileNeighborChecker.CheckNeighbors(currentTileProperty.Coordinates);
               // Debug.Log(currentTileProperty.Coordinates);
            });
        }
    }
}
