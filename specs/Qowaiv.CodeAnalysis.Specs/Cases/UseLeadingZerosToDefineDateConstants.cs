using System;

class Noncompliant
{
    public static readonly DateOnly DateOnly = new(7, 06, 01); // Noncompliant {{Add leading zeros to year}}
    //                                             ^
    public static readonly Qowaiv.Date QowaivDate = new(2017, 6, 01); // Noncompliant {{Add leading zero to month}}
    //                                                        ^
    public static readonly DateTime DateTime = new(2017, 06, 01, 04, 02, 08, 3, DateTimeKind.Local); // Noncompliant {{Add leading zeros to millisecond}}
    //                                                                       ^
    public static readonly Qowaiv.LocalDateTime Qowaiv_LocalDateTime = new(2017, 06, 1, 04, 02, 08, 003); // Noncompliant

    public static readonly DateTimeOffset DateTimeOffset = new(2017, 6, 01, 04, 02, 08, 003, TimeSpan.FromHours(2)); // Noncompliant

    void Explicit()
    {
        var date = new DateOnly(2017, 6, 11); // Noncompliant
    }
}

class Compliant
{
    public static readonly DateOnly DateOnly = new(0007, 06, 01);
    public static readonly Qowaiv.Date QowaivDate = new(2017, 06, 01);
    public static readonly DateTime DateTime = new(2017, 06, 01, 04, 02, 08, 003, DateTimeKind.Local);
    public static readonly Qowaiv.LocalDateTime Qowaiv_LocalDateTime = new(2017, 06, 01, 04, 02, 08, 003);
    public static readonly DateTimeOffset DateTimeOffset = new(2017, 06, 01, 04, 02, 08, 003, TimeSpan.FromHours(2));

    public DateOnly Year(int year) => new(year, 2, 3); // Compliant {{Only constant dates are considered}}
}
