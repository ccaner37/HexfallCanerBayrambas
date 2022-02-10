using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Hexagon.Interfaces;
using Hexagon.Managers;

namespace Hexagon.Tile.Bomb
{
    public class BombTile : AbstractTile, IInteractable
    {
        private int _movesRemaining = 6;

        [SerializeField]
        private Text _movesCountText;

        private void OnEnable() => GameManager.OnMovesCountChanged += CheckMoves;

        private void OnDisable() => GameManager.OnMovesCountChanged -= CheckMoves;

        private void CheckMoves()
        {
            _movesRemaining--;
            _movesCountText.text = _movesRemaining.ToString();
            if(_movesRemaining <= 0)
            {
                ExplodeBomb();
            }
        }

        private void ExplodeBomb()
        {
            float duration = 2f;
            float strength = 1f;
            int vibrato = 10;
            float randomness = 90f;

            Vector2 scale = transform.localScale * 3f;
            Vector3 position = transform.position + -Vector3.forward;

            transform.position = position;

            transform.DOShakePosition(duration, strength, vibrato, randomness, false, false);
            transform.DOScale(scale, duration).OnComplete(() => 
            GameManager.RestartScene());
        }
    }
}
