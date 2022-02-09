using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
