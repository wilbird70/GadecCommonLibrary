'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Singleton that holds the random object and patterns.
''' </summary>
Public Class Randomizer
    ''' <summary>
    ''' Needs one instance of the random object.
    ''' </summary>
    ''' <returns>The random</returns>
    Private ReadOnly Property Random As Random
    ''' <summary>
    ''' Patterns to choose from.
    ''' </summary>
    ''' <returns>The pattern dictionary.</returns>
    Private ReadOnly Property Patterns As Dictionary(Of String, String)

    ''' <summary>
    ''' Private constructor for the singleton.
    ''' </summary>
    Private Sub New()
        Random = New Random
        Patterns = New Dictionary(Of String, String) From {
            {"A", "ABCDEFGHIJKLMNOPQRSTUVWXYZ"},
            {"a", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"},
            {"X", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"},
            {"x", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"},
            {"#", "0123456789"},
            {"!", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%&)(]["},
            {"?", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&)(]["}
        }
    End Sub

    '///shared part of class\\\

    ''' <summary>
    ''' The singleton-Object.
    ''' </summary>
    Private Shared _randomizer As Randomizer = Nothing

    'shared functions

    ''' <summary>
    ''' Gets a random string of the specified length with uppercase characters and numbers only.
    ''' </summary>
    ''' <param name="length">The length of the resulting string.</param>
    ''' <returns>The random string.</returns>
    Public Shared Function GetString(length As Integer) As String
        If IsNothing(_randomizer) Then _randomizer = New Randomizer

        Dim stringBuilder = New Text.StringBuilder
        Dim patternString = _randomizer.Patterns("X")

        For i = 0 To length - 1
            stringBuilder.Append(patternString.Substring(_randomizer.Random.Next(0, patternString.Length - 1), 1))
        Next
        Return stringBuilder.ToString()
    End Function

    ''' <summary>
    ''' Gets a random string with the specified pattern.
    ''' <para>The following characters in the <paramref name="pattern"/> will replaced with random characters:</para>
    ''' <para>- A: with uppercase characters only;</para>
    ''' <para>- a: with upper- and lowercase characters;</para>
    ''' <para>- X: with uppercase characters and numbers;</para>
    ''' <para>- x: with upper- and lowercase characters and numbers;</para>
    ''' <para>- #: with numbers only;</para>
    ''' <para>- !: with uppercase characters, special characters and numbers;</para>
    ''' <para>- ?: with upper- and lowercase characters, special characters and numbers;</para>
    ''' <para>Other characters will remain untouched.</para>
    ''' </summary>
    ''' <param name="pattern">The pattern to use.</param>
    ''' <returns></returns>
    Public Shared Function GetString(pattern As String) As String
        If IsNothing(_randomizer) Then _randomizer = New Randomizer

        Dim stringBuilder = New Text.StringBuilder
        For Each character In pattern
            If Not _randomizer.Patterns.ContainsKey(character) Then stringBuilder.Append(character) : Continue For

            Dim patternString = _randomizer.Patterns(character)
            stringBuilder.Append(patternString.Substring(_randomizer.Random.Next(0, patternString.Length - 1), 1))
        Next
        Return stringBuilder.ToString()
    End Function

End Class
