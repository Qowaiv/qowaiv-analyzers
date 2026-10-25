using Qowaiv.Financial;

class Literals
{
    void Unformatted()
    {
        _ = 1200.Amount();
        _ = 100.0.Amount();
        _ = 88.0m.Amount();
        _ = (Amount)23_000.00;
    }

    void MissFormatted()
    {
        _ = 12_00m.Amount();
        _ = 122_23_100.42m.Amount();
    }

    void Negative()
    {
        _ = (-13.4m).Amount();
        _ = (Amount)(-13.4m);
    }

    void Suffixes()
    {
        _ = 2.740.Amount();
        _ = 2.74d.Amount();
        _ = 3.14D.Amount();
    }

    void AsArgs()
    {
        Arguments(3.1.Amount());
        Arguments((Amount)42);
        Arguments(Amount.Zero, 3.14.Amount());
    }

    void Arguments(params Amount[] amounts) { }

    void AsIndex()
    {
        _ = this[3.14.Amount()];
        _ = this[(Amount)42];
    }

    int this[Amount index] => 42;
}
