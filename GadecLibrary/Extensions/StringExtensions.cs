using System.Text.RegularExpressions;

namespace GadecLibrary.Extensions;
public static class StringExtensions
{
    public static string InStrResult(this string eString, string startsAfter, string endsBefore = "", string noResultString = "", bool includeSearchStrings = false)
    {
        var startsAt = eString.IndexOf(startsAfter);

        if (startsAt == -1)
            return noResultString;

        if (startsAfter.Length == 0)
            startsAt = 0;

        startsAt += startsAfter.Length;

        if (endsBefore.Length == 0)
            return includeSearchStrings ? $"{startsAfter}{eString[startsAt..]}" : eString[startsAt..];

        var endsAt = eString.IndexOf(endsBefore, startsAt);

        if (endsAt == -1)
            return noResultString;

        var result = eString[startsAt..endsAt];

        return includeSearchStrings ? $"{startsAfter}{result}{endsBefore}" : result;
    }

    public static string AutoNumber(this string eString) => eString.AddNumber();

    public static string AddNumber(this string eString, int? add = null)
    {
        var parsed = eString.GetLastNumber();
        if (parsed.Number is not null)
        {
            var numberResult = parsed.Number + (add ?? 1);
            var numberString = Math.Abs(numberResult.Value).ToString();
            var numberLength = numberString.Length;
            var length = numberLength > parsed.Match.Length ? numberLength : parsed.Match.Length;
            var negativeSign = numberResult < 0 ? "-" : string.Empty;
            var result = numberString.PadLeft(length - negativeSign.Length, '0');
            return $"{parsed.Before}{negativeSign}{result}{parsed.After}";
        }

        if (parsed.Number is null && add is null)
            return $"{parsed.Before}1{parsed.After}";

        return eString;
    }

    private static (int? Number, string Before, string Match, string After) GetLastNumber(this string eString)
    {
        MatchCollection matches = Regex.Matches(eString, @"\d+");
        if (matches.Count == 0)
            return (null, eString, string.Empty, string.Empty);

        Match lastMatch = matches[^1];
        int? number = int.Parse(lastMatch.Value);
        string before = eString[..lastMatch.Index];
        string match = lastMatch.Value;
        string after = eString[(lastMatch.Index + lastMatch.Length)..];
        return (number, before, match, after);
    }
}
