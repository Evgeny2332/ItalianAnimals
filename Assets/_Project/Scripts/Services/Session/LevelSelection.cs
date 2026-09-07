using _Project.Scripts.Core;

namespace _Project.Scripts.Services.Session
{
    public sealed class LevelSelection : ILevelSelection
    {
        public bool HasSelection { get; private set; }
        public TypeLevel TypeLevel { get; private set; }
        public int LevelId { get; private set; }

        public void Select(TypeLevel typeLevel, int levelId)
        {
            TypeLevel = typeLevel;
            LevelId = levelId;
            HasSelection = true;
        }
    }
}
