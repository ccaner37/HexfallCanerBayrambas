using Hexagon.Tile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Hexagon.Tile.Neighbor;

namespace Hexagon.Board
{
    public class TileSpawner : MonoBehaviour
    {
        private BoardCreator _board;

        private float _waitTime = 0.5f;

        private void OnEnable() => HexagonTile.OnTileExplode += SpawnNewTile;
        private void OnDisable() => HexagonTile.OnTileExplode -= SpawnNewTile;

        private void Start() => _board = gameObject.GetComponent<BoardCreator>();

        private void SpawnNewTile(Vector2 explodedTileCoordinate)
        {
            StartCoroutine(SpawnNewTileCoroutine(explodedTileCoordinate));
        }

        private IEnumerator SpawnNewTileCoroutine(Vector2 explodedTileCoordinate)
        {
            _waitTime += 0.5f;
            yield return new WaitForSeconds(_waitTime);

            GameObject tile = Instantiate(_board.HexPrefab, new Vector2(0, 6), Quaternion.identity, _board.transform);

            Vector2 position = TileCoordinatePositionHelper.GetLocalPosition(explodedTileCoordinate);
            tile.transform.localPosition = new Vector2(position.x, 5);
            _board.SetTileCoordinates(tile, (int)explodedTileCoordinate.x, 0);
            _board.SetTileColor(tile);

            CheckAllTilesNeighbor();

            if (_waitTime >= 2f)
            {
                _waitTime = 0.5f;
            }
        }

        public void CheckAllTilesNeighbor()
        {
            foreach (var item in TileMap.AllTilesMap)
            {
                StartCoroutine(TileNeighborChecker.CheckNeighbors(item.Key));
                //Debug.Log(item.Key);
            }
        }
    }
}
