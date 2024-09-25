namespace Rules.Apply_arithmetic_operations_on_non_nullables_only;

public class Verify
{
    [Test]
    public void Code()
         => new ApplyArithmeticOperationsOnNonNullablesOnly()
        .ForCS()
        .AddSource(@"Cases/ApplyArithmeticOperationsOnNonNullablesOnly.cs")
        .AddReference<Qowaiv.Financial.Amount>()
        .Verify();
}
