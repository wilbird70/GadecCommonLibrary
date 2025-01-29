'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

''' <summary>
''' Singleton that holds the language database and raises the LanguageChangedEvent the first time used and when the language has changed.
''' </summary>
Public Class Translator
    ''' <summary>
    ''' DataTable in singleton.
    ''' </summary>
    ''' <returns>Language database.</returns>
    Private ReadOnly Property TranslateData As DataTable = Nothing
    ''' <summary>
    ''' Contains the available languages.
    ''' </summary>
    Private ReadOnly Property AvailableLanguages As String()
    ''' <summary>
    ''' Contains the current selected language.
    ''' </summary>
    Private ReadOnly Property SelectedLanguage As String = ""
    ''' <summary>
    ''' The full name of the language xml-file.
    ''' </summary>
    Private ReadOnly _translateFileName As String

    ''' <summary>
    ''' Private constructor for the singleton.
    ''' </summary>
    ''' <param name="languageFile">The full name of the language xml-file.</param>
    Private Sub New(languageFile As String)
        _translateFileName = languageFile
        LoadTranslateFileAndSetLanguage(-1)
    End Sub

    ''' <summary>
    ''' Loads the language database and sets the current language.
    ''' </summary>
    ''' <param name="index">The index number of the selected language.</param>
    Private Sub LoadTranslateFileAndSetLanguage(index As Integer)
        _TranslateData = DataSetHelper.LoadFromXml(_translateFileName).GetTable("Lang", "Name")
        _AvailableLanguages = If(IsNothing(_TranslateData), {}, _TranslateData.GetTable("Available").GetStringsFromColumn("Name"))
        Dim previous = Registerizer.UserSetting("Language")

        Select Case True
            Case _SelectedLanguage = "" : _SelectedLanguage = If(AvailableLanguages.Contains(previous), previous, "EN") : previous = ""
            Case index < 0
            Case AvailableLanguages.Count < 1
            Case AvailableLanguages.Count <= index
            Case Else : _SelectedLanguage = AvailableLanguages(index)
        End Select
        If _SelectedLanguage = previous Then Exit Sub

        Registerizer.UserSetting("Language", _SelectedLanguage)
    End Sub

    '///shared part of class\\\

    ''' <summary>
    ''' The singleton-Object.
    ''' </summary>
    Private Shared _translator As Translator = Nothing
    ''' <summary>
    ''' Event that occurs the first time used and when the language has changed.
    ''' </summary>
    Public Shared Event LanguageChangedEvent As EventHandler(Of LanguageChangedEventArgs)

    'shared subs

    ''' <summary>
    ''' Initializes the translator:
    ''' <para>Loads the language database and sets the language to the previous selected.</para>
    ''' </summary>
    ''' <param name="languageFile">The full name of the language xml-file.</param>
    Public Shared Sub Initialize(languageFile As String)
        _translator = New Translator(languageFile)

        Dim eventArgs = New LanguageChangedEventArgs(_translator.SelectedLanguage, _translator.AvailableLanguages)
        RaiseEvent LanguageChangedEvent(Nothing, eventArgs)
    End Sub

    ''' <summary>
    ''' Reloads the language file and sets the new language for the translator.
    ''' </summary>
    ''' <param name="index">Index number of the language to set.</param>
    Public Shared Sub SetLanguange(Optional index As Integer = -1)
        If IsNothing(_translator) Then MessageBoxInfo("Translator is not initialized.") : Exit Sub

        _translator.LoadTranslateFileAndSetLanguage(index)
        Dim eventArgs = New LanguageChangedEventArgs(_translator.SelectedLanguage, _translator.AvailableLanguages)
        RaiseEvent LanguageChangedEvent(Nothing, eventArgs)
    End Sub

    ''' <summary>
    ''' Translates the controls associated with the specified parent.
    ''' </summary>
    ''' <param name="parent">The parent control.</param>
    Public Shared Sub TranslateControls(ByVal parent As Control)
        For Each control In parent.Controls.ToArray
            Dim translation = control.Name.Translate
            If Not translation = "..." Then control.Text = translation
            If control.HasChildren Then TranslateControls(control)
        Next
    End Sub

    'shared functions

    ''' <summary>
    ''' Gets the current selected language.
    ''' </summary>
    ''' <returns>Language code, eg 'EN'.</returns>
    Public Shared Function Selected() As String
        If IsNothing(_translator) Then MessageBoxInfo("Translator is not initialized.") : Return ""

        Return _translator.SelectedLanguage
    End Function

    ''' <summary>
    ''' Gets the corresponding text from the language-database in the current language.
    ''' </summary>
    ''' <param name="key">The key to find the translation.</param>
    ''' <returns>The translated text.</returns>
    Friend Shared Function Translate(key As String) As String
        If IsNothing(_translator) Then MessageBoxInfo("Translator is not initialized.") : Return "NNN"
        If IsNothing(_translator.TranslateData) Then Return "NNN"

        Dim translateInfo = _translator.TranslateData.Rows.Find(key)
        If IsNothing(translateInfo) Then Return "..."

        Dim output = translateInfo.GetString(_translator.SelectedLanguage)
        If output = "" Then output = translateInfo.GetString("EN")
        If output = "" Then output = "..."
        Return output
    End Function

End Class

Public Module TranslateExtensions

    ''' <summary>
    ''' Gets the corresponding text from the language-database in the current language.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="parameters">Parameters to insert on the numbered codes eg {0}.</param>
    ''' <returns>The translated text composed with parameters and/or special characters.</returns>
    <Extension()>
    Public Function Translate(ByVal eString As String, ParamArray parameters() As String) As String
        Return Translator.Translate(eString).Compose(parameters)
    End Function

    ''' <summary>
    ''' Text to be translated, but not yet in the language-database.
    ''' String is only past through <see cref="Compose"/>, which replaces the compose-items in the string with parameters or special characters.
    ''' </summary>
    ''' <param name="eString"></param>
    ''' <param name="parameters">Parameters to insert on numbered codes eg {0}.</param>
    ''' <returns>Text composed with parameters and/or special characters.</returns>
    <Extension()>
    Public Function NotYetTranslated(ByVal eString As String, ParamArray parameters() As String) As String
        Return eString.Compose(parameters)
    End Function

    ''' <summary>
    ''' Gets the description from the record in the current language.
    ''' </summary>
    ''' <param name="eDataRow"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetTranslation(ByVal eDataRow As DataRow) As String
        Dim output = eDataRow.GetString(Translator.Selected)
        If output = "" Then output = eDataRow.GetString("EN")
        If output = "" Then output = "..."
        Return output
    End Function

    ''' <summary>
    ''' Gets the description from the record in the current language.
    ''' </summary>
    ''' <param name="eDataGridViewRow"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetTranslation(ByVal eDataGridViewRow As DataGridViewRow) As String
        Dim output = eDataGridViewRow.GetString(Translator.Selected)
        If output = "" Then output = eDataGridViewRow.GetString("EN")
        If output = "" Then output = "..."
        Return output
    End Function

End Module
