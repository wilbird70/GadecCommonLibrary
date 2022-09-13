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
    ''' <param name="columnDataArray">Stringarray with stringexpressions for the fielddata eg 'FirstName=String'.</param>
    ''' <returns>The database.</returns>
    Public Shared Function Create(tableName As String, ParamArray columnDataArray As String()) As DataTable
        Dim columnData = columnDataArray.ToIniDictionary
        Dim output = New DataTable(tableName)
        For Each pair In columnData
            Dim columnType = If(pair.Value = "", GetType(String), Type.GetType(pair.Value, False))
            output.Columns.Add(New DataColumn(pair.Key, columnType, "", MappingType.Attribute))
        Next
        Return output
    End Function

End Class
