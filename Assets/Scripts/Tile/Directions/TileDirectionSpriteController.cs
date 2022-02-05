using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hexagon.Tile.Direction
{
    public class TileDirectionSpriteController : MonoBehaviour
    {
        [SerializeField] private Transform _circleSpritePrefab;
        private Transform _circleSprite;

        private float _zPos = -0.1f;

        public void PlaceCircleSprite(DirectionsWithPositions selectedDirectionAndPosition)
        {
            if (!_circleSprite) _circleSprite = Instantiate(_circleSpritePrefab);

            Vector3 position = new Vector3(selectedDirectionAndPosition.Position.x, selectedDirectionAndPosition.Position.y, _zPos);
            _circleSprite.position = position;
        }
    }
}
