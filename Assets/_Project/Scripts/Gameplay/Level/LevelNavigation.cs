using _Project.Scripts.Services.Ads;
using _Project.Scripts.Services.Audio;
using _Project.Scripts.Services.Scenes;
using _Project.Scripts.Services.Session;

namespace _Project.Scripts.Gameplay.Level
{
    public sealed class LevelNavigation : ILevelNavigation
    {
        private readonly ILevelSelection _levelSelection;
        private readonly ISceneLoader _sceneLoader;
        private readonly IAudioService _audioService;
        private readonly IAdsService _adsService;

        public LevelNavigation(
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

        public void Restart()
        {
            _audioService.Play(SoundId.ButtonClick);
            _sceneLoader.LoadLevel(_levelSelection.TypeLevel);
        }

        public void ExitToMenu()
        {
            _audioService.Play(SoundId.ButtonClick);
            _adsService.ShowInterstitial(_sceneLoader.LoadMenu);
        }
    }
}
