'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Provides methodes for reading and writing textfiles.
''' </summary>
Public Class TextFileHelper

    ''' <summary>
    ''' Writes the lines of texts into a new text file.
    ''' </summary>
    ''' <param name="fileName">The fullname of the file.</param>
    ''' <param name="lines">The lines of texts.</param>
    Public Shared Sub Write(fileName As String, lines As String())
        Try
            IO.File.WriteAllLines(fileName, lines)
        Catch ex As Exception
            MessageBoxInfo("{0}{2L}{1}".Compose(fileName, ex.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Appends the line of text into the specified text file.
    ''' </summary>
    ''' <param name="fileName">The fullname of the file.</param>
    ''' <param name="line">The line of text.</param>
    Public Shared Sub Append(fileName As String, line As String)
        Try
            IO.File.AppendAllLines(fileName, {line})
        Catch ex As Exception
            MessageBoxInfo("{0}{2L}{1}".Compose(fileName, ex.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Appends the lines of texts into the specified text file.
    ''' </summary>
    ''' <param name="fileName">The fullname of the file.</param>
    ''' <param name="lines">The lines of texts.</param>
    Public Shared Sub Append(fileName As String, lines As String())
        Try
            IO.File.AppendAllLines(fileName, lines)
        Catch ex As Exception
            MessageBoxInfo("{0}{2L}{1}".Compose(fileName, ex.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Inserts the line of text into the specified text file.
    ''' </summary>
    ''' <param name="fileName">The fullname of the file.</param>
    ''' <param name="line">The line of text.</param>
    Public Shared Sub Insert(fileName As String, line As String)
        Try
            Dim existingLog As New List(Of String)
            If IO.File.Exists(fileName) Then existingLog.AddRange(IO.File.ReadAllLines(fileName))
            IO.File.WriteAllLines(fileName, {line})
            If existingLog.Count > 0 Then IO.File.AppendAllLines(fileName, existingLog)
        Catch ex As Exception
            MessageBoxInfo("{0}{2L}{1}".Compose(fileName, ex.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Inserts the lines of texts into the specified text file.
    ''' </summary>
    ''' <param name="fileName">The fullname of the file.</param>
    ''' <param name="lines">The lines of texts.</param>
    Public Shared Sub Insert(fileName As String, lines As String())
        Try
            Dim existingLog As New List(Of String)
            If IO.File.Exists(fileName) Then existingLog.AddRange(IO.File.ReadAllLines(fileName))
            IO.File.WriteAllLines(fileName, lines)
            If existingLog.Count > 0 Then IO.File.AppendAllLines(fileName, existingLog)
        Catch ex As Exception
            MessageBoxInfo("{0}{2L}{1}".Compose(fileName, ex.Message))
        End Try
    End Sub

    ''' <summary>
    ''' Reads the lines of texts in the specified text file into lines of texts.
    ''' </summary>
    ''' <param name="fileName">The fullname of the file.</param>
    ''' <returns>The lines of texts.</returns>
    Public Shared Function Read(fileName As String) As String()
        Try
            Return IO.File.ReadAllLines(fileName)
        Catch ex As Exception
            MessageBoxInfo("{0}{2L}{1}".Compose(fileName, ex.Message))
            Return {}
        End Try
    End Function

End Class
