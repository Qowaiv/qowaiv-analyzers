namespace Qowaiv.CodeAnalysis.Shared;

public static class Amount
{
    [Pure]
    [SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of methods should not be too high", Justification = "This has to be applied on every amount litteral, so performance is more importent than complexity")]
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
        var suffix = token[^1];
        var dot = token.LastIndexOf('.');

        if (char.IsLetter(suffix))
        {
            if (suffix is 'd' or 'D' || TrimDecimalSuffix(token, suffix))
                suffix = default;

            length--;
        }
        else suffix = default;

        Decimals();
        Integers();

        if (suffix is not default(char))
            sb.Append(suffix);

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

    /// <summary>True if the decimal suffix does not effect the value of the number.</summary>
    [Pure]
    private static bool TrimDecimalSuffix(string token, char suffix)
        => suffix is 'm' or 'M'
        && SyntaxFactory.ParseToken(token).Value is decimal dec
        && SyntaxFactory.ParseToken(token[..^1]).Value is double dbl
        && dec == (decimal)dbl;
}
