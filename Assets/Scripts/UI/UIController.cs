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
        private Text _scoreText;

        private void OnEnable()
        {
            GameManager.OnScoreChanged += UpdateScoreText;
        }

        private void OnDisable()
        {
            GameManager.OnScoreChanged -= UpdateScoreText;
        }

        public void UpdateScoreText()
        {
            _scoreText.text = $"Score: {GameManager.Instance.TotalScore}";
        }

        public void RestartButton() => GameManager.Instance.RestartScene();
    }
}
