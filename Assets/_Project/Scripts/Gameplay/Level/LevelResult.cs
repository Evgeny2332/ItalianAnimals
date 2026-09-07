namespace _Project.Scripts.Gameplay.Level
{
    public readonly struct LevelResult
    {
        public readonly int Stars;
        public readonly int Seconds;

        public LevelResult(int stars, int seconds)
        {
            Stars = stars;
            Seconds = seconds;
        }
    }
}
