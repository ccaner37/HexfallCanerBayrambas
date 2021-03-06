using System.Collections;
using UnityEngine;
using Hexagon.Tile;
using Hexagon.Tile.Neighbor;
using Hexagon.Managers;

namespace Hexagon.Board
{
    public class TileSpawner : MonoBehaviour
    {
        private BoardCreator _board;

        private float _spawnWaitTime = 0.2f;

        private int _nextBombSpawnScore;

        private void OnEnable() => AbstractTile.OnTileExplode += SpawnNewTile;
        private void OnDisable() => AbstractTile.OnTileExplode -= SpawnNewTile;

        private void Start() 
        { 
            _board = gameObject.GetComponent<BoardCreator>();
            _nextBombSpawnScore = _board.BoardSettings.BombSpawningScore;
        }

        private void SpawnNewTile(Vector2 explodedTileCoordinate)
        {
            StartCoroutine(SpawnNewTileCoroutine(explodedTileCoordinate));
        }

        private IEnumerator SpawnNewTileCoroutine(Vector2 explodedTileCoordinate)
        {
            _spawnWaitTime += 0.2f;
            yield return new WaitForSeconds(_spawnWaitTime);

            GameObject tile = SpawnTile();

            Vector2 position = TileCoordinatePositionHelper.GetLocalPosition(explodedTileCoordinate);
            tile.transform.localPosition = new Vector2(position.x, 5);
            _board.SetTileCoordinates(tile, (int)explodedTileCoordinate.x, 0);
            _board.SetTileColor(tile);

            CheckAllTilesNeighbor();

            if (_spawnWaitTime >= 1f)
            {
                _spawnWaitTime = 0.2f;
            }
        }

        public void CheckAllTilesNeighbor()
        {
            foreach (var item in TileMap.AllTilesMap)
            {
                StartCoroutine(TileNeighborChecker.CheckNeighborsCoroutine(item.Key));
            }
        }

        private GameObject SpawnTile()
        {
            if (ShouldSpawnBomb())
            {
                return Instantiate(_board.BoardSettings.BombPrefab, new Vector2(0, 6), Quaternion.identity, _board.transform);
            }
            else
            {
                return Instantiate(_board.BoardSettings.HexPrefab, new Vector2(0, 6), Quaternion.identity, _board.transform);
            }
        }

        private bool ShouldSpawnBomb()
        {
            if (GameManager.TotalScore > _nextBombSpawnScore)
            {
                _nextBombSpawnScore += _board.BoardSettings.BombSpawningScore;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
