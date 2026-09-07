using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Core
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "PuzzleGame/Level Config", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        private const int MinStars = 1;

        [Header("Puzzle sprites")]
        [SerializeField] private Sprite _referenceSprite;
        [SerializeField] private Sprite[] _pieceSprites;

        [Header("Пороги на звёзды (уложился в MaxSeconds — получил CountStars)")]
        [FormerlySerializedAs("_results")]
        [SerializeField] private StarThreshold[] _thresholds;

        public Sprite ReferenceSprite => _referenceSprite;
        public IReadOnlyList<Sprite> PieceSprites => _pieceSprites;

        public int MaxStars
        {
            get
            {
                int max = MinStars;

                foreach (var threshold in _thresholds)
                    max = Mathf.Max(max, threshold.CountStars);

                return max;
            }
        }

        public int GetStars(int seconds)
        {
            int stars = MinStars;

            foreach (var threshold in _thresholds)
            {
                if (seconds <= threshold.MaxSeconds)
                    stars = Mathf.Max(stars, threshold.CountStars);
            }

            return stars;
        }

        [Serializable]
        private class StarThreshold
        {
            [SerializeField, Min(1)] private int _countStars = 1;
            [SerializeField, Min(1)] private int _maxSeconds = 60;

            public int CountStars => _countStars;
            public int MaxSeconds => _maxSeconds;
        }
    }
}
