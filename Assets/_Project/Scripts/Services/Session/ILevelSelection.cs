using _Project.Scripts.Core;

namespace _Project.Scripts.Services.Session
{
    public interface ILevelSelection
    {
        bool HasSelection { get; }
        TypeLevel TypeLevel { get; }
        int LevelId { get; }

        void Select(TypeLevel typeLevel, int levelId);
    }
}
