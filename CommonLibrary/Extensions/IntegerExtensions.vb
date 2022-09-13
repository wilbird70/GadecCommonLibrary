'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module IntegerExtensions

    ''' <summary>
    ''' Converts the number to a binary string (eg 13 -> '1101').
    ''' </summary>
    ''' <param name="eInteger"></param>
    ''' <param name="numberOfBits">Defined the number of bits (eg 8, than 13 -> '00001101').</param>
    ''' <returns>The binary string.</returns>
    <Extension()>
    Public Function ToBinairy(ByVal eInteger As Integer, Optional numberOfBits As Integer = 0) As String
        Return CLng(eInteger).ToBinairy(numberOfBits)
    End Function

    ''' <summary>
    ''' Converts the number to a binary string (eg 13 -> '1101').
    ''' </summary>
    ''' <param name="eLong"></param>
    ''' <param name="numberOfBits">Defined the number of bits (eg 8, than 13 -> '00001101').</param>
    ''' <returns>The binary string.</returns>
    <Extension()>
    Public Function ToBinairy(ByVal eLong As Long, Optional numberOfBits As Integer = 0) As String
        Dim output = ""
        eLong = Math.Abs(eLong)
        Do While eLong <> 0
            output = Trim(Str(eLong - 2 * Int(eLong / 2))) & output
            eLong = Int(eLong / 2)
        Loop
        If numberOfBits = 0 Then Return output

        Select Case Len(output) > numberOfBits
            Case True : Return "Error - Number too large for bit size"
            Case Else : Return output.PadLeft(numberOfBits, "0")
        End Select
    End Function

    ''' <summary>
    ''' Compare the binary string of the atrribute to have at least set the bits of the specified attribute. 
    ''' </summary>
    ''' <param name="eInteger"></param>
    ''' <param name="attribute">Number value of the required attribute-bits.</param>
    ''' <returns>True if atrribute turns out to be equivalent with the specified attribute.</returns>
    <Extension()>
    Public Function AttributesAreChecked(ByVal eInteger As Integer, attribute As Long) As Boolean
        Return CLng(eInteger).AttributesAreChecked(attribute)
    End Function

    ''' <summary>
    ''' Compare the binary string of the atrribute to have at least set the bits of the specified attribute. 
    ''' </summary>
    ''' <param name="eLong"></param>
    ''' <param name="attribute">Number value of the required attribute-bits.</param>
    ''' <returns>True if atrribute turns out to be equivalent with the specified attribute.</returns>
    <Extension()>
    Public Function AttributesAreChecked(ByVal eLong As Long, attribute As Long) As Boolean
        Dim binairyValue = eLong.ToBinairy
        Dim binairyAttrs = attribute.ToBinairy
        Select Case Len(binairyValue) > Len(binairyAttrs)
            Case True : binairyAttrs = binairyAttrs.PadLeft(Len(binairyValue), "0")
            Case Else : binairyValue = binairyValue.PadLeft(Len(binairyAttrs), "0")
        End Select
        For i = 1 To Len(binairyAttrs)
            If Mid(binairyAttrs, i, 1) = "0" Then Continue For
            If Mid(binairyValue, i, 1) = "0" Then Return False
        Next
        Return True
    End Function

End Module
