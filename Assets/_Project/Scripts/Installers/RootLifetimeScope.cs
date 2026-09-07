using _Project.Scripts.Core;
using _Project.Scripts.Gameplay.Piece;
using _Project.Scripts.Services.Ads;
using _Project.Scripts.Services.Audio;
using _Project.Scripts.Services.Config;
using _Project.Scripts.Services.Progress;
using _Project.Scripts.Services.Save;
using _Project.Scripts.Services.Scenes;
using _Project.Scripts.Services.Session;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Installers
{
    public class RootLifetimeScope : LifetimeScope
    {
        [Header("Content")]
        [SerializeField] private LevelCatalog _levelCatalog;
        [SerializeField] private PieceParameters _pieceParameters;

        [Header("Audio")]
        [SerializeField] private AudioLibrary _audioLibrary;
        [SerializeField] private AudioSource _audioSource;

        [Header("Scenes")]
        [SerializeField, Min(0)] private int _menuSceneBuildIndex;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_levelCatalog);
            builder.RegisterInstance(_pieceParameters);
            builder.RegisterInstance(_audioLibrary);
            builder.RegisterInstance(_audioSource);

            builder.Register<CatalogConfigProvider>(Lifetime.Singleton).As<IConfigProvider>();
            builder.Register<ISaveLoadService>(_ => new JsonSaveLoadService(Application.persistentDataPath), Lifetime.Singleton);
            builder.Register<ProgressService>(Lifetime.Singleton).As<IProgressService>();
            builder.Register<LevelSelection>(Lifetime.Singleton).As<ILevelSelection>();
            builder.Register<AudioService>(Lifetime.Singleton).As<IAudioService>();
            builder.Register<StubAdsService>(Lifetime.Singleton).As<IAdsService>();
            builder.Register<ISceneLoader>(
                resolver => new SceneLoader(_menuSceneBuildIndex, resolver.Resolve<IConfigProvider>()),
                Lifetime.Singleton);
        }
    }
}
