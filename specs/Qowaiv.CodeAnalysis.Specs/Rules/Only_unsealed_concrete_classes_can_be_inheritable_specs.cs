namespace Rules.Only__unsealed_concrete_classes_can_be_inheritable_specs;

public class Verify
{
    [Test]
    public void Rule_rule_for_classes()
        => new SealClasses()
        .ForCS()
        .AddSource(@"Cases/OnlyUnsealedConcreteClassesCanBeInheritable.cs")
        .Verify();
}
