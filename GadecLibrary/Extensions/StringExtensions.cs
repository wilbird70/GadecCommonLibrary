using System.Text.RegularExpressions;

namespace GadecLibrary.Extensions;
public static class StringExtensions
{

    /// <summary>
    /// Returns the string which lies between the two specified (and first to be found) strings.
    /// </summary>
    /// <param name="eString"></param>
    /// <param name="startsAfter">First string to be found.</param>
    /// <param name="endsBefore">Second string to be found.</param>
    /// <param name="noResultText">String to return if none of the strings is found.</param>
    /// <param name="includeSearchStrings">If true, the searchstrings will be included in the result.</param>
    /// <returns></returns>
    public static string InStrResult(this string eString, string startsAfter, string endsBefore = "", string noResultText = "", bool includeSearchStrings = false)
    {
        var start = InStr(eString, startsAfter);
        if (startsAfter == "")
            start = 1;
        if (start == 0)
            return noResultText;
        start += startsAfter.Length;
        switch (true)
        {
            case object _ when endsBefore == "":
                {
                    return eString.MidString(start);
                }

            case object _ when includeSearchStrings:
                {
                    var ends = InStr(start, eString, endsBefore);
                    if (ends == 0)
                        return noResultText;
                    return startsAfter + eString.MidString(start, ends - start) + endsBefore;
                }

            default:
                {
                    var ends = InStr(start, eString, endsBefore);
                    if (ends == 0)
                        return noResultText;
                    return eString.MidString(start, ends - start);
                }
        }
    }

    public static string AutoNumber(this string eString) => eString.AddNumber();

    public static string AddNumber(this string eString, int? add = null)
    {
        var parsed = eString.GetLastNumber();
        if (parsed.Number is not null)
        {
            var num = parsed.Number + (add is null ? 1 : add);
            var numLength = num.Value.ToString().Length;
            var length = numLength > parsed.Result.Length ? numLength : parsed.Result.Length;




            return parsed.Before + num.Value.ToString().PadLeft(length, '0') + parsed.After;
        }


        if (parsed.Number is null && add is null)
            return $"{parsed.Before}1{parsed.After}";
    }

    public static (int? Number, string Before, string Result, string After) GetLastNumber(this string eString)
    {
        MatchCollection matches = Regex.Matches(eString, @"\d+");
        if (matches.Count == 0)
            return (null, eString, string.Empty, string.Empty);

        Match lastMatch = matches[^1];
        int? number = int.Parse(lastMatch.Value);
        string before = eString[..lastMatch.Index];
        string result = lastMatch.Value;
        string after = eString[(lastMatch.Index + lastMatch.Length)..];
        return (number, before, result, after);
    }
}
