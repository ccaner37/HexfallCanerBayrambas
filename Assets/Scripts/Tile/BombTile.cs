using DG.Tweening;
using Hexagon.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hexagon.Tile.Bomb
{
    public class BombTile : AbstractTile
    {
        private int _movesRemaining = 6;

        [SerializeField]
        private Text _movesCountText;

        private void OnEnable()
        {
            GameManager.OnMovesCountChanged += CheckMoves;
        }

        private void OnDisable()
        {
            GameManager.OnMovesCountChanged -= CheckMoves;
        }

        private void CheckMoves()
        {
            _movesRemaining--;
            _movesCountText.text = _movesRemaining.ToString();
            if(_movesRemaining <= 0)
            {
                transform.DOShakePosition(2f, 1, 10, 90);
                transform.DOScale(transform.localScale * 5, 2f).OnComplete(() => GameManager.Instance.RestartScene());
            }
        }
    }
}
