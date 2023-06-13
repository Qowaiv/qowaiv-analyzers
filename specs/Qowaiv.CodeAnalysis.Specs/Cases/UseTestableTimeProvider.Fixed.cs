using System;

public class DateTimeAsProvider
{
    public void Issues()
    {
        var now = Clock.Now();
        var utc = Clock.UtcNow();
        var today = Clock.Today();
        var offset_now = Clock.NowWithOffset();
        var offset_utc = Clock.NowWithOffset(TimeZoneInfo.Utc);
    }
}
