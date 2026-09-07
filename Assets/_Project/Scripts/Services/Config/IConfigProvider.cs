using System.Collections.Generic;
using _Project.Scripts.Core;

namespace _Project.Scripts.Services.Config
{
    public interface IConfigProvider
    {
        IReadOnlyList<LevelConfig> GetLevels(TypeLevel typeLevel);
        LevelConfig GetLevel(TypeLevel typeLevel, int levelId);
        int GetLevelsCount(TypeLevel typeLevel);
        int GetMaxStars(TypeLevel typeLevel);
        int GetSceneBuildIndex(TypeLevel typeLevel);
    }
}
