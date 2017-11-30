using SimpleLocalization.Localization.Abstractions;

namespace SimpleLocalization.Localization
{
    public static class Localizer
    {
        public static ILocalizer Current { get; private set; }
        private static object _lock = new object();

        public static void Set(ILocalizer localizer)
        {
            lock (_lock)
            {
                Current = localizer;
            }
        }
    }
}
