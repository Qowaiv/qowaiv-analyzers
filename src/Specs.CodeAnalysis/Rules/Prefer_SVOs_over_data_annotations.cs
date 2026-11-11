namespace Rules.Prefer_SVOs_over_data_annotations;

public class Verify
{
    [Test]
    public void Rule() => new PreventPrimitiveObssession()
        .ForCS()
        .AddSource(@"Cases/PreferSVOsOverDataAnnotations.cs")
        .AddReference<System.ComponentModel.DataAnnotations.DataType>()
        .AddReference<Qowaiv.EmailAddress>()
        .Verify();
}
