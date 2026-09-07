using _Project.Scripts.Bootstraps;
using _Project.Scripts.Gameplay.Level;
using _Project.Scripts.Gameplay.Piece;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Installers
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PuzzleBoardFactory>(Lifetime.Singleton);
            builder.Register<LevelNavigation>(Lifetime.Singleton).As<ILevelNavigation>();

            builder.RegisterComponentInHierarchy<GameBootstrap>();
        }
    }
}
