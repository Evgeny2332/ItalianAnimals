using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Menu.LevelItem;
using _Project.Scripts.Services.Config;
using _Project.Scripts.Services.Progress;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Menu.Selection
{
    public sealed class LevelSelectionPresenter : IDisposable
    {
        private readonly LevelSelectionView _view;
        private readonly IProgressService _progressService;
        private readonly IConfigProvider _configProvider;
        private readonly List<LevelItemPresenter> _itemPresenters = new();

        public LevelSelectionPresenter(
            LevelSelectionView view,
            IProgressService progressService,
            IConfigProvider configProvider,
            ILevelLauncher levelLauncher)
        {
            _view = view;
            _progressService = progressService;
            _configProvider = configProvider;

            CreateItemPresenters(levelLauncher);

            _progressService.Changed += Refresh;
            Refresh();
        }

        public void Dispose()
        {
            _progressService.Changed -= Refresh;

            foreach (var presenter in _itemPresenters)
                presenter.Dispose();
        }

        private void CreateItemPresenters(ILevelLauncher levelLauncher)
        {
            var items = _view.LevelItems;
            int levelsCount = _configProvider.GetLevelsCount(_view.TypeLevel);

            if (items.Count != levelsCount)
                Debug.LogWarning($"[{_view.TypeLevel}] кнопок уровней на сцене {items.Count}, а конфигов в каталоге {levelsCount}.");

            int count = Mathf.Min(items.Count, levelsCount);

            for (int i = 0; i < count; i++)
                _itemPresenters.Add(new LevelItemPresenter(items[i], _view.TypeLevel, i + 1, _progressService, levelLauncher));
        }

        private void Refresh()
        {
            foreach (var presenter in _itemPresenters)
                presenter.Refresh();

            _view.SetStarsCounter(
                _progressService.GetCollectedStars(_view.TypeLevel),
                _configProvider.GetMaxStars(_view.TypeLevel));
        }
    }
}
