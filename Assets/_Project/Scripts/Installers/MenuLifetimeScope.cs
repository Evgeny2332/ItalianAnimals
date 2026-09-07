using _Project.Scripts.Bootstraps;
using _Project.Scripts.Gameplay.Menu;
using _Project.Scripts.UI;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Installers
{
    public class MenuLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<WindowService>(Lifetime.Singleton).As<IWindowService>();
            builder.Register<LevelLauncher>(Lifetime.Singleton).As<ILevelLauncher>();

            builder.RegisterComponentInHierarchy<MenuBootstrap>();
            builder.RegisterComponentInHierarchy<MainMenuPresenter>();
        }
    }
}
