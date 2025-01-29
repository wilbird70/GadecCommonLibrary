'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Provides some fonts.
''' </summary>
Public Class FontHelper

    'SansSerif

    ''' <summary>
    ''' Gets the SansSerif Regular font.
    ''' </summary>
    ''' <param name="size">Optional size for the font.</param>
    ''' <returns>The SansSerif Regular font.</returns>
    Public Shared Function SansSerifRegular(Optional size As Single = 8.25!) As Font
        Return New Font("Microsoft Sans Serif", size, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
    End Function

    ''' <summary>
    ''' Gets the SansSerif Bold font.
    ''' </summary>
    ''' <param name="size">Optional size for the font.</param>
    ''' <returns>The SansSerif Bold font.</returns>
    Public Shared Function SansSerifBold(Optional size As Single = 8.25!) As Font
        Return New Font("Microsoft Sans Serif", size, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
    End Function

    ''' <summary>
    ''' Gets the SansSerif Italic font.
    ''' </summary>
    ''' <param name="size">Optional size for the font.</param>
    ''' <returns>The SansSerif Italic font.</returns>
    Public Shared Function SansSerifItalic(Optional size As Single = 8.25!) As Font
        Return New Font("Microsoft Sans Serif", size, FontStyle.Italic, GraphicsUnit.Point, CType(0, Byte))
    End Function

    ''' <summary>
    ''' Gets the SansSerif Italic and Bold font.
    ''' </summary>
    ''' <param name="size">Optional size for the font.</param>
    ''' <returns>The SansSerif Italic and Bold font.</returns>
    Public Shared Function SansSerifItalicBold(Optional size As Single = 8.25!) As Font
        Return New Font("Microsoft Sans Serif", size, FontStyle.Italic + FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
    End Function

    'Arial

    ''' <summary>
    ''' Gets the Arial Regular font.
    ''' </summary>
    ''' <param name="size">Optional size for the font.</param>
    ''' <returns>The Arial Regular font.</returns>
    Public Shared Function ArialRegular(Optional size As Single = 8.25!) As Font
        Return New Font("Arial", size, FontStyle.Regular, GraphicsUnit.Point)
    End Function

    ''' <summary>
    ''' Gets the Arial Bold font.
    ''' </summary>
    ''' <param name="size">Optional size for the font.</param>
    ''' <returns>The Arial Bold font.</returns>
    Public Shared Function ArialBold(Optional size As Single = 8.25!) As Font
        Return New Font("Arial", size, FontStyle.Bold, GraphicsUnit.Point)
    End Function

    'Arial Narrow

    ''' <summary>
    ''' Gets the Arial Narrow Regular font.
    ''' </summary>
    ''' <param name="size">Optional size for the font.</param>
    ''' <returns>The Arial Narrow Regular font.</returns>
    Public Shared Function ArialNarrowRegular(Optional size As Single = 8.25!) As Font
        Return New Font("Arial Narrow", size, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
    End Function

    ''' <summary>
    ''' Gets the Arial Narrow Bold font.
    ''' </summary>
    ''' <param name="size">Optional size for the font.</param>
    ''' <returns>The Arial Narrow Bold font.</returns>
    Public Shared Function ArialNarrowBold(Optional size As Single = 8.25!) As Font
        Return New Font("Arial Narrow", size, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
    End Function

End Class
