using Hexagon.Tile.Swap;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Hexagon.Tile.Neighbor
{
    public class TileNeighborChecker : MonoBehaviour
    {
        public static void CheckNeighbors(Vector2 tilecoor)
        {
            var values = CornerDirections.GetValues(typeof(CornerDirections));

            //var s = TileNeighborSelector.SelectedNeighborTiles;

            foreach (CornerDirections item in values)
            {
                var s = new List<AbstractTile>();
                TileNeighborSelector.SelectNeighborsIntoList(tilecoor, item, s);
                //Debug.Log(GetColor(s[0]).ToString());
                if (GetColor(s[0]) == GetColor(s[1]) && GetColor(s[1]) == GetColor(s[2]) && GetColor(s[0]) == GetColor(s[2]) && s[0].Coordinates != s[1].Coordinates && s[1].Coordinates != s[2].Coordinates && s[0].Coordinates != s[2].Coordinates)
                {
                 Debug.Log($"BOOM: {s[0].Coordinates} - S1: {s[1].Coordinates} - S2: {s[2].Coordinates}");
                    Debug.Log(s.Count);

                    //Destroy(s[0].gameObject);
                    //Destroy(s[1].gameObject);
                    //Destroy(s[2].gameObject);

                    s[0].transform.localScale *= 0.5f;
                    s[1].transform.localScale *= 0.5f;
                    s[2].transform.localScale *= 0.5f;
                    TileSwapController._swapSequence.Kill();
                    TileSwapController.stopp = true;
                }
            }
        }

        private static Color GetColor(AbstractTile tile)
        {
            return tile.GetComponent<SpriteRenderer>().color;
        }
    }
}
