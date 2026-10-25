using System;
using Qowaiv;
using System.Collections.Generic;

public class SomeModel
{
    private readonly Qowaiv.Date Field;

    public Qowaiv.Date Property { get; set; }

    public Qowaiv.Date[] Array { get; set; }

    public List<Qowaiv.Date> List { get; set; }

    public Date? NullableProperty { get; set; }

    public Date SomeMethod() => default;

    public Qowaiv.Date? SomeNullableMethod() => default;

    public void Arguments(Qowaiv.Date argument) { }
}

public record SomeRecord(Qowaiv.Date Property);
