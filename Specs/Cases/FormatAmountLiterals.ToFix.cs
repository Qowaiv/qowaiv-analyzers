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
        _ = 12_00.Amount();
        _ = 122_23_100.42.Amount();
    }

    void Negative()
    {
        _ = (-2313.4).Amount();
        _ = (Amount)(-13.4);
    }
}

