using Hexagon.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hexagon.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private Text _scoreText, _movesCountText;

        private void OnEnable()
        {
            GameManager.OnScoreChanged += UpdateScoreText;
            GameManager.OnMovesCountChanged += UpdateMovesCountText;
        }

        private void OnDisable()
        {
            GameManager.OnScoreChanged -= UpdateScoreText;
            GameManager.OnMovesCountChanged -= UpdateMovesCountText;
        }

        public void UpdateScoreText()
        {
            _scoreText.text = $"Score: {GameManager.Instance.TotalScore}";
        }

        public void UpdateMovesCountText()
        {
            _movesCountText.text = GameManager.Instance.MovesCount.ToString();
        }

        public void RestartButton() => GameManager.Instance.RestartScene();
    }
}
