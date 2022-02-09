using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexagon.Tile;
using Hexagon.ScriptableObjects;

namespace Hexagon.Board
{
	public class BoardCreator : MonoBehaviour
	{
		public GameObject HexPrefab;

		public ScriptableBoard BoardSettings;

		private Vector3 _boardPosition = new Vector3(-2.11f, 2.8f, 0);

		private void Start()
		{
			InitializeBoard();
		}

		private bool IsOddRow(int x) => x % 2 == 1;
		private bool IsEvenRow(int x) => x % 2 == 0;

		private void InitializeBoard()
		{
			for (int x = 0; x < BoardSettings.Width; x++)
			{
				for (int y = 0; y < BoardSettings.Height; y++)
				{
					float xPos = x;
					float yPos = y;

					if (IsOddRow(x))
					{
						xPos += BoardSettings.OddRowXOffset;
						yPos += BoardSettings.OddRowYOffset;
					}
					else if (IsEvenRow(x))
					{
						xPos += BoardSettings.OddRowXOffset;
					}

					Vector2 tilePosition = new Vector2(xPos * BoardSettings.HorizontalDistance, yPos * -BoardSettings.VerticalDistance);
					GameObject tile = Instantiate(HexPrefab, tilePosition, Quaternion.identity, transform);

					SetTileCoordinates(tile, x, y);
					SetTileColor(tile);
				}
			}
			FixBoardPosition();
		}

		public virtual void SetTileCoordinates(GameObject spawnedTile, int x, int y)
        {
			Vector2 coordinates = new Vector2(x, y);

			var tile = spawnedTile.GetComponent<AbstractTile>();
			tile.SetProperties(coordinates);
		}

		public virtual void SetTileColor(GameObject tile)
        {
			int randomNumber = Random.Range(0, BoardSettings.TileColors.Length);
			Color randomColor = BoardSettings.TileColors[randomNumber];

			tile.GetComponent<SpriteRenderer>().color = randomColor;
        }

		private void FixBoardPosition() => transform.position = _boardPosition;
	}
}
