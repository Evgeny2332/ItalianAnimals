using _Project.Scripts.Core;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Timer
{
    public class LevelTimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;

        private ILevelTimer _timer;

        private void OnDestroy()
        {
            if (_timer != null)
                _timer.Ticked -= UpdateTime;
        }

        public void Initialize(ILevelTimer timer)
        {
            _timer = timer;
            _timer.Ticked += UpdateTime;

            UpdateTime(_timer.ElapsedSeconds);
        }

        private void UpdateTime(int totalSeconds) =>
            _timeText.text = TimeFormat.ToClock(totalSeconds);
    }
}
