using System;
using _Project.Scripts.Core;
using _Project.Scripts.Services.Progress;

namespace _Project.Scripts.Gameplay.Menu.LevelItem
{
    public sealed class LevelItemPresenter : IDisposable
    {
        private readonly LevelItemView _view;
        private readonly IProgressService _progressService;
        private readonly ILevelLauncher _levelLauncher;
        private readonly TypeLevel _typeLevel;
        private readonly int _levelId;

        public LevelItemPresenter(
            LevelItemView view,
            TypeLevel typeLevel,
            int levelId,
            IProgressService progressService,
            ILevelLauncher levelLauncher)
        {
            _view = view;
            _typeLevel = typeLevel;
            _levelId = levelId;
            _progressService = progressService;
            _levelLauncher = levelLauncher;

            _view.Clicked += OnClicked;
            _view.SetLevelNumber(_levelId);
        }

        public void Dispose() =>
            _view.Clicked -= OnClicked;

        public void Refresh()
        {
            _view.SetStars(_progressService.GetStars(_typeLevel, _levelId));
            _view.SetState(
                _progressService.IsUnlocked(_typeLevel, _levelId),
                _progressService.IsCompleted(_typeLevel, _levelId));
        }

        private void OnClicked()
        {
            if (!_progressService.IsUnlocked(_typeLevel, _levelId))
                return;

            _levelLauncher.Launch(_typeLevel, _levelId);
        }
    }
}
