using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hexagon.Managers;
using Hexagon.Enums;

namespace Hexagon.Tile.Neighbor
{
    public class TileNeighborChecker : MonoBehaviour
    {
        public static Action OnTileMatch;

        private const float _explodeWaitDuration = 0.1f;

        private static int explodedTilesTotalScore = 0;

        public static IEnumerator CheckNeighborsCoroutine(Vector2 tileCoordinate)
        {
            var allCornerDirections = CornerDirections.GetValues(typeof(CornerDirections));

            foreach (CornerDirections cornerDirection in allCornerDirections)
            {
                var neighborList = new List<AbstractTile>();
                TileNeighborSelector.SelectNeighborsIntoList(tileCoordinate, cornerDirection, neighborList);

                if (!CheckNullErrors(neighborList)) continue;

                bool isColorsSame = 
                    GetColor(neighborList[0]) == GetColor(neighborList[1]) && 
                    GetColor(neighborList[1]) == GetColor(neighborList[2]) && 
                    GetColor(neighborList[0]) == GetColor(neighborList[2]);

                bool isCoordinatesRight =
                    neighborList[0].Coordinates != neighborList[1].Coordinates &&
                    neighborList[1].Coordinates != neighborList[2].Coordinates &&
                    neighborList[0].Coordinates != neighborList[2].Coordinates;

                
                if (isColorsSame && isCoordinatesRight)
                {
                    OnTileMatch?.Invoke();
                    for (int i = 0; i < neighborList.Count; i++) // Explode matched tiles & count score
                    {
                        yield return new WaitForSeconds(_explodeWaitDuration);
                        if (neighborList[i] == null) continue;

                        explodedTilesTotalScore += neighborList[i].Score;
                        neighborList[i].ExplodeTile();
                    }
                }
                GiveScore();
            }
        }

        private static Color GetColor(AbstractTile tile)
        {
            if (tile == null) return Color.red;
            return tile.GetComponent<SpriteRenderer>().color;
        }

        private static bool CheckNullErrors(List<AbstractTile> neighborList)
        {
            if (neighborList == null) return false;
            if (neighborList.Count != 3) return false;
            if (neighborList[0] == null) return false;
            if (neighborList[1] == null) return false;
            if (neighborList[2] == null) return false;
            if (neighborList[0].Coordinates == null) return false;
            if (neighborList[1].Coordinates == null) return false;
            if (neighborList[2].Coordinates == null) return false;

            return true;
        }

        private static void GiveScore()
        {
            GameManager.TotalScore += explodedTilesTotalScore;
            explodedTilesTotalScore = 0;
        }
    }
}
