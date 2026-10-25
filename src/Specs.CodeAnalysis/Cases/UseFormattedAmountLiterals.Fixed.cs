using Qowaiv.Financial;

class Literals
{
    void Unformatted()
    {
        _ = 1_200.Amount();
        _ = 100.00m.Amount();
        _ = 88.00m.Amount();
        _ = 23_000.00m.Amount();
    }

    void MissFormatted()
    {
        _ = 1_200m.Amount();
        _ = 12_223_100.42m.Amount();
    }

    void Negative()
    {
        _ = (-13.40m).Amount();
        _ = (-13.40m).Amount();
    }

    void Suffixes()
    {
        _ = 2.740m.Amount();
        _ = 2.74m.Amount();
        _ = 3.14m.Amount();
    }

    void AsArgs()
    {
        Arguments(3.10m.Amount());
        Arguments(42.Amount());
        Arguments(Amount.Zero, 3.14m.Amount());
    }

    void Arguments(params Amount[] amounts) { }

    void AsIndex()
    {
        _ = this[3.14m.Amount()];
        _ = this[42.Amount()];
    }

    int this[Amount index] => 42;
}
