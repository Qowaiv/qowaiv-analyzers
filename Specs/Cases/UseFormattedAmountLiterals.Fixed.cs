using Qowaiv.Financial;

class Literals
{
    void Unformatted()
    {
        _ = 1_200.Amount();
        _ = 100.00.Amount();
        _ = 88.00.Amount();
        _ = 23_000.00.Amount();
    }

    void MissFormatted()
    {
        _ = 1_200.Amount();
        _ = 12_223_100.42.Amount();
    }

    void Negative()
    {
        _ = (-2_313.40).Amount();
        _ = (-13.40).Amount();
    }

    void AsArgs()
    {
        Arguments(3.10.Amount());
        Arguments(42.Amount());
        Arguments(Amount.Zero, 3.14.Amount());
    }

    void Arguments(params Amount[] amounts) { }

    void AsIndex()
    {
        _ = this[3.14.Amount()];
        _ = this[42.Amount()];
    }

    int this[Amount index] => 42;
}
