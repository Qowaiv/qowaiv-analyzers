namespace Rules.Only_unsealed_concrete_classes_can_be_inheritable;

public class Verify
{
    [Test]
    public void Rule_for_classes()
        => new SealClasses()
        .ForCS()
        .AddSource(@"Cases/OnlyUnsealedConcreteClassesCanBeInheritable.cs")
        .Verify();
}
