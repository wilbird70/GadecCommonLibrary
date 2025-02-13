using GadecLibrary.Extensions;

namespace GadecLibraryTests;
internal class ExceptionExtensionsTests
{
    [TestCase("DezeTekstWordtNuBekeken", "Tekst", "Nu", "Error", false, "Wordt")]
    [TestCase("DezeTekstWordtNuBekeken", "Tekst", "Nu", "Error", true, "TekstWordtNu")]
    [TestCase("DezeTekstWordtNuBekeken", "Tekst", "", "Error", false, "WordtNuBekeken")]
    [TestCase("DezeTekstWordtNuBekeken", "Tekst", "", "Error", true, "TekstWordtNuBekeken")]
    [TestCase("DezeTekstWordtNuBekeken", "Niet", "Nu", "Error", false, "Error")]
    [TestCase("DezeTekstWordtNuBekeken", "Tekst", "Niet", "Error", true, "Error")]
    public void Test_InStrResult(string inputString, string startsAfter, string endsBefore, string noResultString, bool includeSearchStrings, string expectedResult)
    {
        var result = inputString.InStrResult(startsAfter, endsBefore, noResultString, includeSearchStrings);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase("ABC", "ABC1")]
    [TestCase("A0", "A1")]
    [TestCase("A9", "A10")]
    [TestCase("A001", "A002")]
    [TestCase("A000345", "A000346")]
    public void Test_AutoNumber(string inputString, string expectedResult)
    {
        var result = inputString.AutoNumber();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [TestCase("ABC", null, "ABC1")]
    [TestCase("A0", null, "A1")]
    [TestCase("A009", 5, "A014")]
    [TestCase("A009", -5, "A004")]
    [TestCase("A004", -5, "A-01")]
    [TestCase("A000345", 8, "A000353")]
    public void Test_AddNumber(string inputString, int? add, string expectedResult)
    {
        var result = inputString.AddNumber(add);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
