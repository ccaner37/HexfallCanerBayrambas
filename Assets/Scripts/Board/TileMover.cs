using Hexagon.Tile;
using System.Collections;
using UnityEngine;

namespace Hexagon.Board
{
    public class TileMover : MonoBehaviour
    {
        private BoardCreator _board;

        private void OnEnable() => AbstractTile.OnBoardChanged += MoveAllTilesInSameRow;
        private void OnDisable() => AbstractTile.OnBoardChanged -= MoveAllTilesInSameRow;

        private void Start() => _board = gameObject.GetComponent<BoardCreator>();

        public void MoveAllTilesInSameRow(Vector2 explodedTileCoordinate)
        {
            StartCoroutine(TriggerExplodedColumnTilesCoroutine(explodedTileCoordinate));
        }

        // Triggers exploded column's tiles for check down coordinate, if tile's down is empty then it'll move.
        private IEnumerator TriggerExplodedColumnTilesCoroutine(Vector2 explodedTileCoordinate)
        {
            for (int i = 0; i < _board.BoardSettings.Height - 1; i++)
            {
                yield return null;

                Vector2 currentTileCoordinate = new Vector2(explodedTileCoordinate.x, i);
                AbstractTile currentTile;
                TileMap.AllTilesMap.TryGetValue(currentTileCoordinate, out currentTile);
                if (currentTile != null)
                {
                    currentTile.CheckDownCoordinate();
                }
            }
        }
    }
}
