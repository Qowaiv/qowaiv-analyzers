﻿using System;

public class DateTimeAsProvider
{
    public void Noncompliant()
    {
        var now = DateTime.Now;
        var utc = DateTime.UtcNow;
        var today = DateTime.Today;
        var offset_now = DateTimeOffset.Now;
        var offset_utc = DateTimeOffset.UtcNow;
    }
}
