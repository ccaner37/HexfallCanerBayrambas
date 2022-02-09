using Hexagon.Tile;
using System.Collections;
using UnityEngine;

namespace Hexagon.Board
{
    public class TileMover : MonoBehaviour
    {
        //private void OnEnable() => HexagonTile.OnTileExplode += MoveTiles;
        //private void OnDisable() => HexagonTile.OnTileExplode -= MoveTiles;

        public static TileMover Instance { get; private set; }

        private BoardCreator _board;

        private void Awake() => Instance = this;

        private void Start() => _board = gameObject.GetComponent<BoardCreator>();

        public void MoveAllTilesInSameRow(Vector2 explodedTileCoordinate)
        {
            StartCoroutine(TriggerExplodedRowTiles(explodedTileCoordinate));
        }

        private IEnumerator TriggerExplodedRowTiles(Vector2 explodedTileCoordinate)
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
