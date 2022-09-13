'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Singleton that holds the section settings.
''' </summary>
Public Class Registerizer
    ''' <summary>
    ''' The dictionary that holds the settings.
    ''' </summary>
    Private ReadOnly _settings As Dictionary(Of String, String)

    ''' <summary>
    ''' Private constructor for the singleton.
    ''' </summary>
    ''' <param name="settings"></param>
    Private Sub New(settings As Dictionary(Of String, String))
        _settings = settings
    End Sub

    ''' <summary>
    ''' Gets or sets a setting from/to the registery.
    ''' </summary>
    ''' <param name="section">The section where setting is stored.</param>
    ''' <param name="name">The name of the setting.</param>
    ''' <param name="newValue">Optional new value for the setting.</param>
    ''' <returns></returns>
    Private Function Setting(section As String, name As String, Optional newValue As String = "$$$") As String
        If Not newValue = "$$$" Then SaveSetting(_settings("Company"), _settings(section), name, newValue)
        Return GetSetting(_settings("Company"), _settings(section), name, "")
    End Function

    ''' <summary>
    ''' Gets or sets a database from/to the registery.
    ''' <para>NB: Only small databases allowed!</para>
    ''' </summary>
    ''' <param name="section">The section where setting is stored.</param>
    ''' <param name="name">The name of the setting.</param>
    ''' <param name="newValue">Optional new value for the setting.</param>
    ''' <returns>The database</returns>
    Private Function Data(section As String, name As String, Optional newValue As DataTable = Nothing) As DataTable
        If NotNothing(newValue) Then
            newValue.TableName = name
            Dim stringWriter = New IO.StringWriter()
            newValue.WriteXml(stringWriter)
            SaveSetting(_settings("Company"), _settings(section), name, stringWriter.ToString)
        End If
        Dim buffer = GetSetting(_settings("Company"), _settings(section), name, "")
        If buffer = "" Then Return Nothing

        Dim dataSet = New DataSet("GadecRegistry")
        Dim stringReader = New IO.StringReader(buffer)
        dataSet.ReadXml(stringReader)
        Return dataSet.Tables(0)
    End Function

    '///shared part of class\\\

    ''' <summary>
    ''' The singleton-Object.
    ''' </summary>
    Private Shared _registerizer As Registerizer

    'shared subs

    ''' <summary>
    ''' Initializes the Registerizer.
    ''' </summary>
    ''' <param name="company">The name of the company of the app.</param>
    ''' <param name="mainKey">Section for mainsettings, usually the name of the app.</param>
    ''' <param name="subKey">Section for usersettings, usually 'app-name\Settings'.</param>
    ''' <param name="sub2Key">A secundairy section for usersettings when needed.</param>
    Public Shared Sub Initialize(company As String, mainKey As String, Optional subKey As String = "", Optional sub2Key As String = "")
        If company = "" Or mainKey = "" Then Exit Sub

        subKey = If(subKey = "", mainKey, subKey)
        sub2Key = If(sub2Key = "", subKey, sub2Key)
        Dim settings = New Dictionary(Of String, String) From {{"Company", company}, {"MainKey", mainKey}, {"UserKey", subKey}, {"User2Key", sub2Key}}
        _registerizer = New Registerizer(settings)
    End Sub

    'shared functions

    ''' <summary>
    ''' Gets the application-name and -version that are registered at the main settings in 'mod0' and 'ver0'.
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetApplicationVersion() As String
        If IsNothing(_registerizer) Then Throw New Exception("Registerizer not initialized")

        Return "{0} {1}".Compose(MainSetting("mod0"), MainSetting("ver0"))
    End Function

    ''' <summary>
    ''' Gets or sets a string from/to the registery at the initially specified main section.
    ''' </summary>
    ''' <param name="name">The name of the setting.</param>
    ''' <param name="newValue">Optional new value for the setting.</param>
    ''' <returns>The string.</returns>
    Public Shared Function MainSetting(name As String, Optional newValue As String = "$$$") As String
        If IsNothing(_registerizer) Then Throw New Exception("Registerizer not initialized")

        Return _registerizer.Setting("MainKey", name, newValue)
    End Function

    ''' <summary>
    ''' Gets or sets a string from/to the registery at the initially specified user section.
    ''' </summary>
    ''' <param name="name">The name of the setting.</param>
    ''' <param name="newValue">Optional new value for the setting.</param>
    ''' <returns>The string.</returns>
    Public Shared Function UserSetting(name As String, Optional newValue As String = "$$$") As String
        If IsNothing(_registerizer) Then Throw New Exception("Registerizer not initialized")

        Return _registerizer.Setting("UserKey", name, newValue)
    End Function

    ''' <summary>
    ''' Gets or sets a string from/to the registery at the initially specified secudairy user section.
    ''' </summary>
    ''' <param name="name">The name of the setting.</param>
    ''' <param name="newValue">Optional new value for the setting.</param>
    ''' <returns>The string.</returns>
    Public Shared Function User2Setting(name As String, Optional newValue As String = "$$$") As String
        If IsNothing(_registerizer) Then Throw New Exception("Registerizer not initialized")

        Return _registerizer.Setting("User2Key", name, newValue)
    End Function

    ''' <summary>
    ''' Gets or sets a database from/to the registery at the initially specified main section.
    ''' <para>NB: Only small databases allowed!</para>
    ''' </summary>
    ''' <param name="name">The name of the setting.</param>
    ''' <param name="newValue">Optional new value for the setting.</param>
    ''' <returns>The database.</returns>
    Public Shared Function MainData(name As String, Optional newValue As DataTable = Nothing) As DataTable
        If IsNothing(_registerizer) Then Throw New Exception("Registerizer not initialized")

        Return _registerizer.Data("MainKey", name, newValue)
    End Function

    ''' <summary>
    ''' Gets or sets a database from/to the registery at the initially specified user section.
    ''' <para>NB: Only small databases allowed!</para>
    ''' </summary>
    ''' <param name="name">The name of the setting.</param>
    ''' <param name="newValue">Optional new value for the setting.</param>
    ''' <returns>The database.</returns>
    Public Shared Function UserData(name As String, Optional newValue As DataTable = Nothing) As DataTable
        If IsNothing(_registerizer) Then Throw New Exception("Registerizer not initialized")

        Return _registerizer.Data("UserKey", name, newValue)
    End Function

    ''' <summary>
    ''' Gets or sets a database from/to the registery at the initially specified secudairy user section.
    ''' <para>NB: Only small databases allowed!</para>
    ''' </summary>
    ''' <param name="name">The name of the setting.</param>
    ''' <param name="newValue">Optional new value for the setting.</param>
    ''' <returns>The database.</returns>
    Public Shared Function User2Data(name As String, Optional newValue As DataTable = Nothing) As DataTable
        If IsNothing(_registerizer) Then Throw New Exception("Registerizer not initialized")

        Return _registerizer.Data("User2Key", name, newValue)
    End Function

End Class
