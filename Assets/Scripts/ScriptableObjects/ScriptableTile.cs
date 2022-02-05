using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hexagon.ScriptableObjects
{
    [CreateAssetMenu(menuName = "HexagonGame/Tile")]
    public class ScriptableTile : ScriptableObject
    {
        public Image TileImage;
        public Color[] TileColors;
    }
}
