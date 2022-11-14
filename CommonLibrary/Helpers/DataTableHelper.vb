'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Provides methodes for databases.
''' </summary>
Public Class DataTableHelper

    ''' <summary>
    ''' Creates a new database and adds fields to it.
    ''' </summary>
    ''' <param name="tableName">The name of the database.</param>
    ''' <param name="columnNames">List of names for the fields.</param>
    ''' <returns>The database.</returns>
    Public Shared Function Create(tableName As String, ParamArray columnNames As String()) As DataTable
        Dim output = New DataTable(tableName)
        For Each name In columnNames
            output.Columns.Add(New DataColumn(name, GetType(String), "", MappingType.Attribute))
        Next
        Return output
    End Function

End Class
