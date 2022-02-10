using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hexagon.Managers
{
    public class GameManager : MonoBehaviour
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

        private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
        private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

        public static void RestartScene()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _totalScore = 0;
            _movesCount = 0;
        }
    }
}
