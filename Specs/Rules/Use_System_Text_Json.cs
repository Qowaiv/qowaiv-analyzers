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
}
