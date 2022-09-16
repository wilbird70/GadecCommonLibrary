'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module DataTableExtensions

    ''' <summary>
    ''' Gets all the names of the fields in the database.
    ''' </summary>
    ''' <param name="eDataTable"></param>
    ''' <returns>List of fieldnames</returns>
    <Extension()>
    Public Function GetColumnNames(ByVal eDataTable As DataTable) As String()
        If IsNothing(eDataTable) Then Return {}

        Return eDataTable.Columns.ToList.Select(Function(x) x.ColumnName).ToArray
    End Function

    ''' <summary>
    ''' Gets another database from the same collection.
    ''' </summary>
    ''' <param name="eDataTable"></param>
    ''' <param name="tableName">The name of the database.</param>
    ''' <returns>The database.</returns>
    <Extension()>
    Public Function GetTable(ByVal eDataTable As DataTable, tableName As String) As DataTable
        If IsNothing(eDataTable) Then Return Nothing

        If IsNothing(eDataTable.DataSet) OrElse Not eDataTable.DataSet.Tables.Contains(tableName) Then Return New DataTable(tableName)

        Return eDataTable.DataSet.Tables(tableName)
    End Function

    ''' <summary>
    ''' Indexes the database.
    ''' </summary>
    ''' <param name="eDataTable"></param>
    ''' <param name="primaryKeyString">String with fieldnames, delimited with semicolons.</param>
    <Extension()>
    Public Sub AssignPrimaryKey(ByVal eDataTable As DataTable, primaryKeyString As String)
        If IsNothing(eDataTable) Then Exit Sub

        Dim primaryColumns = New List(Of DataColumn)
        For Each key In primaryKeyString.Cut
            Select Case True
                Case key = ""
                Case eDataTable.Columns.Contains(key) : primaryColumns.Add(eDataTable.Columns(key))
                Case Else : MessageBoxInfo("PrimaryKeyNotValid".Translate(key))
            End Select
        Next
        If primaryColumns.Count > 0 Then eDataTable.PrimaryKey = primaryColumns.ToArray
    End Sub

    ''' <summary>
    ''' Sorts the database on the fields in the sortstring.
    ''' </summary>
    ''' <param name="eDataTable"></param>
    ''' <param name="sortString">String with fieldnames, delimited with semicolons.</param>
    <Extension()>
    Public Sub AssignDefaultViewSort(ByRef eDataTable As DataTable, sortString As String)
        If IsNothing(eDataTable) Then Exit Sub

        Dim sortColumns = New List(Of String)
        For Each key In sortString.Cut
            Select Case True
                Case key = ""
                Case eDataTable.Columns.Contains(key) : sortColumns.Add(key)
                Case Else : MessageBoxInfo("ColumnNotValid".Translate(key))
            End Select
        Next
        If sortColumns.Count > 0 Then eDataTable.DefaultView.Sort = String.Join(",", sortColumns)
    End Sub

    ''' <summary>
    ''' Inserts fields to the database.
    ''' </summary>
    ''' <param name="eDataTable"></param>
    ''' <param name="columnDataArray">Stringarray with stringexpressions for the fielddata eg 'FirstName=String'.</param>
    <Extension()>
    Public Sub InsertColumns(ByRef eDataTable As DataTable, ParamArray columnDataArray As String())
        If IsNothing(eDataTable) Then Exit Sub

        Dim columnData = columnDataArray.ToIniDictionary
        For Each pair In columnData
            Dim columnType = If(pair.Value = "", GetType(String), Type.GetType(pair.Value, False))
            eDataTable.InsertColumn(pair.Key, columnType)
        Next
    End Sub

    ''' <summary>
    ''' Inserts a field to the database.
    ''' </summary>
    ''' <param name="eDataTable"></param>
    ''' <param name="columnName">The name of the field.</param>
    ''' <param name="columnType">The type of data to store in the field.</param>
    ''' <param name="Ordinal">The orderplace for the field.</param>
    <Extension()>
    Public Sub InsertColumn(ByRef eDataTable As DataTable, columnName As String, columnType As Type, Optional Ordinal As Integer = -1)
        If IsNothing(eDataTable) OrElse eDataTable.Columns.Contains(columnName) Then Exit Sub

        Dim dataColumn = New DataColumn(columnName, columnType, "", MappingType.Attribute)
        eDataTable.Columns.Add(dataColumn)
        If Ordinal < 0 Then Exit Sub

        If Ordinal < eDataTable.Columns.Count Then dataColumn.SetOrdinal(Ordinal)
    End Sub

    ''' <summary>
    ''' Sets all the fields to mappingtype attribute.
    ''' </summary>
    ''' <param name="eDataTable"></param>
    <Extension()>
    Public Sub MapColumnsToAttribute(ByRef eDataTable As DataTable)
        If IsNothing(eDataTable) Then Exit Sub

        For Each column In eDataTable.Columns.ToArray
            column.ColumnMapping = MappingType.Attribute
        Next
    End Sub

    ''' <summary>
    ''' Gets values (late bound) from the field of all records.
    ''' </summary>
    ''' <param name="eDataTable"></param>
    ''' <param name="columnName"></param>
    ''' <returns>List of values (late bound).</returns>
    <Extension()>
    Public Function GetValuesFromColumn(ByVal eDataTable As DataTable, columnName As String) As Object()
        If IsNothing(eDataTable) OrElse Not eDataTable.Columns.Contains(columnName) Then Return {}

        Return eDataTable.AsEnumerable().Select(Function(x) x(columnName)).Cast(Of Object)().ToArray
    End Function

    ''' <summary>
    ''' Gets texts from the field of all records.
    ''' </summary>
    ''' <param name="eDataTable"></param>
    ''' <param name="columnName"></param>
    ''' <returns>List of texts.</returns>
    <Extension()>
    Public Function GetStringsFromColumn(ByVal eDataTable As DataTable, columnName As String) As String()
        If IsNothing(eDataTable) Then Return {}

        If Not eDataTable.Columns.Contains(columnName) OrElse Not eDataTable.Columns(columnName).DataType = GetType(String) Then Return {}

        Return eDataTable.Rows.Cast(Of DataRow).Select(Function(x) x.GetString(columnName)).ToArray
    End Function

    ''' <summary>
    ''' Gets unique texts from the field of all records.
    ''' </summary>
    ''' <param name="eDataTable"></param>
    ''' <param name="columnName">The name of the field.</param>
    ''' <returns>List of unique texts.</returns>
    <Extension()>
    Public Function GetUniqueStringsFromColumn(ByVal eDataTable As DataTable, columnName As String) As String()
        If IsNothing(eDataTable) Then Return {}

        If Not eDataTable.Columns.Contains(columnName) OrElse Not eDataTable.Columns(columnName).DataType = GetType(String) Then Return {}

        Return eDataTable.DefaultView.ToTable(True, columnName).GetStringsFromColumn(columnName)
    End Function

End Module
