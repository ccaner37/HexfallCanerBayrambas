using Hexagon.Interfaces;
using Hexagon.Tile.Direction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using Hexagon.Tile.Neighbor;

namespace Hexagon.Tile
{
    public class HexagonTile : AbstractTile, IInteractable
    {
        public static Action<Vector2, Vector2, Vector2> OnTileInteract;
        public void ClickInteract(Vector2 inputPosition)
        {
            OnTileInteract(transform.position, inputPosition, Coordinates);
            StartCoroutine(domovetest());
        }

        private static IEnumerator domovetest()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.35f);
                for (int j = 0; j < TileNeighborSelector._selectedTiles.Count; j++)
                {
                    int nextNeig = j + 1;
                    if (nextNeig >= 3) nextNeig = 0;
                    var nextNeigTra = TileNeighborSelector._selectedTiles[nextNeig].transform;
                    TileNeighborSelector._selectedTiles[j].transform.DOMoveX(nextNeigTra.position.x, 0.25f).SetEase(Ease.OutQuad);
                    TileNeighborSelector._selectedTiles[j].transform.DOMoveY(nextNeigTra.position.y, 0.2f).SetEase(Ease.InQuad);
                }
            }
        }
    }
}
