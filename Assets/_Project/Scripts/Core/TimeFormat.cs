namespace _Project.Scripts.Core
{
    public static class TimeFormat
    {
        private const int SecondsInMinute = 60;

        public static string ToClock(int totalSeconds)
        {
            int minutes = totalSeconds / SecondsInMinute;
            int seconds = totalSeconds % SecondsInMinute;

            return $"{minutes}:{seconds:00}";
        }
    }
}
