using UnityEngine;

namespace Hexagon.ScriptableObjects
{
    [CreateAssetMenu(menuName = "HexagonGame/Board")]
    public class ScriptableBoard : ScriptableObject
    {
        public int Width = 8;
        public int Height = 9;

        public float OddRowYOffset = 0.48f;
        public float OddRowXOffset = -0.20f;

        public float VerticalDistance = 0.74f;
        public float HorizontalDistance = 0.62f;

        public int BombSpawningScore = 200;

        public Color[] TileColors;
    }
}
