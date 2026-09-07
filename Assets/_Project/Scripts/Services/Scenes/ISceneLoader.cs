using _Project.Scripts.Core;

namespace _Project.Scripts.Services.Scenes
{
    public interface ISceneLoader
    {
        void LoadMenu();
        void LoadLevel(TypeLevel typeLevel);
    }
}
