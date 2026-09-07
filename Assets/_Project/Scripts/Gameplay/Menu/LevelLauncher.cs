using _Project.Scripts.Core;
using _Project.Scripts.Services.Ads;
using _Project.Scripts.Services.Audio;
using _Project.Scripts.Services.Scenes;
using _Project.Scripts.Services.Session;

namespace _Project.Scripts.Gameplay.Menu
{
    public sealed class LevelLauncher : ILevelLauncher
    {
        private readonly ILevelSelection _levelSelection;
        private readonly ISceneLoader _sceneLoader;
        private readonly IAudioService _audioService;
        private readonly IAdsService _adsService;

        private bool _isLaunching;

        public LevelLauncher(
            ILevelSelection levelSelection,
            ISceneLoader sceneLoader,
            IAudioService audioService,
            IAdsService adsService)
        {
            _levelSelection = levelSelection;
            _sceneLoader = sceneLoader;
            _audioService = audioService;
            _adsService = adsService;
        }

        public void Launch(TypeLevel typeLevel, int levelId)
        {
            if (_isLaunching)
                return;

            _isLaunching = true;

            _audioService.Play(SoundId.ButtonClick);
            _levelSelection.Select(typeLevel, levelId);

            _adsService.ShowInterstitial(() => _sceneLoader.LoadLevel(typeLevel));
        }
    }
}
