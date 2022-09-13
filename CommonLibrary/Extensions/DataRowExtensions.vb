'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module DataRowExtensions

    'subs

    ''' <summary>
    ''' Stores a text in the field of the record.
    ''' </summary>
    ''' <param name="eDataRow"></param>
    ''' <param name="columnName">The name of the field.</param>
    ''' <param name="text">Text to store.</param>
    <Extension()>
    Public Sub SetString(ByRef eDataRow As DataRow, columnName As String, text As String)
        Select Case eDataRow.Table.Columns.Contains(columnName)
            Case True : eDataRow(columnName) = text
            Case Else : eDataRow.Table.Columns.Add(columnName, GetType(String)) : eDataRow(columnName) = text
        End Select
    End Sub

    'functions

    ''' <summary>
    ''' Gets the fieldnames of the record.
    ''' </summary>
    ''' <param name="eDataRow"></param>
    ''' <returns>List of fieldnames.</returns>
    <Extension()>
    Public Function GetColumnNames(ByVal eDataRow As DataRow) As String()
        Return eDataRow.Table.GetColumnNames
    End Function

    ''' <summary>
    ''' Gets the text stored in the field.
    ''' </summary>
    ''' <param name="eDataRow"></param>
    ''' <param name="columnName">The name of the field.</param>
    ''' <returns>Stored text.</returns>
    <Extension()>
    Public Function GetString(ByVal eDataRow As DataRow, columnName As String) As String
        Select Case True
            Case Not eDataRow.Table.Columns.Contains(columnName) : Return ""
            Case IsDBNull(eDataRow(columnName)) : Return ""
            Case Else : Return eDataRow(columnName)
        End Select
    End Function

    ''' <summary>
    ''' Gets the numeric value of the expression (text or otherwise) stored in the field.
    ''' </summary>
    ''' <param name="eDataRow"></param>
    ''' <param name="columnName">The name of the field.</param>
    ''' <returns>Stored nummeric value.</returns>
    <Extension()>
    Public Function GetDouble(ByVal eDataRow As DataRow, columnName As String) As Double
        Return Val(eDataRow.GetValue(columnName))
    End Function

    ''' <summary>
    ''' Determine if the field has something stored in it.
    ''' </summary>
    ''' <param name="eDataRow"></param>
    ''' <param name="columnName">The name of the field.</param>
    ''' <returns>True if is not nothing.</returns>
    <Extension()>
    Public Function HasValue(ByVal eDataRow As DataRow, columnName As String) As Boolean
        Select Case True
            Case Not eDataRow.Table.Columns.Contains(columnName) : Return False
            Case IsDBNull(eDataRow(columnName)) : Return False
            Case Else : Return True
        End Select
    End Function

    ''' <summary>
    ''' Gets the (late bound) value stored in the field.
    ''' </summary>
    ''' <param name="eDataRow"></param>
    ''' <param name="columnName">The name of the field.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetValue(ByVal eDataRow As DataRow, columnName As String) As Object
        Select Case True
            Case Not eDataRow.Table.Columns.Contains(columnName) : Return Nothing
            Case IsDBNull(eDataRow(columnName)) : Return Nothing
            Case Else : Return eDataRow(columnName)
        End Select
    End Function

End Module
