using System;
using _Project.Scripts.Core;
using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Level
{
    public class LevelCompleteView : Window, ILevelCompleteView
    {
        [Header("Результат")]
        [SerializeField] private StarsView _stars;
        [SerializeField] private TextMeshProUGUI _timeText;

        [Header("Кнопки")]
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        [Header("Эффекты")]
        [SerializeField] private GameObject _confetti;

        public event Action RestartRequested;
        public event Action MenuRequested;

        protected override void Awake()
        {
            base.Awake();

            _restartButton.onClick.AddListener(OnRestartClicked);
            _menuButton.onClick.AddListener(OnMenuClicked);

            if (_confetti != null)
                _confetti.SetActive(false);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _restartButton.onClick.RemoveListener(OnRestartClicked);
            _menuButton.onClick.RemoveListener(OnMenuClicked);
        }

        public void Show(LevelResult result)
        {
            if (_stars != null)
                _stars.SetCount(result.Stars);

            if (_timeText != null)
                _timeText.text = TimeFormat.ToClock(result.Seconds);

            if (_confetti != null)
                _confetti.SetActive(true);

            base.Show();
        }

        private void OnRestartClicked() =>
            RestartRequested?.Invoke();

        private void OnMenuClicked() =>
            MenuRequested?.Invoke();
    }
}
