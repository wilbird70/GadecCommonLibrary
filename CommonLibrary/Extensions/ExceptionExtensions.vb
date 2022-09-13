'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module ExceptionExtensions

    ''' <summary>
    ''' Rethrows the exception.
    ''' </summary>
    ''' <param name="eException"></param>
    <Extension()>
    Public Sub Rethrow(ByVal eException As Exception)
        Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(eException).Throw()
    End Sub

    ''' <summary>
    ''' Adds user-defined information about the exception.
    ''' </summary>
    ''' <param name="eException"></param>
    ''' <param name="text">Information to add.</param>
    <Extension()>
    Public Sub AddData(ByVal eException As Exception, text As String)
        Dim key = "1: "
        Do While eException.Data.Contains(key)
            key = key.AutoNumber
        Loop
        eException.Data.Add(key, text)
    End Sub

End Module
