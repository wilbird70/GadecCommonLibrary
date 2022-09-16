'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Provides some general methods.
''' </summary>
Public Module Functions

    ''' <summary>
    ''' A type to indicate what should be used in the AddRange dictionaries method.
    ''' <para><see cref="AddMode.Override"/> overrides values of existing keys.</para>
    ''' <para><see cref="AddMode.NewOnly"/> will ignore existing keys.</para>
    ''' </summary>
    Public Enum AddMode As Byte
        Override = 1
        NewOnly = 2
    End Enum

    ''' <summary>
    ''' An inverted function of IsNothing.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <returns>True if expression is an object, otherwise False.</returns>
    Public Function NotNothing(expression As Object) As Boolean
        Return Not IsNothing(expression)
    End Function

    ''' <summary>
    ''' Gets the last part of the type-description of an object.
    ''' </summary>
    ''' <param name="expression"></param>
    ''' <returns>The partly type-description</returns>
    Public Function GetObjectType(expression As Object) As String
        Dim type = expression.GetType.ToString
        Return type.MidString(InStrRev(type, ".") + 1)
    End Function

    ''' <summary>
    ''' Shows a detailed dialog of an exception with the option to email to the developer.
    ''' </summary>
    ''' <param name="exception">The exception to show.</param>
    ''' <param name="appName">The name and version of the present app..</param>
    ''' <param name="appBuild">The lastwritetime of the present app.</param>
    ''' <param name="stackBack">Sometimes an app has an extra methode for exceptions to add data. Enter the number of cascades.</param>
    Public Function MessageBoxException(exception As Exception, appName As String, appBuild As String, Optional stackBack As Integer = 0)
        Dim methodeName = (New StackTrace).GetFrame(1 + stackBack).GetMethod.Name
        Dim dialog = New ExceptionDialog(exception, appName, appBuild, methodeName, "gadec.engineerings.software@outlook.com")
        dialog.Show()
        Beep()
        Return Nothing
    End Function

    ''' <summary>
    ''' Shows a dialog with the available app-history, stored in the SetHistory.xml-file found in the apps supportfolder.
    ''' </summary>
    Public Sub MessageBoxHistory()
        Dim dialog = New HistoryDialog(DataSetHelper.LoadFromXml("{Support}\SetHistory.xml".Compose).GetTable("Versions", "Name"))
        Registerizer.MainSetting("AppUpdate", "Log-Showed")
        dialog.ShowDialog()
    End Sub

    ''' <summary>
    ''' Shows a <see cref="MessageBox"/> with <see cref="MessageBoxIcon.Information"/> icon and <see cref="MessageBoxButtons.OK"/> button.
    ''' </summary>
    ''' <param name="prompt">The Text to display in the message box</param>
    ''' <returns>One of the DialogResult values.</returns>
    Public Function MessageBoxInfo(prompt As String) As DialogResult
        Return MessageBox.Show(prompt, Registerizer.GetApplicationVersion, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    ''' <summary>
    ''' Shows a <see cref="MessageBox"/> with <see cref="MessageBoxIcon.Question"/> icon and with <see cref="MessageBoxButtons.YesNo"/> buttons if not otherwise specified.
    ''' </summary>
    ''' <param name="prompt">The Text to display in the message box.</param>
    ''' <param name="buttons">One of the MessageBoxButtons values that specifies which buttons to display in the message box.</param>
    ''' <returns>One of the DialogResult values.</returns>
    Public Function MessageBoxQuestion(prompt As String, Optional buttons As MessageBoxButtons = MessageBoxButtons.YesNo) As DialogResult
        Return MessageBox.Show(prompt, Registerizer.GetApplicationVersion, buttons, MessageBoxIcon.Question)
    End Function

End Module
