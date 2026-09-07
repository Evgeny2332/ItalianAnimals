using System;
using _Project.Scripts.Core;
using _Project.Scripts.Gameplay.Menu.Selection;
using _Project.Scripts.Services.Config;
using _Project.Scripts.Services.Progress;
using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Scripts.Gameplay.Menu
{
    public class MainMenuPresenter : MonoBehaviour
    {
        [SerializeField] private DifficultyEntry[] _difficulties;

        private IWindowService _windowService;
        private IProgressService _progressService;
        private IConfigProvider _configProvider;

        [Inject]
        private void Construct(IWindowService windowService, IProgressService progressService, IConfigProvider configProvider)
        {
            _windowService = windowService;
            _progressService = progressService;
            _configProvider = configProvider;
        }

        private void Start()
        {
            foreach (var difficulty in _difficulties)
                difficulty.Bind(_windowService);

            _progressService.Changed += Refresh;
            Refresh();
        }

        private void OnDestroy()
        {
            if (_progressService != null)
                _progressService.Changed -= Refresh;

            foreach (var difficulty in _difficulties)
                difficulty.Unbind();
        }

        private void Refresh()
        {
            foreach (var difficulty in _difficulties)
            {
                difficulty.SetProgress(
                    _progressService.GetCompletedLevelsCount(difficulty.TypeLevel),
                    _configProvider.GetLevelsCount(difficulty.TypeLevel));
            }
        }

        [Serializable]
        private class DifficultyEntry
        {
            [SerializeField] private Button _openButton;
            [SerializeField] private LevelSelectionView _window;
            [SerializeField] private TextMeshProUGUI _progressText;

            private IWindowService _windowService;

            public TypeLevel TypeLevel => _window.TypeLevel;

            public void Bind(IWindowService windowService)
            {
                _windowService = windowService;
                _openButton.onClick.AddListener(Open);
            }

            public void Unbind() =>
                _openButton.onClick.RemoveListener(Open);

            public void SetProgress(int completed, int total) =>
                _progressText.text = $"{completed}/{total}";

            private void Open() =>
                _windowService.Open(_window);
        }
    }
}
