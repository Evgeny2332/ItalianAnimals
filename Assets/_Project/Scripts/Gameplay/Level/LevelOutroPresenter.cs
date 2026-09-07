using System;
using _Project.Scripts.Services.Audio;

namespace _Project.Scripts.Gameplay.Level
{
    public sealed class LevelOutroPresenter : IDisposable
    {
        private readonly LevelFlow _levelFlow;
        private readonly ILevelCompleteView _completeView;
        private readonly IAudioService _audioService;
        private readonly ILevelNavigation _levelNavigation;

        public LevelOutroPresenter(
            LevelFlow levelFlow,
            ILevelCompleteView completeView,
            IAudioService audioService,
            ILevelNavigation levelNavigation)
        {
            _levelFlow = levelFlow;
            _completeView = completeView;
            _audioService = audioService;
            _levelNavigation = levelNavigation;

            _levelFlow.Completed += OnLevelCompleted;
            _completeView.RestartRequested += _levelNavigation.Restart;
            _completeView.MenuRequested += _levelNavigation.ExitToMenu;
        }

        public void Dispose()
        {
            _levelFlow.Completed -= OnLevelCompleted;
            _completeView.RestartRequested -= _levelNavigation.Restart;
            _completeView.MenuRequested -= _levelNavigation.ExitToMenu;
        }

        private void OnLevelCompleted(LevelResult result)
        {
            _audioService.Play(SoundId.LevelComplete);
            _completeView.Show(result);
        }
    }
}
