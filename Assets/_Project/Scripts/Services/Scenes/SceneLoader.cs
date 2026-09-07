using _Project.Scripts.Core;
using _Project.Scripts.Services.Config;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Services.Scenes
{
    public sealed class SceneLoader : ISceneLoader
    {
        private readonly int _menuSceneBuildIndex;
        private readonly IConfigProvider _configProvider;

        public SceneLoader(int menuSceneBuildIndex, IConfigProvider configProvider)
        {
            _menuSceneBuildIndex = menuSceneBuildIndex;
            _configProvider = configProvider;
        }

        public void LoadMenu() =>
            SceneManager.LoadScene(_menuSceneBuildIndex);

        public void LoadLevel(TypeLevel typeLevel) =>
            SceneManager.LoadScene(_configProvider.GetSceneBuildIndex(typeLevel));
    }
}
