'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module ToArrayExtensions

    ''' <summary>
    ''' Converts the collection to an array of <see cref="Control"/>.
    ''' </summary>
    ''' <param name="eControlCollection"></param>
    ''' <returns>The array of <see cref="Control"/>.</returns>
    <Extension()>
    Public Function ToArray(ByVal eControlCollection As Control.ControlCollection) As Control()
        Return eControlCollection.Cast(Of Control).ToArray
    End Function

    ''' <summary>
    ''' Converts the collection to an array of <see cref="DataRow"/>.
    ''' </summary>
    ''' <param name="eDataRowCollection"></param>
    ''' <returns>The array of <see cref="DataRow"/>.</returns>
    <Extension()>
    Public Function ToArray(ByVal eDataRowCollection As DataRowCollection) As DataRow()
        Return eDataRowCollection.Cast(Of DataRow).ToArray
    End Function

    ''' <summary>
    ''' Converts the collection to an array of <see cref="DataColumn"/>.
    ''' </summary>
    ''' <param name="eDataColumnCollection"></param>
    ''' <returns>The array of <see cref="DataColumn"/>.</returns>
    <Extension()>
    Public Function ToArray(ByVal eDataColumnCollection As DataColumnCollection) As DataColumn()
        Return eDataColumnCollection.Cast(Of DataColumn).ToArray
    End Function

    ''' <summary>
    ''' Converts the collection to an array of <see cref="DataGridViewRow"/>.
    ''' </summary>
    ''' <param name="eDataGridViewRowCollection"></param>
    ''' <returns>The array of <see cref="DataGridViewRow"/>.</returns>
    <Extension()>
    Public Function ToArray(ByVal eDataGridViewRowCollection As DataGridViewRowCollection) As DataGridViewRow()
        Return eDataGridViewRowCollection.Cast(Of DataGridViewRow).ToArray
    End Function

    ''' <summary>
    ''' Converts the collection to an array of <see cref="DataGridViewRow"/>.
    ''' </summary>
    ''' <param name="eDataGridViewSelectedRowCollection"></param>
    ''' <returns>The array of <see cref="DataGridViewRow"/>.</returns>
    <Extension()>
    Public Function ToArray(ByVal eDataGridViewSelectedRowCollection As DataGridViewSelectedRowCollection) As DataGridViewRow()
        Return eDataGridViewSelectedRowCollection.Cast(Of DataGridViewRow).ToArray
    End Function

    ''' <summary>
    ''' Converts the collection to an array of <see cref="DataGridViewColumn"/>.
    ''' </summary>
    ''' <param name="eDataGridViewColumnCollection"></param>
    ''' <returns>The array of <see cref="DataGridViewColumn"/>.</returns>
    <Extension()>
    Public Function ToArray(ByVal eDataGridViewColumnCollection As DataGridViewColumnCollection) As DataGridViewColumn()
        Return eDataGridViewColumnCollection.Cast(Of DataGridViewColumn).ToArray
    End Function

    ''' <summary>
    ''' Converts the collection to an array of <see cref="DataGridViewCell"/>.
    ''' </summary>
    ''' <param name="eDataGridViewCellCollection"></param>
    ''' <returns>The array of <see cref="DataGridViewCell"/>.</returns>
    <Extension()>
    Public Function ToArray(ByVal eDataGridViewCellCollection As DataGridViewCellCollection) As DataGridViewCell()
        Return eDataGridViewCellCollection.Cast(Of DataGridViewCell).ToArray
    End Function

End Module
