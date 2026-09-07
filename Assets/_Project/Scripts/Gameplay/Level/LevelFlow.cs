using System;
using _Project.Scripts.Core;
using _Project.Scripts.Gameplay.Piece;
using _Project.Scripts.Gameplay.Timer;
using _Project.Scripts.Services.Progress;

namespace _Project.Scripts.Gameplay.Level
{
    public sealed class LevelFlow : IDisposable
    {
        private readonly PuzzleBoard _board;
        private readonly ILevelTimer _timer;
        private readonly IProgressService _progressService;
        private readonly LevelConfig _levelConfig;
        private readonly TypeLevel _typeLevel;
        private readonly int _levelId;

        public event Action<LevelResult> Completed;

        public LevelFlow(
            PuzzleBoard board,
            ILevelTimer timer,
            IProgressService progressService,
            LevelConfig levelConfig,
            TypeLevel typeLevel,
            int levelId)
        {
            _board = board;
            _timer = timer;
            _progressService = progressService;
            _levelConfig = levelConfig;
            _typeLevel = typeLevel;
            _levelId = levelId;
        }

        public void Start()
        {
            _board.Completed += OnBoardCompleted;
            _timer.Start();
        }

        public void Dispose() =>
            _board.Completed -= OnBoardCompleted;

        private void OnBoardCompleted()
        {
            _timer.Stop();

            int seconds = _timer.ElapsedSeconds;
            int stars = _levelConfig.GetStars(seconds);

            _progressService.RegisterResult(_typeLevel, _levelId, stars);

            Completed?.Invoke(new LevelResult(stars, seconds));
        }
    }
}
