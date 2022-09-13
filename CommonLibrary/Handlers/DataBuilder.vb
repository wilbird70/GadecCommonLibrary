'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' <para>Databuilder can create a database on-the-fly.</para>
''' </summary>
Public Class DataBuilder
    Implements IDisposable
    ''' <summary>
    ''' Database to create on-the-fly.
    ''' </summary>
    Private ReadOnly _data As DataTable
    ''' <summary>
    ''' Current record to store the new values.
    ''' </summary>
    Private _currentRow As DataRow = Nothing
    ''' <summary>
    ''' Status of disposing.
    ''' </summary>
    Private _disposedValue As Boolean

    ''' <summary>
    ''' Initializes a new instance of <see cref="DataBuilder"/> with the specified database name and any fields.
    ''' <para><see cref="DataBuilder"/> can create a database on-the-fly.</para>
    ''' </summary>
    ''' <param name="tableName">The name to give the table.</param>
    ''' <param name="initialColumnDataArray">Stringarray with stringexpressions for the initial fielddata eg 'FirstName=String'.</param>
    Public Sub New(tableName As String, ParamArray initialColumnDataArray As String())
        _data = DataTableHelper.Create(tableName, initialColumnDataArray)
    End Sub

    ''' <summary>
    ''' Disposes the datatable.
    ''' </summary>
    Protected Overridable Sub Dispose(disposing As Boolean)
        If _disposedValue Then Exit Sub

        If disposing Then DirectCast(_data, IDisposable).Dispose()
        _disposedValue = True
    End Sub

    ''' <summary>
    ''' Implements the dispose method.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub

    'subs

    ''' <summary>
    ''' Insert a new field to the database.
    ''' </summary>
    ''' <param name="columnName">The name to give the field.</param>
    ''' <param name="columnType">Type of the new field.</param>
    ''' <param name="Ordinal">Orderplace of the new field.</param>
    Public Sub InsertColumn(columnName As String, columnType As Type, Optional Ordinal As Integer = -1)
        If _data.Columns.Contains(columnName) Then Exit Sub

        Dim dataColumn = New DataColumn(columnName, columnType, "", MappingType.Attribute)
        _data.Columns.Add(dataColumn)
        If Ordinal < 0 Then Exit Sub

        If Ordinal < _data.Columns.Count Then dataColumn.SetOrdinal(Ordinal)
    End Sub

    ''' <summary>
    ''' Appends a field to the current record and stores the value in it.
    ''' </summary>
    ''' <param name="columnName">The name of the field.</param>
    ''' <param name="value">The value to store.</param>
    Public Sub AppendValue(columnName As String, value As Object)
        If IsNothing(_currentRow) Then _currentRow = _data.NewRow
        If Not _data.Columns.Contains(columnName) Then _data.Columns.Add(New DataColumn(columnName, value.GetType, "", MappingType.Attribute))
        _currentRow(columnName) = value
    End Sub

    ''' <summary>
    ''' Changes the value of a field in the current record, only if there is already a value.
    ''' </summary>
    ''' <param name="columnName">The name of the field.</param>
    ''' <param name="value">The new value to store.</param>
    Public Sub ChangeValue(columnName As String, value As Object)
        If _currentRow?.HasValue(columnName) Then _currentRow(columnName) = value
    End Sub

    ''' <summary>
    ''' Adds the current record to the database. New values will stored in a new record.
    ''' </summary>
    Public Sub AddNewlyCreatedRow()
        If NotNothing(_currentRow) Then _data.Rows.Add(_currentRow)
        _currentRow = Nothing
    End Sub

    ''' <summary>
    ''' Disposes the current record. New values will stored in a new record.
    ''' </summary>
    Public Sub RejectRow()
        _currentRow = Nothing
    End Sub

    'functions

    ''' <summary>
    ''' Gets the string, stored in a field.
    ''' </summary>
    ''' <param name="columnName">The name of the field.</param>
    ''' <returns>Stored string.</returns>
    Public Function GetString(columnName As String) As String
        If IsNothing(_currentRow) OrElse Not _currentRow.HasValue(columnName) Then Return ""

        Return _currentRow(columnName)
    End Function

    ''' <summary>
    ''' Gets the value, stored in a field.
    ''' </summary>
    ''' <param name="columnName">The name of the field.</param>
    ''' <returns>Stored value.</returns>
    Public Function GetValue(columnName As String) As Object
        If IsNothing(_currentRow) OrElse Not _currentRow.HasValue(columnName) Then Return Nothing

        Return _currentRow(columnName)
    End Function

    ''' <summary>
    ''' Gets the database from the DataBuilder.
    ''' </summary>
    ''' <param name="primaryKeyString">Optional: Set a primarykey.</param>
    ''' <param name="sortString">Optional: Set a sortstring.</param>
    ''' <returns>The on-the-fly created database.</returns>
    Public Function GetDataTable(Optional primaryKeyString As String = "", Optional sortString As String = "") As DataTable
        If Not primaryKeyString = "" Then _data.AssignPrimaryKey(primaryKeyString)
        If Not sortString = "" Then _data.AssignDefaultViewSort(sortString)
        Return _data
    End Function

End Class
