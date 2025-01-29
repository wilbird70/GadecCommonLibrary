'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Provides methodes for starting other processes (outside the app).
''' </summary>
Public Class ProcessHelper

    ''' <summary>
    ''' Starts explorer in the specified folder.
    ''' </summary>
    ''' <param name="folderName">The full name of the folder.</param>
    ''' <returns>The new <see cref="Process"/>.</returns>
    Public Shared Function StartExplorer(folderName As String) As Process
        Try
            Windows.Input.Mouse.OverrideCursor = Windows.Input.Cursors.AppStarting
            Return Process.Start("explorer.exe", "{Q}{0}{Q}".Compose(folderName))
        Catch ex As Exception
            Return Nothing
        Finally
            Windows.Input.Mouse.OverrideCursor = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Starts explorer in the folder of the specified file and selects the file.
    ''' </summary>
    ''' <param name="fileName">The full name of the file.</param>
    ''' <returns>The new <see cref="Process"/>.</returns>
    Public Shared Function StartExplorerAndSelectFile(fileName As String) As Process
        Try
            Windows.Input.Mouse.OverrideCursor = Windows.Input.Cursors.AppStarting
            Return Process.Start("explorer.exe", "/select, {Q}{0}{Q}".Compose(fileName))
        Catch ex As Exception
            Return Nothing
        Finally
            Windows.Input.Mouse.OverrideCursor = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Opens the specified document in a new <see cref="Process"/>.
    ''' </summary>
    ''' <param name="fileName">The full name of the file.</param>
    ''' <returns>The new <see cref="Process"/>.</returns>
    Public Shared Function StartDocument(fileName As String) As Process
        Dim process As New Process
        process.StartInfo.FileName = fileName
        process.StartInfo.UseShellExecute = True
        process.StartInfo.RedirectStandardOutput = False
        process.Start()
        Return process
    End Function

    ''' <summary>
    ''' Starts a new <see cref="Process"/> specified by the string and overrides the mouse cursor with the app starting icon while busy.
    ''' </summary>
    ''' <param name="processString">String expression of the process to start.</param>
    ''' <returns>The new <see cref="Process"/>.</returns>
    Public Shared Function StartProcess(processString As String) As Process
        Try
            Windows.Input.Mouse.OverrideCursor = Windows.Input.Cursors.AppStarting
            Return Process.Start(processString)
        Catch ex As Exception
            Return Nothing
        Finally
            Windows.Input.Mouse.OverrideCursor = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Starts a new browser(-tab) and opens the specified url and overrides the mouse cursor with the app starting icon while busy.
    ''' </summary>
    ''' <param name="url">The url-address to browse for.</param>
    ''' <returns>The new <see cref="Process"/>.</returns>
    Public Shared Function StartBrowser(url As String) As Process
        Try
            Windows.Input.Mouse.OverrideCursor = Windows.Input.Cursors.AppStarting
            Return Process.Start(url)
        Catch ex As Exception
            Return Nothing
        Finally
            Windows.Input.Mouse.OverrideCursor = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Starts a new email item with the specified subject and content.
    ''' </summary>
    ''' <param name="emailSubject">The subject of the new email.</param>
    ''' <param name="emailBody">The content of the new email.</param>
    ''' <returns>The new <see cref="Process"/>.</returns>
    Public Shared Function StartMailMessage(emailAddress As String, emailSubject As String, emailBody As String) As Process
        Try
            Windows.Input.Mouse.OverrideCursor = Windows.Input.Cursors.AppStarting
            Return Process.Start("mailto:{0}?subject={1}&body={2}".Compose(emailAddress, emailSubject.Replace("&", ""), emailBody.Replace("&", "")))
        Catch ex As Exception
            Return Nothing
        Finally
            Windows.Input.Mouse.OverrideCursor = Nothing
        End Try
    End Function

End Class
