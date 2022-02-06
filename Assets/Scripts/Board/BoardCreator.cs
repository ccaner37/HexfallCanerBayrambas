using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexagon.Tile;
using Hexagon.ScriptableObjects;

namespace Hexagon.Board
{
	public class BoardCreator : MonoBehaviour
	{
		[SerializeField]
		private GameObject _hexPrefab;

		[SerializeField]
		private ScriptableBoard _boardSettings;

		private Vector3 _boardPosition = new Vector3(-2.01f, 2.36f, 0);

		private void Start()
		{
			InitializeBoard();
		}

		private bool IsOddRow(int x) => x % 2 == 1;
		private bool IsEvenRow(int x) => x % 2 == 0;

		private void InitializeBoard()
		{
			for (int x = 0; x < _boardSettings.Width; x++)
			{
				for (int y = 0; y < _boardSettings.Height; y++)
				{
					float xPos = x;
					float yPos = y;

					if (IsOddRow(x))
					{
						xPos += _boardSettings.OddRowXOffset;
						yPos += _boardSettings.OddRowYOffset;
					}
					else if (IsEvenRow(x))
					{
						xPos += _boardSettings.OddRowXOffset;
					}

					Vector2 tilePosition = new Vector2(xPos * _boardSettings.HorizontalDistance, yPos * -_boardSettings.VerticalDistance);
					GameObject tile = Instantiate(_hexPrefab, tilePosition, Quaternion.identity, transform);

					SetTileCoordinates(tile, x, y);
					SetTileColor(tile);
				}
			}
			FixBoardPosition();
		}

		private void SetTileCoordinates(GameObject spawnedTile, int x, int y)
        {
			Vector2 coordinates = new Vector2(x, y);

			var tile = spawnedTile.GetComponent<AbstractTile>();
			tile.SetProperties(coordinates);

			TileMap.AllTilesMap.Add(coordinates, tile);
		}

		private void SetTileColor(GameObject tile)
        {
			int randomNumber = Random.Range(0, _boardSettings.TileColors.Length);
			Color randomColor = _boardSettings.TileColors[randomNumber];

			tile.GetComponent<SpriteRenderer>().color = randomColor;
        }

		private void FixBoardPosition() => transform.position = _boardPosition;
	}
}
