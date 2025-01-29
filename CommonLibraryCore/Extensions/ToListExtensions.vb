'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module ToListExtensions

    ''' <summary>
    ''' Converts the collection to a list of <see cref="Control"/>.
    ''' </summary>
    ''' <param name="eControlCollection"></param>
    ''' <returns>The list of <see cref="Control"/>.</returns>
    <Extension()>
    Public Function ToList(ByVal eControlCollection As Control.ControlCollection) As List(Of Control)
        Return eControlCollection.Cast(Of Control).ToList
    End Function

    ''' <summary>
    ''' Converts the collection to a list of <see cref="DataRow"/>.
    ''' </summary>
    ''' <param name="eDataRowCollection"></param>
    ''' <returns>The list of <see cref="DataRow"/>.</returns>
    <Extension()>
    Public Function ToList(ByVal eDataRowCollection As DataRowCollection) As List(Of DataRow)
        Return eDataRowCollection.Cast(Of DataRow).ToList
    End Function

    ''' <summary>
    ''' Converts the collection to a list of <see cref="DataColumn"/>.
    ''' </summary>
    ''' <param name="eDataColumnCollection"></param>
    ''' <returns>The list of <see cref="DataColumn"/>.</returns>
    <Extension()>
    Public Function ToList(ByVal eDataColumnCollection As DataColumnCollection) As List(Of DataColumn)
        Return eDataColumnCollection.Cast(Of DataColumn).ToList
    End Function

    ''' <summary>
    ''' Converts the collection to a list of <see cref="DataGridViewRow"/>.
    ''' </summary>
    ''' <param name="eDataGridViewRowCollection"></param>
    ''' <returns>The list of <see cref="DataGridViewRow"/>.</returns>
    <Extension()>
    Public Function ToList(ByVal eDataGridViewRowCollection As DataGridViewRowCollection) As List(Of DataGridViewRow)
        Return eDataGridViewRowCollection.Cast(Of DataGridViewRow).ToList
    End Function

    ''' <summary>
    ''' Converts the collection to a list of <see cref="DataGridViewRow"/>.
    ''' </summary>
    ''' <param name="eDataGridViewSelectedRowCollection"></param>
    ''' <returns>The list of <see cref="DataGridViewRow"/>.</returns>
    <Extension()>
    Public Function ToList(ByVal eDataGridViewSelectedRowCollection As DataGridViewSelectedRowCollection) As List(Of DataGridViewRow)
        Return eDataGridViewSelectedRowCollection.Cast(Of DataGridViewRow).ToList
    End Function

    ''' <summary>
    ''' Converts the collection to a list of <see cref="DataGridViewColumn"/>.
    ''' </summary>
    ''' <param name="eDataGridViewColumnCollection"></param>
    ''' <returns>The list of <see cref="DataGridViewColumn"/>.</returns>
    <Extension()>
    Public Function ToList(ByVal eDataGridViewColumnCollection As DataGridViewColumnCollection) As List(Of DataGridViewColumn)
        Return eDataGridViewColumnCollection.Cast(Of DataGridViewColumn).ToList
    End Function

    ''' <summary>
    ''' Converts the collection to a list of <see cref="DataGridViewCell"/>.
    ''' </summary>
    ''' <param name="eDataGridViewCellCollection"></param>
    ''' <returns>The list of <see cref="DataGridViewCell"/>.</returns>
    <Extension()>
    Public Function ToList(ByVal eDataGridViewCellCollection As DataGridViewCellCollection) As List(Of DataGridViewCell)
        Return eDataGridViewCellCollection.Cast(Of DataGridViewCell).ToList
    End Function

End Module
