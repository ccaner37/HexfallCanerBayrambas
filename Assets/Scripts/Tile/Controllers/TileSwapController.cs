using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Hexagon.Tile.Neighbor;
using Hexagon.Managers;
using System.Linq;

namespace Hexagon.Tile.Swap
{
    public class TileSwapController : MonoBehaviour
    {
        private const float SWAP_DURATION = 0.2f;
        private const float X_SWAP_DURATION = SWAP_DURATION * 0.9f;
        private const float Y_SWAP_DURATION = SWAP_DURATION * 0.8f;
        private const float SWAP_COOLDOWN = 0.6f;

        private static int _swapCount = 3;

        public static bool IsSwapping;

        private static Sequence _swapSequence;

        private void OnEnable() => TileNeighborChecker.OnTileMatch += StopSwapping;
        private void OnDisable() => TileNeighborChecker.OnTileMatch -= StopSwapping;

        public IEnumerator SwapClockwiseCoroutine()
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

        public IEnumerator SwapCounterClockwiseCoroutine()
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

        private void MoveTiles(int currentTileIndex, int nextNeighborIndex)
        {
            if (IsNeighborsDontExist()) return;

            Transform currentTile = TileNeighborSelector.SelectedSwapTiles[currentTileIndex].transform;
            Transform nextNeighbor = TileNeighborSelector.SelectedSwapTiles[nextNeighborIndex].transform;
            Vector2 nextNeighborPosition = nextNeighbor.position;

            _swapSequence = DOTween.Sequence();
            _swapSequence.Append(currentTile.DOMoveX(nextNeighborPosition.x, X_SWAP_DURATION).SetEase(Ease.OutQuad));
            _swapSequence.Join(currentTile.DOMoveY(nextNeighborPosition.y, Y_SWAP_DURATION).SetEase(Ease.InQuad));

            var currentTileProperty = currentTile.GetComponent<AbstractTile>();
            var neighborTileProperty = nextNeighbor.GetComponent<AbstractTile>();

            Vector2 tempCoordinate = neighborTileProperty.Coordinates;

            _swapSequence.OnComplete(() =>
            {
                currentTileProperty.SetProperties(neighborTileProperty.Coordinates);
            }).OnComplete(() =>
            {
                currentTileProperty.SetProperties(tempCoordinate);
                StartCoroutine(TileNeighborChecker.CheckNeighborsCoroutine(currentTileProperty.Coordinates));
            });
        }

        private void StopSwapping()
        {
            DOTween.CompleteAll();
            StartCoroutine(EnableSwappingCoroutine());
        }

        private IEnumerator EnableSwappingCoroutine()
        {
            yield return new WaitForSeconds(SWAP_COOLDOWN);

            if (IsSwapping)
            {
                GameManager.MovesCount++;
            }

            TileNeighborSelector.ClearSelectedTilesList(TileNeighborSelector.SelectedSwapTiles);
            IsSwapping = false;
        }

        private bool IsNeighborsDontExist()
        {
            var result = TileNeighborSelector.SelectedSwapTiles.ElementAt(0) == null ||
                         TileNeighborSelector.SelectedSwapTiles.ElementAt(1) == null ||
                         TileNeighborSelector.SelectedSwapTiles.ElementAt(2) == null;
            return result;
        }
    }
}
