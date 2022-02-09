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
            GameManager.OnScoreChanged += SetTex;
        }

        private void OnDisable()
        {
            GameManager.OnScoreChanged -= SetTex;
        }

        public void SetTex()
        {
            _scoreText.text = $"Score: {GameManager.Instance.TotalScore}";
        }
    }
}
