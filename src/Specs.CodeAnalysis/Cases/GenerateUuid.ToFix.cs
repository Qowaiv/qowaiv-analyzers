using Qowaiv;

class Model
{
    public void DefaultCtor()
    {
        Uuid uuid = new();
        Uuid half = new(
    }

    public Uuid EmptyString() => Uuid.Parse("");
    public Uuid MissingArgument() => Uuid.Parse();
    public Uuid OnlyOpening() => Uuid.Parse(;
}
