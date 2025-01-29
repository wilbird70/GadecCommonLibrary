'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module DateExtensions
    ''' <summary>
    ''' Constant representing a five second period.
    ''' </summary>
    Private Const _fiveSeconds = 0.00005787  'vijf seconden
    ''' <summary>
    ''' Constant representing an hour period.
    ''' </summary>
    Private Const _oneHour = 0.04166667      'een uur

    ''' <summary>
    ''' Gets a formated date and time string to store in a textbased database (to store in xml-file).
    ''' </summary>
    ''' <param name="eDate"></param>
    ''' <returns>Formated date-time string: 'yyyy-MM-dd@HH.mm.ss'.</returns>
    <Extension()>
    Public Function ToTimeStamp(ByVal eDate As Date) As String
        Select Case IsNothing(eDate)
            Case True : Return "....-..-..@..-..-.."
            Case Else : Return Format(eDate, "yyyy-MM-dd@HH.mm.ss")
        End Select
    End Function

    ''' <summary>
    ''' Compare filedates and return if they are equal or not. Caused by different file systems, the comparison neglects minor differences and the difference in daylight saving time.
    ''' </summary>
    ''' <param name="eDate"></param>
    ''' <param name="compareDate"></param>
    ''' <returns>True if dates are equal.</returns>
    <Extension()>
    Public Function FileDateEquals(ByVal eDate As Date, compareDate As Date) As Boolean
        Dim firstOADate = eDate.ToOADate
        Dim secondOADate = compareDate.ToOADate
        Select Case True
            Case firstOADate = secondOADate : Return True
            Case firstOADate.NearlyEquals(secondOADate, _fiveSeconds) : Return True
            Case firstOADate > secondOADate : Return firstOADate.NearlyEquals(secondOADate + _oneHour, _fiveSeconds)
            Case firstOADate < secondOADate : Return firstOADate.NearlyEquals(secondOADate - _oneHour, _fiveSeconds)
            Case Else : Return False
        End Select
    End Function

    'todate extensions

    ''' <summary>
    ''' Parse a formated date-time string ('yyyy-MM-dd@HH.mm.ss') to a date datatype.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <returns>Date datatype.</returns>
    <Extension()>
    Public Function TimeStampToDate(ByVal eString As String) As Date
        Return DateTime.ParseExact(eString, "yyyy-MM-dd@HH.mm.ss", Nothing)
    End Function

    ''' <summary>
    ''' Parse a double to a date datatype.
    ''' </summary>
    ''' <param name="eDouble"></param>
    ''' <returns>Date datatype.</returns>
    <Extension()>
    Public Function ToDate(ByVal eDouble As Double) As Date
        Return Date.FromOADate(eDouble)
    End Function

End Module
