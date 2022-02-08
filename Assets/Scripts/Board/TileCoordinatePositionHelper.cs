using Hexagon.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexagon.Board
{
    public class TileCoordinatePositionHelper : MonoBehaviour
    {
    //    public static TileCoordinatePositionHelper Instance { get; private set; }

        private static bool _isEvenRow(int parity) => parity == 0;

        private const float ODD_ROW_Y_DIFFERENCE = -0.3552f;

        private static BoardCreator _board;

       // private void Awake() => Instance = this;

        private void Start() => _board = gameObject.GetComponent<BoardCreator>();

        public static Vector2 GetLocalPosition(Vector2 tileCoordinate)
        {
            // This is for even or row. Variable will be 0 if selectedTileCoordinate.x is even, returns 1 for odd (Bitwise operator)
            int parity = (int)tileCoordinate.x & 1;

            float verticalDistance = _board.BoardSettings.VerticalDistance;
            float horizontalDistance = _board.BoardSettings.HorizontalDistance;

            if (_isEvenRow(parity))
            {
                Vector2 worldPosition = new Vector2(tileCoordinate.x * horizontalDistance, -tileCoordinate.y * verticalDistance);
                return worldPosition;
            }
            else
            {
                Vector2 worldPosition = new Vector2(tileCoordinate.x * horizontalDistance, -tileCoordinate.y * verticalDistance + ODD_ROW_Y_DIFFERENCE);
                return worldPosition;
            }
        }
    }
}
