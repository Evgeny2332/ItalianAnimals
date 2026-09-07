using _Project.Scripts.Core;

namespace _Project.Scripts.Gameplay.Menu
{
    public interface ILevelLauncher
    {
        void Launch(TypeLevel typeLevel, int levelId);
    }
}
