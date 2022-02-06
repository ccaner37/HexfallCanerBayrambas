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

        public static IEnumerator SwapClockwiseCoroutine()
        {
            if (IsSwapping) yield break;
            IsSwapping = true;

            for (int i = 0; i < _swapCount; i++)
            {
                yield return new WaitForSeconds(SWAP_DURATION);
                for (int j = 0; j < TileNeighborSelector._selectedTiles.Count; j++)
                {
                    int nextNeighborIndex = j + 1;
                    if (nextNeighborIndex >= 3) nextNeighborIndex = 0;

                    Vector2 nextNeighborPosition = TileNeighborSelector._selectedTiles[nextNeighborIndex].transform.position;
                    TileNeighborSelector._selectedTiles[j].transform.DOMoveX(nextNeighborPosition.x, X_SWAP_DURATION).SetEase(Ease.OutQuad);
                    TileNeighborSelector._selectedTiles[j].transform.DOMoveY(nextNeighborPosition.y, Y_SWAP_DURATION).SetEase(Ease.InQuad);
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
                for (int j = TileNeighborSelector._selectedTiles.Count - 1; j >= 0; j--)
                {
                    int nextNeighborIndex = j - 1;
                    if (nextNeighborIndex <= -1) nextNeighborIndex = 2;

                    Vector2 nextNeighborPosition = TileNeighborSelector._selectedTiles[nextNeighborIndex].transform.position;
                    TileNeighborSelector._selectedTiles[j].transform.DOMoveX(nextNeighborPosition.x, X_SWAP_DURATION).SetEase(Ease.OutQuad);
                    TileNeighborSelector._selectedTiles[j].transform.DOMoveY(nextNeighborPosition.y, Y_SWAP_DURATION).SetEase(Ease.InQuad);
                }
            }

            IsSwapping = false;
        }
    }
}
