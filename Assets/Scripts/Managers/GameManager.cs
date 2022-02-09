using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.Compilation;

namespace Hexagon.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static Action OnScoreChanged;
        public static Action OnMovesCountChanged;

        public static GameManager Instance;

        private int _totalScore;
        public int TotalScore
        {
            get
            {
                return _totalScore;
            }
            set
            {
                _totalScore = value;
                OnScoreChanged();
            }
        }

        private int _movesCount;
        public int MovesCount
        {
            get
            {
                return _movesCount;
            }
            set
            {
                _movesCount = value;
                OnMovesCountChanged();
            }
        }

        private void Awake() => Instance = this;

        public void RestartScene()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }
}
