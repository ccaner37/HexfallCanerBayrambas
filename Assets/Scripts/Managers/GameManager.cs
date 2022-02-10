using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Hexagon.Managers
{
    public class GameManager
    {
        public static Action OnScoreChanged;
        public static Action OnMovesCountChanged;

        private static int _totalScore;
        public static int TotalScore
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

        private static int _movesCount;
        public static int MovesCount
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

        public static void RestartScene()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }
}
