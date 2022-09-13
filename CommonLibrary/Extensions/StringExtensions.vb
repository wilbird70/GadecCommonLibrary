'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module StringExtensions

    ''' <summary>
    ''' Returns the string which lies between the two specified (and first to be found) strings.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="startsAfter">First string to be found.</param>
    ''' <param name="endsBefore">Second string to be found.</param>
    ''' <param name="noResultText">String to return if none of the strings is found.</param>
    ''' <param name="includeSearchStrings">If true, the searchstrings will be included in the result.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function InStrResult(ByVal eString As String, startsAfter As String, Optional endsBefore As String = "",
                         Optional noResultText As String = "", Optional includeSearchStrings As Boolean = False) As String
        Dim start = InStr(eString, startsAfter)
        If startsAfter = "" Then start = 1
        If start = 0 Then Return noResultText
        start += startsAfter.Length
        Select Case True
            Case endsBefore = "" : Return eString.MidString(start)
            Case includeSearchStrings
                Dim ends = InStr(start, eString, endsBefore)
                If ends = 0 Then Return noResultText
                Return startsAfter & eString.MidString(start, ends - start) & endsBefore
            Case Else
                Dim ends = InStr(start, eString, endsBefore)
                If ends = 0 Then Return noResultText
                Return eString.MidString(start, ends - start)
        End Select
    End Function

    ''' <summary>
    ''' Returns the string which lies between the two specified (and from the right first to be found) strings.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="startsAfter">First string to be found.</param>
    ''' <param name="endsBefore">Second string to be found.</param>
    ''' <param name="noResultText"></param>
    ''' <param name="includeSearchStrings"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function InStrRevResult(ByVal eString As String, startsAfter As String, Optional endsBefore As String = "",
                            Optional noResultText As String = "", Optional includeSearchStrings As Boolean = False) As String
        Dim start = InStrRev(eString, startsAfter)
        If startsAfter = "" Then start = 1
        If start = 0 Then Return noResultText
        start += startsAfter.Length
        Select Case True
            Case endsBefore = "" : Return eString.MidString(start)
            Case includeSearchStrings
                Dim ends = InStr(start, eString, endsBefore)
                If ends = 0 Then Return noResultText
                Return startsAfter & eString.MidString(start, ends - start) & endsBefore
            Case Else
                Dim ends = InStr(start, eString, endsBefore)
                If ends = 0 Then Return noResultText
                Return eString.MidString(start, ends - start)
        End Select
    End Function

    ''' <summary>
    ''' Splits the string into a stringarray with the specified delimiter.
    ''' <para>When no dlimiter is specified the semicolon is used.</para>
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="delimiter">Delimiter to use.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function Cut(ByVal eString As String, Optional delimiter As String = ";") As String()
        Return Split(eString, delimiter)
    End Function

    ''' <summary>
    ''' Returns the string at the indexnumber from the stringarray.
    ''' </summary>
    ''' <param name="eStrings"></param>
    ''' <param name="index">Indexnumber.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function Item(ByVal eStrings As String(), index As Integer) As String
        Return eStrings(index)
    End Function

    ''' <summary>
    ''' Replaces all keystrings with the corresponding valuesstrings.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="dictionary">The KeyValuePairs to use.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function ReplaceMultiple(ByVal eString As String, dictionary As Dictionary(Of String, String)) As String
        Dim output = eString
        dictionary.ToList.ForEach(Sub(pair) output = output.Replace(pair.Key, pair.Value))
        Return output
    End Function

    ''' <summary>
    ''' Adds 1 to the rightmost number found in the string.
    ''' If the string has no number, it appends the string with an '1'.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function AutoNumber(ByVal eString As String) As String
        Return AddNumber(eString, 0)
    End Function

    ''' <summary>
    ''' Adds a number to the rightmost number found in the string.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="add">Number to add.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function AddNumber(ByVal eString As String, add As Integer) As String
        Dim patternResult = eString.FindResultAsPatternRev("0123456789")
        Select Case True
            Case Not patternResult.Result = ""
                Dim length = patternResult.Result.Length
                Dim num As Integer
                Select Case add = 0
                    Case True : num = patternResult.Result.ToInteger + 1
                    Case Else : num = patternResult.Result.ToInteger + add
                End Select
                If num.ToString.Length > length Then length = num.ToString.Length
                Select Case num < 0
                    Case True : patternResult.Result = "-" & Math.Abs(num).ToString.PadLeft(length - 1, "0")
                    Case Else : patternResult.Result = num.ToString.PadLeft(length, "0")
                End Select
            Case add = 0 : patternResult.Result = "1"
        End Select
        Return patternResult.Prefix & patternResult.Result & patternResult.Suffix
    End Function

    ''' <summary>
    ''' Returns the rightmost letter of the string.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function LastLetter(ByVal eString As String) As String
        Dim patternResult = eString.FindResultAsPatternRev("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
        If patternResult.Result = "" Then Return ""

        Return patternResult.Result.RightString(1)
    End Function

    ''' <summary>
    ''' Determine whether the string contains a number.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function HasNumber(ByVal eString As String) As Boolean
        Dim output = eString.FindResultAsPattern("0123456789")
        Return Not (output.Result = "")
    End Function

    ''' <summary>
    ''' Finds the first string of characters that matches the pattern.
    ''' Prefix and suffix returns the charaters before and after the result. 
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="pattern">Pattern to use.</param>
    ''' <returns>Tuple value of strings: Prefix, Result and Suffix</returns>
    <Extension()>
    Public Function FindResultAsPattern(ByVal eString As String, pattern As String) As (Prefix As String, Result As String, Suffix As String)
        Dim prefix = ""
        Dim result = ""
        Dim suffix = ""
        Dim hasValue = False
        For i = 1 To eString.Length
            Select Case True
                Case pattern.Contains(eString.MidString(i, 1))
                    hasValue = True
                    result &= eString.MidString(i, 1)
                Case hasValue
                    suffix = eString.MidString(i)
                    Exit For
                Case Else
                    prefix &= eString.MidString(i, 1)
            End Select
        Next
        Return (prefix, result, suffix)
    End Function

    ''' <summary>
    ''' Finds the most right string of characters that matches the pattern.
    ''' Prefix and suffix returns the charaters before and after the result. 
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="pattern">Pattern to use.</param>
    ''' <returns>Tuple value of strings: Prefix, Result and Suffix</returns>
    <Extension()>
    Public Function FindResultAsPatternRev(ByVal eString As String, pattern As String) As (Prefix As String, Result As String, Suffix As String)
        Dim prefix = ""
        Dim result = ""
        Dim suffix = ""
        Dim hasValue = False
        For i = eString.Length To 1 Step -1
            Select Case True
                Case pattern.Contains(eString.MidString(i, 1))
                    hasValue = True
                    result = eString.MidString(i, 1) & result
                Case hasValue
                    prefix = eString.LeftString(i)
                    Exit For
                Case Else
                    suffix = eString.MidString(i, 1) & suffix
            End Select
        Next
        If Not hasValue Then prefix = suffix : suffix = ""
        Return (prefix, result, suffix)
    End Function

    ''' <summary>
    ''' Returns the string excluding a specific number of characters at the begining of the string.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="length">Number of characters to exclude.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function EraseStart(ByVal eString As String, length As Integer) As String
        Return eString.MidString(length + 1)
    End Function

    ''' <summary>
    ''' Returns the string excluding a specific number of characters at the end of the string.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="length">Number of characters to exclude.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function EraseEnd(ByVal eString As String, length As Integer) As String
        Return eString.LeftString(eString.Length - length)
    End Function

    ''' <summary>
    ''' Returns a string containing a specified number of characters from the left side of the string.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="length">Number of characters to return.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function LeftString(ByVal eString As String, length As Integer) As String
        Return Left(eString, length)
    End Function

    ''' <summary>
    ''' Returns a string containing a specified number of characters from the right side of the string.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="length">Number of characters to return.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function RightString(ByVal eString As String, length As Integer) As String
        Return Right(eString, length)
    End Function

    ''' <summary>
    ''' Returns a string containing a specified number of characters starting from the specified position of the string.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="start">Starting position of the charaters to return.</param>
    ''' <param name="length">Number of characters to return.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function MidString(ByVal eString As String, start As Integer, Optional length As Integer = -1) As String
        Select Case length = -1
            Case True : Return Mid(eString, start)
            Case Else : Return Mid(eString, start, length)
        End Select
    End Function

    ''' <summary>
    ''' Returns the number contained in the string as a double data type.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function ToDouble(ByVal eString As String) As Double
        Return Val(eString.Replace(",", "."))
    End Function

    ''' <summary>
    ''' Returns the number contained in the string as a integer data type.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function ToInteger(ByVal eString As String) As Integer
        Return CInt(Val(eString))
    End Function

    ''' <summary>
    ''' Gets the character code of the character on a specified position.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="position">Position of character to be used.</param>
    ''' <returns>Character code.</returns>
    <Extension()>
    Public Function GetAscii(ByVal eString As String, position As Integer) As Integer
        Return Asc(eString.MidString(position, 1))
    End Function

    ''' <summary>
    ''' Returns the character on a specified position as a char data type.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="position">Position of character to be used.</param>
    ''' <returns>A char type string.</returns>
    <Extension()>
    Public Function GetChar(ByVal eString As String, position As Integer) As Char
        Return CChar(eString.MidString(position, 1))
    End Function

End Module
