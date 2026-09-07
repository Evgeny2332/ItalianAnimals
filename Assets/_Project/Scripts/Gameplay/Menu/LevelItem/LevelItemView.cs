using System;
using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Menu.LevelItem
{
    public class LevelItemView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private StarsView _stars;

        [Header("Фон по состоянию уровня")]
        [SerializeField] private Sprite _lockedBackground;
        [SerializeField] private Sprite _openedBackground;
        [SerializeField] private Sprite _completedBackground;

        public event Action Clicked;

        private void Awake() =>
            _button.onClick.AddListener(OnButtonClicked);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnButtonClicked);

        public void SetLevelNumber(int levelId) =>
            _levelText.text = levelId.ToString();

        public void SetStars(int countStars) =>
            _stars.SetCount(countStars);

        public void SetState(bool isUnlocked, bool isCompleted)
        {
            _button.interactable = isUnlocked;
            _background.sprite = isCompleted ? _completedBackground
                : isUnlocked ? _openedBackground
                : _lockedBackground;
        }

        private void OnButtonClicked() =>
            Clicked?.Invoke();
    }
}
