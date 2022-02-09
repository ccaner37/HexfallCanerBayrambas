using Hexagon.Tile;
using System.Collections;
using UnityEngine;
using Hexagon.Tile.Neighbor;
using Hexagon.Manager;

namespace Hexagon.Board
{
    public class TileSpawner : MonoBehaviour
    {
        private BoardCreator _board;

        private float _waitTime = 0.5f;

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
            _waitTime += 0.5f;
            yield return new WaitForSeconds(_waitTime);

            GameObject tile = SpawnTile();

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
            }
        }

        private GameObject SpawnTile()
        {
            if (ShouldSpawnBomb())
            {
                return Instantiate(_board.BombPrefab, new Vector2(0, 6), Quaternion.identity, _board.transform);
            }
            else
            {
                return Instantiate(_board.HexPrefab, new Vector2(0, 6), Quaternion.identity, _board.transform);
            }
        }

        private bool ShouldSpawnBomb()
        {
            if (GameManager.Instance.TotalScore > _nextBombSpawnScore)
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
