namespace Qowaiv.CodeAnalysis.Shared;

public static class Amount
{
    [Pure]
    public static string Format(string token)
    {
        if ("eEbBxX".Any(token.Contains))
        {
            var parsed = SyntaxFactory.ParseToken(token);
            return parsed.Value is string
                ? token
                : Format(parsed.ValueText);
        }

        var sb = new StringBuilder(token.Length * 2);
        var length = token.Length;
        var prefix = token[^1];
        var dot = token.LastIndexOf('.');

        if (char.IsLetter(prefix))
            length--;
        else prefix = default;

        Decimals();
        Integers();

        if (prefix is not default(char))
            sb.Append(prefix);

        return sb.ToString();

        void Decimals()
        {
            if (dot is -1) return;

            sb.Append('.');

            var pos = dot + 1;

            while (pos < length)
            {
                var ch = token[pos++];

                if (char.IsDigit(ch))
                    sb.Append(ch);

            }
            // Add trailing zero.
            while (sb.Length < 3)
                sb.Append('0');
        }

        void Integers()
        {
            var pos = dot is -1 ? length : dot;
            var len = 0;

            while (pos-- > 0)
            {
                var ch = token[pos];

                if (char.IsDigit(ch))
                {
                    // we have a group of 3 and and a next digit, so add a seperator.
                    if (len is 3)
                    {
                        sb.Insert(0, '_');
                        len = 0;
                    }
                    sb.Insert(0, ch);
                    len++;
                }
            }

            // Add leading zero for decimals only.
            if (sb[0] is '.')
                sb.Insert(0, '0');
        }
    }

    [Pure]
    public static bool IsFormatted(string token)
        => token == Format(token);
}
