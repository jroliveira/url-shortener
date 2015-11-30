using System;

namespace UrlShortener.Infrastructure
{
    public static class Clock
    {
        public static Func<DateTime> Now = () => DateTime.Now;
        public static Func<DateTime> UtcNow = () => DateTime.UtcNow;

        public static void ResetClock()
        {
            Now = () => DateTime.Now;
            UtcNow = () => DateTime.UtcNow;
        }
    }
}
