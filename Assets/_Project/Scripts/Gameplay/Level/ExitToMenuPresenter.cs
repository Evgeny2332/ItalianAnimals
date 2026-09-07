using System;
using UnityEngine.UI;

namespace _Project.Scripts.Gameplay.Level
{
    public sealed class ExitToMenuPresenter : IDisposable
    {
        private readonly Button _exitButton;
        private readonly ILevelNavigation _levelNavigation;

        public ExitToMenuPresenter(Button exitButton, ILevelNavigation levelNavigation)
        {
            _exitButton = exitButton;
            _levelNavigation = levelNavigation;

            _exitButton.onClick.AddListener(OnExitClicked);
        }

        public void Dispose() =>
            _exitButton.onClick.RemoveListener(OnExitClicked);

        private void OnExitClicked() =>
            _levelNavigation.ExitToMenu();
    }
}
