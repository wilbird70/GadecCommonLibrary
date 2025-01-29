'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Provides data for the LanguageChangedEvent.
''' </summary>
Public Class LanguageChangedEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Gets the selected language.
    ''' </summary>
    ''' <returns>Language code, eg 'EN'.</returns>
    Public ReadOnly Property Selected As String
    ''' <summary>
    ''' Gets the available languages.
    ''' </summary>
    ''' <returns>List of language codes.</returns>
    Public ReadOnly Property AvialableLanguages As String()

    ''' <summary>
    ''' Initializes a new instance of the <see cref="LanguageChangedEventArgs"/> with the specified properties.
    ''' </summary>
    ''' <param name="selectedLanguage">Code of selected language.</param>
    ''' <param name="avialableLanguages">Codes of avialable languages.</param>
    Public Sub New(selectedLanguage As String, avialableLanguages As String())
        _Selected = selectedLanguage
        _AvialableLanguages = avialableLanguages
    End Sub

End Class
