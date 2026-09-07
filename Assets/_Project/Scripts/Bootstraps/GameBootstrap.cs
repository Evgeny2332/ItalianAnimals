using _Project.Scripts.Core;
using _Project.Scripts.Gameplay.Level;
using _Project.Scripts.Gameplay.Piece;
using _Project.Scripts.Gameplay.Timer;
using _Project.Scripts.Services.Audio;
using _Project.Scripts.Services.Config;
using _Project.Scripts.Services.Progress;
using _Project.Scripts.Services.Session;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Scripts.Bootstraps
{
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Board")]
        [SerializeField] private PuzzlePieceView[] _pieceViews;
        [SerializeField] private Image _referenceIcon;

        [Header("HUD")]
        [SerializeField] private LevelTimerView _timerView;
        [SerializeField] private LevelCompleteView _completeView;
        [SerializeField] private Button _exitButton;

        [Header("Только для запуска сцены из редактора без меню")]
        [SerializeField] private TypeLevel _fallbackTypeLevel = TypeLevel.Easy;
        [SerializeField, Min(1)] private int _fallbackLevelId = 1;

        private PuzzleBoardFactory _puzzleBoardFactory;
        private IConfigProvider _configProvider;
        private IProgressService _progressService;
        private ILevelSelection _levelSelection;
        private IAudioService _audioService;
        private ILevelNavigation _levelNavigation;

        private PuzzleBoard _board;
        private LevelTimer _timer;
        private LevelFlow _levelFlow;
        private LevelOutroPresenter _outroPresenter;
        private ExitToMenuPresenter _exitPresenter;

        [Inject]
        private void Construct(
            PuzzleBoardFactory puzzleBoardFactory,
            IConfigProvider configProvider,
            IProgressService progressService,
            ILevelSelection levelSelection,
            IAudioService audioService,
            ILevelNavigation levelNavigation)
        {
            _puzzleBoardFactory = puzzleBoardFactory;
            _configProvider = configProvider;
            _progressService = progressService;
            _levelSelection = levelSelection;
            _audioService = audioService;
            _levelNavigation = levelNavigation;
        }

        private void Start() =>
            Initialize();

        private void OnDestroy()
        {
            _exitPresenter?.Dispose();
            _outroPresenter?.Dispose();
            _levelFlow?.Dispose();
            _timer?.Dispose();
            _board?.Dispose();
        }

        private void Initialize()
        {
            if (!_levelSelection.HasSelection)
                _levelSelection.Select(_fallbackTypeLevel, _fallbackLevelId);

            TypeLevel typeLevel = _levelSelection.TypeLevel;
            int levelId = _levelSelection.LevelId;

            LevelConfig levelConfig = _configProvider.GetLevel(typeLevel, levelId);

            if (levelConfig == null)
                return;

            _referenceIcon.sprite = levelConfig.ReferenceSprite;

            _board = _puzzleBoardFactory.Create(_pieceViews, levelConfig);

            _timer = new LevelTimer();
            _timerView.Initialize(_timer);

            _levelFlow = new LevelFlow(_board, _timer, _progressService, levelConfig, typeLevel, levelId);
            _outroPresenter = new LevelOutroPresenter(_levelFlow, _completeView, _audioService, _levelNavigation);
            _exitPresenter = new ExitToMenuPresenter(_exitButton, _levelNavigation);

            _levelFlow.Start();
        }
    }
}
