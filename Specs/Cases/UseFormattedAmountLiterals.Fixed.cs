using Qowaiv.Financial;

class Literals
{
    void Unformatted()
    {
        _ = 1_200.Amount();
        _ = 100.00.Amount();
        _ = 88.00m.Amount();
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
}
