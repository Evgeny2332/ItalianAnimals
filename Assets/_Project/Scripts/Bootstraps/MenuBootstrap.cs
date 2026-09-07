using System.Collections.Generic;
using _Project.Scripts.Gameplay.Menu;
using _Project.Scripts.Gameplay.Menu.Selection;
using _Project.Scripts.Services.Config;
using _Project.Scripts.Services.Progress;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Bootstraps
{
    public class MenuBootstrap : MonoBehaviour
    {
        [SerializeField] private LevelSelectionView[] _levelSelectionViews;

        private readonly List<LevelSelectionPresenter> _presenters = new();

        private IProgressService _progressService;
        private IConfigProvider _configProvider;
        private ILevelLauncher _levelLauncher;

        [Inject]
        private void Construct(IProgressService progressService, IConfigProvider configProvider, ILevelLauncher levelLauncher)
        {
            _progressService = progressService;
            _configProvider = configProvider;
            _levelLauncher = levelLauncher;
        }

        private void Start()
        {
            foreach (var view in _levelSelectionViews)
                _presenters.Add(new LevelSelectionPresenter(view, _progressService, _configProvider, _levelLauncher));
        }

        private void OnDestroy()
        {
            foreach (var presenter in _presenters)
                presenter.Dispose();

            _presenters.Clear();
        }
    }
}
