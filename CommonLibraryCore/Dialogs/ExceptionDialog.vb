'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' <see cref="ExceptionDialog"/> is a detailed dialog of an exception with the option to email to the developer.
''' </summary>
Friend Class ExceptionDialog
    ''' <summary>
    ''' Previous height of the form.
    ''' </summary>
    Private _formHeight As Integer
    ''' <summary>
    ''' Previous width of the form.
    ''' </summary>
    Private _formWidth As Integer
    ''' <summary>
    ''' Determine if the dialog is still loading.
    ''' </summary>
    Private ReadOnly _dialogLoading As Boolean = True
    ''' <summary>
    ''' The detailed exception message in separate lines.
    ''' </summary>
    Private ReadOnly _message As List(Of String)
    ''' <summary>
    ''' The address to which the exception report can be sent.
    ''' </summary>
    Private ReadOnly _emailAdress As String
    ''' <summary>
    ''' The name and version of the application.
    ''' </summary>
    Private ReadOnly _appNameAndVersion As String

    'form

    ''' <summary>
    ''' Initializes a new instance the <see cref="ExceptionDialog"/> of the specified exception.
    ''' <para><see cref="ExceptionDialog"/> is a detailed dialog of an exception with the option to email to the developer.</para>
    ''' </summary>
    ''' <param name="exception">The present exception.</param>
    ''' <param name="appName">The name and version of the present app..</param>
    ''' <param name="appBuild">The lastwritetime of the present app.</param>
    ''' <param name="methodeName">The name of the method that caught the exception.</param>
    ''' <param name="emailAddress">The emailaddress to send the feedback to.</param>
    Public Sub New(exception As Exception, appName As String, appBuild As String, methodeName As String, emailAddress As String)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Me.Text = appName
        TranslateDialog(Translator.Selected)

        _appNameAndVersion = appName
        _emailAdress = emailAddress
        _message = {
            "#Occured on:{T}{0}".Compose(Format(Now, "dd-MM-yyyy - HH:mm:ss")),
            "#Occured in:{T}{0}".Compose(exception?.TargetSite.Name),
            "#Catched in:{T}{0}".Compose(methodeName),
            "#Gadec build:{T}{0}".Compose(appBuild),
            "#Message:{T}{0}".Compose(exception?.Message)
        }.ToList
        _message.AddRange(GetLocalVariables(exception))
        _message.AddRange(GetStackTrace(exception))
        _message.Add("")

        OutputTextBox.Text = String.Join(vbLf, _message)
        WriteExceptionTextToLogfile()

        _formHeight = Me.Height
        _formWidth = Me.Width
        _dialogLoading = False
        Me.TopMost = True
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user resizes this dialog box.
    ''' <para>It also changes the size and location of the controls.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Me_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Try
            If _dialogLoading Then Exit Sub

            Me.Width = {300, Me.Width}.Max
            Me.Height = {158, Me.Height}.Max
            Dim heightDifference = Me.Height - _formHeight
            If Not heightDifference = 0 Then
                ltSend.Top += heightDifference
                ltClose.Top += heightDifference
                OutputTextBox.Height += heightDifference
                CaptionLabel.Top += heightDifference
                QuestionLabel.Top += heightDifference
            End If
            Dim widthDifference = Me.Width - _formWidth
            If Not widthDifference = 0 Then
                ltClose.Left += widthDifference
                ltSend.Left += widthDifference
                OutputTextBox.Width += widthDifference
            End If
            _formWidth = Me.Width
            _formHeight = Me.Height
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    'buttons

    ''' <summary>
    ''' EventHandler for the event that occurs when user clicks the accept button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AcceptButton_Click(sender As Object, e As EventArgs) Handles ltSend.Click
        Try
            _message.AddRange({"", ""})
            Dim exceptionText = String.Join("{P}".Compose, _message)
            ProcessHelper.StartMailMessage(_emailAdress, "Report Unhandled Exception {0}".Compose(_appNameAndVersion), exceptionText)
            Me.Hide()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when user clicks the cancel button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles ltClose.Click
        Try
            Me.Hide()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    'private subs
    ''' <summary>
    ''' Translates the dialog to the specified language.
    ''' <para>Note: Only 'EN', 'NL', 'DE' and 'FR' can be used. Other language code results in English.</para>
    ''' </summary>
    ''' <param name="language">Language code, eg 'EN'.</param>
    Private Sub TranslateDialog(language As String)
        Select Case language
            Case "NL"
                CaptionLabel.Text = "Er is een onverwerkte fout opgetreden!"
                QuestionLabel.Text = "Laten we een bericht maken voor de ontwikkelaar..."
                ltSend.Text = "Verzenden"
                ltClose.Text = "Sluiten"
            Case "DE"
                CaptionLabel.Text = "Es ist ein nicht behandelter Fehler aufgetreten!"
                QuestionLabel.Text = "Lassen Sie uns eine Nachricht an den Entwickler senden..."
                ltSend.Text = "Senden"
                ltClose.Text = "Schließen"
            Case "FR"
                CaptionLabel.Text = "Une erreur non gérée s'est produite!"
                QuestionLabel.Text = "Faisons un message pour le développeur..."
                ltSend.Text = "Envoyer"
                ltClose.Text = "Fermer"
            Case Else
                CaptionLabel.Text = "Unhandled error has occurred!"
                QuestionLabel.Text = "Let's make a message for the developer..."
                ltSend.Text = "Send"
                ltClose.Text = "Close"
        End Select
    End Sub

    ''' <summary>
    ''' Writes the detailed exception message to a logfile in the appdata folder.
    ''' </summary>
    Private Sub WriteExceptionTextToLogfile()
        Try
            FileSystemHelper.CreateFolder("{AppData}".Compose)
            Using streamWriter = IO.File.AppendText("{AppData}\LogMessageException.log".Compose)
                For Each line In _message
                    streamWriter.WriteLine(line)
                Next
            End Using
        Catch ex As IO.FileNotFoundException
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    'private functions

    ''' <summary>
    ''' Gets the stack trace of the exception and removes the extra lines caused by rethrows.
    ''' </summary>
    ''' <param name="exception">The present exception.</param>
    ''' <returns>A list of lines with the stack trace.</returns>
    Private Function GetStackTrace(exception As Exception) As String()
        If IsNothing(exception) Then Return {}

        Dim output = {"#Stacktrace:"}.ToList
        output.AddRange(exception.StackTrace.Cut(vbCrLf))
        For i = output.Count - 2 To 0 Step -1
            Select Case True
                Case output(i).Contains("ExceptionExtensions.ReThrow") : output.RemoveAt(i + 1) : output.RemoveAt(i)
                Case output(i).Contains("---") : output.RemoveAt(i + 1) : output.RemoveAt(i)
            End Select
        Next
        Return output.ToArray
    End Function

    ''' <summary>
    ''' Retrieves the values of local variables, when added with the AddData extension method.
    ''' </summary>
    ''' <param name="exception">The present exception.</param>
    ''' <returns>A list of lines with the values of added local variables.</returns>
    Private Function GetLocalVariables(exception As Exception) As String()
        If IsNothing(exception) Then Return {}

        Dim output = {"#Local variables:"}.ToList
        For Each key In exception.Data.Keys
            output.Add("   {0} - {1}".Compose(key, exception.Data(key)))
        Next
        Return output.ToArray
    End Function

End Class