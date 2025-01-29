'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

''' <summary>
''' Singleton that holds the compose-items for special characters.
''' <para>Compose-items are codes between curly braces:</para>
''' <para>- {0}, {1}, {2} And so on for corresponding parameters;</para>
''' <para>- {L} for linefeed and {2L} for double linefeed;</para>
''' <para>- {C} for carriage-return and {CL} for carriage-return + Linefeed;</para>
''' <para>- {T} for tab and {Esc} for escape-character;</para>
''' <para>- {Q} for quote-character and {AMP} for ampersand;</para>
''' <para>- {G} for greater-than-character and {K} for less-than-character;</para>
''' <para>- {P} for linefeed in mailbody;</para>
''' <para>- {Desktop} for desktop-folder and {AppData} for appdata-folder;</para>
''' <para>and eventually supplemented by customcodes via <see cref="SetCustumCodes"/>.</para>
''' </summary>
Public Class Composer
    ''' <summary>
    ''' Dictionary in singleton.
    ''' </summary>
    ''' <returns>Compose dictionary for special characters.</returns>
    Friend ReadOnly Property Codes As Dictionary(Of String, String) = Nothing

    ''' <summary>
    ''' Private constructor for the singleton.
    ''' </summary>
    Private Sub New()
        Codes = New Dictionary(Of String, String) From {
            {"Q", Chr(34)},
            {"L", vbLf},
            {"2L", vbLf & vbLf},
            {"C", vbCr},
            {"CL", vbCr & vbLf},
            {"T", vbTab},
            {"Esc", Chr(27)},
            {"AMP", "&"},
            {"G", ">"},
            {"K", "<"},
            {"P", "%0D%0A"},
            {"Desktop", Environment.GetFolderPath(Environment.SpecialFolder.Desktop)},
            {"AppData", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}
        }
    End Sub


    ''' <summary>
    ''' The singleton-Object.
    ''' </summary>
    Private Shared _composer As Composer = Nothing

    '///shared part of class\\\

    ''' <summary>
    ''' Sets custom codes for the compose extension methode.
    ''' </summary>
    ''' <param name="codes">Dictionary with codes (keys) and coresponding texts (values).</param>
    Public Shared Sub SetCustumCodes(codes As Dictionary(Of String, String))
        _composer = New Composer
        If IsNothing(codes) Then Exit Sub

        For Each code In codes
            Select Case _composer.Codes.ContainsKey(code.Key)
                Case True : _composer.Codes(code.Key) = code.Value
                Case Else : _composer.Codes.Add(code.Key, code.Value)
            End Select
        Next
    End Sub

    ''' <summary>
    ''' Replaces the compose-items in the string with parameters and/or special characters.
    ''' </summary>
    ''' <param name="text">The text to process.</param>
    ''' <param name="parameters">Parameters to insert on numbered codes eg {0}.</param>
    ''' <returns>String expression composed with parameters and/or special characters.</returns>
    Friend Shared Function Compose(text As String, ParamArray parameters() As String) As String
        If IsNothing(_composer) Then _composer = New Composer

        Dim output = text
        Do
            Dim code = output.InStrResult("{", "}", "", False)
            If code = "" Then Exit Do

            Select Case _composer.Codes.ContainsKey(code)
                Case True : output = output.Replace("{" & code & "}", _composer.Codes(code))
                Case Else : output = output.Replace("{" & code & "}", "<" & code & ">")
            End Select
        Loop
        If parameters.Count > 0 Then
            For i = 0 To parameters.Count - 1
                output = output.Replace("<" & i.ToString & ">", parameters(i))
            Next
        End If
        Return output
    End Function

End Class

Public Module ComposeExtensions

    ''' <summary>
    ''' Replaces the compose-items in the string with parameters and/or special characters.
    ''' <para>See the <see cref="Composer"/> class for more information.</para>
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="parameters">Parameters to insert on numbered codes eg {0}.</param>
    ''' <returns>String expression composed with parameters and/or special characters.</returns>
    <Extension()>
    Public Function Compose(ByVal eString As String, ParamArray parameters() As String) As String
        Return Composer.Compose(eString, parameters)
    End Function

End Module
