'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Provides methodes for the file system.
''' </summary>
Public Class FileSystemHelper

    ''' <summary>
    ''' Provides a <see cref="SaveFileDialog"/> dialogbox.
    ''' </summary>
    ''' <param name="initialFileName">The full name of the initial file to use.</param>
    ''' <param name="title">Optional title of the dialogbox.</param>
    ''' <returns>The selected or given filename.</returns>
    Public Shared Function FileSaveAs(initialFileName As String, Optional title As String = "") As String
        Dim folder = IO.Path.GetDirectoryName(initialFileName)
        Dim file = IO.Path.GetFileName(initialFileName)
        If folder = "\" Then folder = "{Desktop}".Compose
        Dim dialog = New SaveFileDialog With {
            .Filter = "{0} files (*{0})|*{0}|All files (*.*)|*.*".Compose(IO.Path.GetExtension(initialFileName)),
            .FilterIndex = 1,
            .RestoreDirectory = True,
            .AddExtension = True,
            .OverwritePrompt = True,
            .InitialDirectory = folder,
            .FileName = file
        }
        If Not title = "" Then dialog.Title = title
        Do
            Select Case True
                Case Not dialog.ShowDialog() = Windows.Forms.DialogResult.OK : Return ""
                Case FileNotLocked(dialog.FileName) : Return dialog.FileName
                Case Else : MsgBox("SaveAsFileInUse".Translate, MsgBoxStyle.Exclamation)
            End Select
        Loop
    End Function

    ''' <summary>
    ''' Provides a <see cref="OpenFileDialog"/> to select multiple files.
    ''' </summary>
    ''' <param name="initialFolderName">The name of the initial folder.</param>
    ''' <param name="extension">The extension of files to select from.</param>
    ''' <param name="title">Optional title of the dialogbox.</param>
    ''' <returns>The list of selected files.</returns>
    Public Shared Function SelectFiles(initialFolderName As String, extension As String, Optional title As String = "") As String()
        Dim dialog = New OpenFileDialog With {
            .Filter = "{0} files (*{0})|*{0}|All files (*.*)|*.*".Compose(extension),
            .FilterIndex = 1,
            .RestoreDirectory = True,
            .AddExtension = True,
            .Multiselect = True,
            .InitialDirectory = initialFolderName
        }
        If Not title = "" Then dialog.Title = title
        Do
            Select Case dialog.ShowDialog() = Windows.Forms.DialogResult.OK
                Case True : Return dialog.FileNames
                Case Else : Return {}
            End Select
        Loop
    End Function

    ''' <summary>
    ''' Renames a file.
    ''' </summary>
    ''' <param name="fileName">The full name of the file.</param>
    ''' <param name="newFileName">The new full filename.</param>
    Public Shared Sub RenameFile(fileName As String, newFileName As String)
        Try
            If IO.File.Exists(fileName) Then IO.File.Move(fileName, newFileName)
        Catch ex As Exception
            MessageBoxInfo("{0}{2L}{1}".Compose(fileName, ex.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Deletes a file.
    ''' </summary>
    ''' <param name="fileName">The full name of the file.</param>
    Public Shared Sub DeleteFile(fileName As String)
        Try
            If IO.File.Exists(fileName) Then IO.File.Delete(fileName)
        Catch ex As Exception
            MessageBoxInfo("{0}{2L}{1}".Compose(fileName, ex.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Deletes all files in the folder.
    ''' </summary>
    ''' <param name="folderName">The full name of the folder.</param>
    Public Shared Sub DeleteAllFiles(folderName As String)
        For Each file In IO.Directory.GetFiles(folderName)
            DeleteFile(file)
        Next
    End Sub

    ''' <summary>
    ''' Determine if a file is not locked by another process.
    ''' </summary>
    ''' <param name="fileName">The full name of the file.</param>
    ''' <returns>True if file is not locked.</returns>
    Public Shared Function FileNotLocked(fileName As String) As Boolean
        Return Not FileLocked(fileName)
    End Function

    ''' <summary>
    ''' Determine if a file is locked by another process.
    ''' </summary>
    ''' <param name="fileName">The full name of the file.</param>
    ''' <returns>True if file is locked.</returns>
    Public Shared Function FileLocked(fileName As String) As Boolean
        Try
            If IO.File.Exists(fileName) Then
                Using fil = New IO.FileStream(fileName, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.None)
                End Using
            End If
            Return False
        Catch ex As Exception
            Return True
        End Try
    End Function

    ''' <summary>
    ''' Compares two files to be equal.
    ''' </summary>
    ''' <param name="firstFileName">The full name of the first file.</param>
    ''' <param name="secondFileName">The full name of the second file.</param>
    ''' <returns>True if files are actual equal.</returns>
    Public Shared Function FilesAreEqual(ByVal firstFileName As String, ByVal secondFileName As String) As Boolean
        If Not IO.File.Exists(firstFileName) Or Not IO.File.Exists(secondFileName) Then Return False
        If firstFileName = secondFileName Then Return True

        Using fileStream1 = New IO.FileStream(firstFileName, IO.FileMode.Open)
            Using fileStream2 = New IO.FileStream(secondFileName, IO.FileMode.Open)
                If Not fileStream1.Length = fileStream2.Length Then Return False

                Do
                    Select Case fileStream1.ReadByte
                        Case -1 : Return True
                        Case fileStream2.ReadByte
                        Case Else : Return False
                    End Select
                Loop
            End Using
        End Using
    End Function

    'folders

    ''' <summary>
    ''' Creates new folders.
    ''' </summary>
    ''' <param name="folderNames">List of full names of the folders.</param>
    Public Shared Sub CreateFolder(ParamArray folderNames() As String)
        For Each folder In folderNames
            If IO.Directory.Exists(folder) Then Continue For

            Try
                IO.Directory.CreateDirectory(folder)
            Catch ex As Exception
                MessageBoxInfo("{0}{2L}{1}".Compose(folder, ex.Message))
            End Try
        Next
    End Sub

    ''' <summary>
    ''' Determine if the user has write permission for the folder.
    ''' </summary>
    ''' <param name="folderName">The full name of the folder.</param>
    ''' <returns></returns>
    Public Shared Function FolderHasWritePermission(folderName As String) As Boolean
        Try
            IO.File.Create(folderName & "\writepermissiontest.txt").Close()
            IO.File.Delete(folderName & "\writepermissiontest.txt")
            Return True
        Catch ex As UnauthorizedAccessException
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Provides a <see cref="OpenFileDialog"/> to select a folder.
    ''' </summary>
    ''' <param name="initialFolderName">The full name of the initial folder.</param>
    ''' <returns></returns>
    Public Shared Function SelectFolder(initialFolderName As String) As String
        Dim dialog = New OpenFileDialog With {
            .InitialDirectory = initialFolderName,
            .ValidateNames = False,
            .CheckFileExists = False,
            .CheckPathExists = True,
            .Filter = "Folder Selection|",
            .Multiselect = False,
            .FileName = "Folder Selection"
        }
        If Not dialog.ShowDialog = Windows.Forms.DialogResult.OK Then Return ""

        Return IO.Path.GetDirectoryName(dialog.FileName)
    End Function

    'files/folders

    ''' <summary>
    ''' Gets a readable filesize (eg '10 MB').
    ''' </summary>
    ''' <param name="length">The length of the file in bytes.</param>
    ''' <param name="decimals">Number of decimals to use (eg 1, than 10.2 MB).</param>
    ''' <returns>The readable filesize.</returns>
    Public Shared Function ReadableFileSystemSize(length As Long, Optional decimals As Integer = 0) As String
        Dim divide = 1099511627776
        Dim unit = "TB"
        Select Case length
            Case Is < 1024 : divide = 1 : unit = "B"
            Case Is < 1048576 : divide = 1024 : unit = "kB"
            Case Is < 1073741824 : divide = 1048576 : unit = "MB"
            Case Is < 1099511627776 : divide = 1073741824 : unit = "GB"
        End Select
        Dim exponentiation = 10 ^ decimals
        Return "{0} {1}".Compose(Int((length * exponentiation / divide) + 1) / exponentiation, unit)
    End Function

    ''' <summary>
    ''' Gets a readable filesize (eg '10 MB'), which is left padding with spaces to be right aligned.
    ''' </summary>
    ''' <param name="length">The length of the file in bytes.</param>
    ''' <param name="decimals">Number of decimals to use (eg 1, than 10.2 MB).</param>
    ''' <param name="totalWidth">The total number of characters in the resulting string.</param>
    ''' <returns>The right aligned readable filesize.</returns>
    Public Shared Function ReadableFileSystemSizeRightAligned(length As Long, Optional decimals As Integer = 0, Optional totalWidth As Integer = 7)
        Return ReadableFileSystemSize(length, decimals).PadLeft(totalWidth)
    End Function

    ''' <summary>
    ''' Limits the length of the full name of the file.
    ''' </summary>
    ''' <param name="fileName">The full name of the file.</param>
    ''' <param name="maxLength">The maximum length of the resulting string.</param>
    ''' <returns>The shortened file name.</returns>
    Public Shared Function LimitDisplayLengthFileName(fileName As String, maxLength As Integer) As String
        If fileName.Length < maxLength Then Return fileName

        Dim prefix = "{0}...\".Compose(fileName.LeftString(InStr(fileName, "\")))
        Dim suffix = fileName.MidString(InStrRev(fileName, "\") + 1)
        Dim output As String
        Do
            output = "{0}{1}".Compose(prefix, suffix)
            suffix = fileName.MidString(InStrRev(fileName, "\", fileName.Length - (suffix.Length + 1)) + 1)
        Loop While prefix.Length + suffix.Length < maxLength
        Return output
    End Function

    ''' <summary>
    ''' Replaces all invalid characters for folders with underscores.
    ''' </summary>
    ''' <param name="folderName">The name of the folder.</param>
    ''' <returns></returns>
    Public Shared Function RemoveInvalidPathCharacters(folderName As String) As String
        Dim output = folderName.Trim.Replace("..", "_")
        IO.Path.GetInvalidPathChars.ToList.ForEach(Sub(c) output = output.Replace(c, "_"))
        Return output
    End Function

    ''' <summary>
    ''' Replaces all invalid characters for files with underscores.
    ''' </summary>
    ''' <param name="fileName">The name of the file.</param>
    ''' <returns></returns>
    Public Shared Function RemoveInvalidFileNameCharacters(fileName As String) As String
        Dim output = fileName.Trim.Replace("..", "_")
        IO.Path.GetInvalidFileNameChars.ToList.ForEach(Sub(c) output = output.Replace(c, "_"))
        Return output
    End Function

End Class
