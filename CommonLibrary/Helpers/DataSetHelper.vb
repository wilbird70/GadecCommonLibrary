'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Provides methodes for database collections.
''' </summary>
Public Class DataSetHelper

    ''' <summary>
    ''' Loads a database collection from the file.
    ''' </summary>
    ''' <param name="file">The full filename of the xml-file.</param>
    ''' <returns>The database collection.</returns>
    Public Shared Function LoadFromXml(file As String) As DataSet
        If Not IO.File.Exists(file) Then Return New DataSet

        Try
            Dim output = New DataSet
            output.ReadXml(file)
            Return output
        Catch ex As Exception
            ex.AddData($"FileName: {file}")
            ex.Rethrow
            Return Nothing
        End Try
    End Function

End Class
