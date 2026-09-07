using System.Collections.Generic;
using _Project.Scripts.Core;
using _Project.Scripts.Gameplay.Menu.LevelItem;
using _Project.Scripts.UI;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Menu.Selection
{
    public class LevelSelectionView : Window
    {
        [Header("Levels")]
        [SerializeField] private TypeLevel _typeLevel;
        [SerializeField] private LevelItemView[] _levelItems;
        [SerializeField] private TextMeshProUGUI _starsCounterText;

        public TypeLevel TypeLevel => _typeLevel;
        public IReadOnlyList<LevelItemView> LevelItems => _levelItems;

        public void SetStarsCounter(int collected, int max)
        {
            if (_starsCounterText != null)
                _starsCounterText.text = $"{collected}/{max}";
        }
    }
}
