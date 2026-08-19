using System;

public class DateTimeAsProvider
{
    public void Noncompliant()
    {
        var now = DateTime.Now; // Noncompliant {{Use a testable (date) time provider instead.}}
        //        ^^^^^^^^^^^^
        var utc = DateTime.UtcNow; // Noncompliant
        var today = DateTime.Today; // Noncompliant
        var offset_now = DateTimeOffset.Now; // Noncompliant
        var offset_utc = DateTimeOffset.UtcNow; // Noncompliant
    }

    public void CompliantAre()
    {
        var other = DateTime.DaysInMonth(2000, 2); // Compliant
        var noSystemDateTime = NotSystem.DateTime.Now; // Compliant
    }
}

namespace NotSystem
{
    public class DateTime
    {
        public static DateTime Now => new DateTime();
    }
}
