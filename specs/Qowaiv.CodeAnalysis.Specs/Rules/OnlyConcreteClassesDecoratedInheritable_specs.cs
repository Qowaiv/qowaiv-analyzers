namespace Rules.Only_concrete_classes_decorate_inheritable_specs;

public class Verify
{
    [Test]
    public void Rule_rule_for_classes()
        => new SealClasses()
        .ForCS()
        .AddSource(@"Cases/OnlyConcreteClassesDecorateInheritable.cs")
        .Verify();
}
