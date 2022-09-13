'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module CharExtensions

    ''' <summary>
    ''' Gets the character code of the character.
    ''' </summary>
    ''' <param name="eChar"></param>
    ''' <returns>Character code.</returns>
    <Extension()>
    Public Function GetAscii(ByVal eChar As Char) As Integer
        Return Asc(eChar)
    End Function

End Module
