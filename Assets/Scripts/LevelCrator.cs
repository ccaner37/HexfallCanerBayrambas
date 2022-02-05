using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexagon.Tile;

namespace Hexagon
{
	public class LevelCrator : MonoBehaviour
	{
		public GameObject hexPrefab;

		[SerializeField]
		private int _width = 8;
		[SerializeField]
		private int _height = 9;

		private float oddRowYOffset = 0.48f;
		private float oddRowXOffset = -0.20f;

		[SerializeField] private float _distanceBetweenVertical, _distanceBetweenHorizontal; 

		void Start()
		{
			CreateHexagons();
		}

		void CreateHexagons()
		{
			for (int x = 0; x < _width; x++)
			{
				for (int y = 0; y < _height; y++)
				{

					float xPos = x;
					float yPos = y;

					//if (y % 2 == 0)
					//{
					//	yPos += oddRowYOffset;
					//}

					if (x % 2 == 1)
					{
						xPos += oddRowXOffset;
						yPos += oddRowYOffset;
					}
					else if (x % 2 == 0)
					{
						xPos += oddRowXOffset;
					}
					GameObject hexagon = Instantiate(hexPrefab, new Vector2(xPos * _distanceBetweenVertical, yPos * -_distanceBetweenHorizontal), Quaternion.identity, transform);
					//hexagon.GetComponent<HexagonTile>().Coordinates;
					//Color newColor = colors[Random.Range(0, colors.Count)];
					//hexagon.GetComponent<SpriteRenderer>().color = newColor;
					Vector2 hamut = new Vector2(x, y);
					hexagon.GetComponent<AbstractTile>().SetProperties(hamut);
					TileMap.AllTilesMap.Add(hamut, hexagon.GetComponent<HexagonTile>());
					hexagon.name = "(" + x + ", " + y + ")";

					//Tile spawnedTile = hexagon.GetComponent<Tile>();
					//spawnedTile.SetCoordinates(x, y);

				}
			}

			transform.position = new Vector3(-1.94f, 1.70f, 0);
			//transform.rotation = new Quaternion(180, 0, 0, 0);
		}
	}
}
