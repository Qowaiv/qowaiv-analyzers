using System;
using Qowaiv;
using System.Collections.Generic;

public class SomeModel
{
    private readonly DateOnly Field;

    public DateOnly Property { get; set; }

    public DateOnly[] Array { get; set; }

    public List<DateOnly> List { get; set; }

    public DateOnly? NullableProperty { get; set; }

    public DateOnly SomeMethod() => default;

    public DateOnly? SomeNullableMethod() => default;

    public void Arguments(DateOnly argument) { }
}

public record SomeRecord(DateOnly Property);
