'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module DataSetExtensions

    ''' <summary>
    ''' Gets all the names of databases in the collection.
    ''' </summary>
    ''' <param name="eDataSet"></param>
    ''' <returns>List of database-names.</returns>
    <Extension()>
    Public Function GetTableNames(ByVal eDataSet As DataSet) As String()
        If IsNothing(eDataSet) Then Return {}

        Dim output = (From table As DataTable In eDataSet.Tables Select table.TableName).ToList
        output.Sort()
        Return output.ToArray
    End Function

    ''' <summary>
    ''' Gets the database from the collection. Optionally the database will be indexed.
    ''' </summary>
    ''' <param name="eDataSet"></param>
    ''' <param name="tableName">The name of the database.</param>
    ''' <param name="primaryKeyString">String with fieldnames, delimited with semicolons.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetTable(ByVal eDataSet As DataSet, tableName As String, Optional primaryKeyString As String = "") As DataTable
        If IsNothing(eDataSet) OrElse Not eDataSet.Tables.Contains(tableName) Then Return New DataTable(tableName)

        If primaryKeyString = "" Then Return eDataSet.Tables(tableName)

        Dim output = eDataSet.Tables(tableName)
        output.AssignPrimaryKey(primaryKeyString)
        Return output
    End Function

    ''' <summary>
    ''' Inserts the database into the collection.
    ''' </summary>
    ''' <param name="eDataSet"></param>
    ''' <param name="dataTable">The database to insert.</param>
    <Extension()>
    Public Sub AddOrReplaceDataTable(ByRef eDataSet As DataSet, dataTable As DataTable)
        If IsNothing(eDataSet) Or IsNothing(dataTable) Then Exit Sub

        If NotNothing(dataTable.DataSet) Then dataTable.DataSet.Tables.Remove(dataTable)
        If eDataSet.Tables.Contains(dataTable.TableName) Then eDataSet.Tables.Remove(dataTable.TableName)
        eDataSet.Tables.Add(dataTable)
    End Sub

End Module
