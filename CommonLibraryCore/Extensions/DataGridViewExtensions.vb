'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module DataGridViewExtensions

    ''' <summary>
    ''' Gets the record where the specified field contains the specified value.
    ''' </summary>
    ''' <param name="eDataGridView"></param>
    ''' <param name="columnName">The name of the field.</param>
    ''' <param name="value">The value to search for.</param>
    ''' <returns>The found record.</returns>
    <Extension()>
    Public Function GetRowByValue(ByVal eDataGridView As DataGridView, columnName As String, value As String) As DataGridViewRow
        For Each gridRow In eDataGridView.Rows.ToArray
            If gridRow.Cells(columnName).Value = value Then Return gridRow
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Gets all texts from the specified field.
    ''' </summary>
    ''' <param name="eDataGridView"></param>
    ''' <param name="columnName">The name of the field.</param>
    ''' <returns>List of texts.</returns>
    <Extension()>
    Public Function GetStringsFromColumn(ByVal eDataGridView As DataGridView, columnName As String) As String()
        If IsNothing(eDataGridView) Then Return {}

        If Not eDataGridView.Columns.Contains(columnName) OrElse Not eDataGridView.Columns(columnName).ValueType = GetType(String) Then Return {}

        Return eDataGridView.Rows.Cast(Of DataGridViewRow).Select(Function(x) x.GetString(columnName)).ToArray
    End Function

    ''' <summary>
    ''' Sets the widths and order of the fields.
    ''' </summary>
    ''' <param name="eDataGridView"></param>
    ''' <param name="columnWidths">Dictionary with the fieldnames (keys) and widths to set (values).</param>
    <Extension()>
    Public Sub SetColumnWidths(ByRef eDataGridView As DataGridView, columnWidths As Dictionary(Of String, String))
        Dim font = FontHelper.ArialBold
        Dim keys = columnWidths.Keys
        For Each dataGridViewColumn In eDataGridView.Columns.ToArray
            With dataGridViewColumn
                If Not keys.Contains(.Name) OrElse columnWidths(.Name) = "0" Then .Visible = False : Continue For

                .Visible = True
                .Width = 10 'highlight werkte niet op laatste kolom. hierdoor wel...
                Select Case True
                    Case columnWidths(.Name).EndsWith("%")
                        .Width = Val(columnWidths(.Name).EraseEnd(1)) * (eDataGridView.Width / 100)
                    Case columnWidths(.Name).StartsWith("*")
                        .Width = Val(columnWidths(.Name).EraseStart(1))
                        .DefaultCellStyle.Font = font
                        .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                    Case Else
                        .Width = Val(columnWidths(.Name))
                End Select
                .HeaderText = .Name.Translate
                .SortMode = DataGridViewColumnSortMode.NotSortable
            End With
        Next
        Dim index = 0
        For Each columnName In columnWidths.Keys
            If Not eDataGridView.Columns.Contains(columnName) Then Continue For

            Dim dataGridViewColumn = eDataGridView.Columns.Item(columnName)
            dataGridViewColumn.DisplayIndex = index
            index += 1
        Next
    End Sub

    ''' <summary>
    ''' Gets the string from the field of the record.
    ''' </summary>
    ''' <param name="eDataGridViewRow"></param>
    ''' <param name="columnName">The name of the field.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function GetString(ByVal eDataGridViewRow As DataGridViewRow, columnName As String) As String
        Select Case True
            Case Not eDataGridViewRow.DataGridView.Columns.Contains(columnName) : Return ""
            Case IsDBNull(eDataGridViewRow.Cells(columnName).Value) : Return ""
            Case Else : Return eDataGridViewRow.Cells(columnName).Value
        End Select
    End Function

    ''' <summary>
    ''' Sets or resets the highlight of a record.
    ''' </summary>
    ''' <param name="eDataGridViewRow"></param>
    ''' <param name="value">When True, the record will be highlighted.</param>
    <Extension()>
    Public Sub HighlightRow(ByRef eDataGridViewRow As DataGridViewRow, value As Boolean)
        Dim cellStyle = If(value, New DataGridViewCellStyle With {.BackColor = colorGradient(eDataGridViewRow.DefaultCellStyle.BackColor, Color.Black, 0.15)}, Nothing)
        For Each dataGridViewCell In eDataGridViewRow.Cells.ToArray
            If Not dataGridViewCell.Visible Then Continue For

            dataGridViewCell.Style = cellStyle
        Next
    End Sub

    'private functions

    ''' <summary>
    ''' Gets the gradient color of the source color with the target color for a percentage amount.
    ''' </summary>
    ''' <param name="sourceColor">Source color</param>
    ''' <param name="targetColor">Target color</param>
    ''' <param name="amount">Percentage amount</param>
    ''' <returns>The gradient color.</returns>
    Private Function colorGradient(sourceColor As Color, targetColor As Color, amount As Double) As Color
        Dim r = byteGradient(sourceColor.R, targetColor.R, amount)
        Dim g = byteGradient(sourceColor.G, targetColor.G, amount)
        Dim b = byteGradient(sourceColor.B, targetColor.B, amount)
        Return Color.FromArgb(r, g, b)
    End Function

    ''' <summary>
    ''' Gets the gradient byte of the source byte with the target byte for a percentage amount.
    ''' </summary>
    ''' <param name="sourceByte">Source byte</param>
    ''' <param name="targetByte">Target byte</param>
    ''' <param name="amount">Percentage amount</param>
    ''' <returns></returns>
    Private Function byteGradient(sourceByte As Byte, ByVal targetByte As Byte, amount As Double)
        Return sourceByte + (CDbl(targetByte) - CDbl(sourceByte)) * amount
    End Function

End Module
