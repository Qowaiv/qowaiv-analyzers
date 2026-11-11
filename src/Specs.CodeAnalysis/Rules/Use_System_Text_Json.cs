namespace Rules.Use_System_Text_Json;

public class Verify
{
    [Test]
    public void Rule() => new UseSystemTextJson()
        .ForCS()
        .AddSource(@"Cases/UseSystemTextJson.cs")
        .AddReference<Newtonsoft.Json.JsonException>()
        .AddReference<System.Text.Json.JsonException>()
        .Verify();

    [Test]
    public void Rule_without_usings() => new UseSystemTextJson()
        .ForCS()
        .AddSource(@"Cases/UseSystemTextJson.Gate.cs")
        .AddReference<Newtonsoft.Json.JsonException>()
        .AddReference<System.Text.Json.JsonException>()
        .Verify();

    [Test]
    public void Rule_with_global_usings() => new UseSystemTextJson()
        .ForCS()
        .AddSources(@"Cases/UseSystemTextJson.GlobalUsings.Gate.cs", @"Cases/UseSystemTextJson.GlobalUsings.cs")
        .AddReference<Newtonsoft.Json.JsonException>()
        .AddReference<System.Text.Json.JsonException>()
        .Verify();
}
