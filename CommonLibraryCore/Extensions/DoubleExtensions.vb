'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module DoubleExtensions

    ''' <summary>
    ''' Returns a string where the nummeric value will be formatted depending the size of the number.
    ''' </summary>
    ''' <param name="eDouble"></param>
    ''' <returns>String of the value with a comma for the decimals and points for thousands.</returns>
    <Extension()>
    Public Function ToFormatedString(ByVal eDouble As Double) As String
        Return ToFormatedString(eDouble, False)
    End Function

    ''' <summary>
    ''' Returns a string where the nummeric value will be formatted depending the size of the number.
    ''' </summary>
    ''' <param name="eDouble"></param>
    ''' <param name="asDefault">If true, the string of the value wil be with a point for the decimals and commas for thousands.</param>
    ''' <returns>String of the value.</returns>
    <Extension()>
    Public Function ToFormatedString(ByVal eDouble As Double, asDefault As Boolean) As String
        Dim format As String
        Select Case eDouble
            Case Is < 1 : format = "0.###"
            Case Is < 10 : format = "#.##"
            Case Is < 100 : format = "#.#"
            Case Else : format = "#,###,###,###,###"
        End Select
        Select Case asDefault
            Case True : Return Strings.Format(eDouble, format)
            Case Else : Return Strings.Format(eDouble, format).Replace(".", "!").Replace(",", ".").Replace("!", ",")
        End Select
    End Function

    ''' <summary>
    ''' Gets average of the value and the specified value.
    ''' </summary>
    ''' <param name="eDouble"></param>
    ''' <param name="value">Specified value.</param>
    ''' <returns>Average value</returns>
    <Extension()>
    Public Function Average(ByVal eDouble As Double, value As Double) As Double
        Return (eDouble + value) / 2
    End Function

    ''' <summary>
    ''' Compares the value with the specified value within the allowed tolerance.
    ''' </summary>
    ''' <param name="eDouble"></param>
    ''' <param name="value">Specified value.</param>
    ''' <param name="tolerance">Allowed tolerance.</param>
    ''' <returns>True if values are equal within tolerance.</returns>
    <Extension()>
    Public Function NearlyEquals(ByVal eDouble As Double, value As Double, tolerance As Double) As Boolean
        Return eDouble > value - tolerance AndAlso eDouble < value + tolerance
    End Function

End Module
