using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor.Compilation;

namespace Hexagon.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static Action OnScoreChanged;

        public static GameManager Instance;

        private void Awake() => Instance = this;

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

        public void RestartScene()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
        }
    }
}
