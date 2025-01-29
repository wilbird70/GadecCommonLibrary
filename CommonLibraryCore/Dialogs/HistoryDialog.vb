'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' <see cref="HistoryDialog"/> is a dialog showing any available app history.
''' </summary>
Friend Class HistoryDialog
    Private ReadOnly _versionData As DataTable
    Private ReadOnly _versionKeys As List(Of String)

    'form

    ''' <summary>
    ''' Initializes a new instance the <see cref="HistoryDialog"/> with the specified version data.
    ''' <para><see cref="HistoryDialog"/> is a dialog showing any available app history.</para>
    ''' </summary>
    ''' <param name="versionData">A database with the version history.</param>
    Sub New(versionData As DataTable)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        Me.Text = Registerizer.GetApplicationVersion()
        Me.Controls.ToList.ForEach(Sub(c) If c.Name.StartsWith("lt") Then c.Text = c.Name.Translate)

        _versionData = versionData
        _versionKeys = versionData.GetStringsFromColumn("Name").ToList
        _versionKeys.Sort()
        HorizontalScrollBar.Maximum = _versionKeys.Count - 1
        HorizontalScrollBar.Value = HorizontalScrollBar.Maximum
        ShowVersionChange(HorizontalScrollBar.Value)
    End Sub

    'buttons

    ''' <summary>
    ''' EventHandler for the event that occurs when user clicks the next button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub NextButton_Click(sender As Object, e As EventArgs) Handles ltNext.Click
        If HorizontalScrollBar.Value > HorizontalScrollBar.Maximum - 1 Then Exit Sub

        HorizontalScrollBar.Value += 1
        ShowVersionChange(HorizontalScrollBar.Value)
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when user clicks the previous button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PreviousButton_Click(sender As Object, e As EventArgs) Handles ltPrevious.Click
        If HorizontalScrollBar.Value < 1 Then Exit Sub

        HorizontalScrollBar.Value -= 1
        ShowVersionChange(HorizontalScrollBar.Value)
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when user clicks the close button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles ltClose.Click
        Me.Hide()
    End Sub

    'scrollbars

    ''' <summary>
    ''' EventHandler for the event that occurs when user slides the scrollbar.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub HorizontalScrollBar_Scroll(sender As Object, e As ScrollEventArgs) Handles HorizontalScrollBar.Scroll
        ShowVersionChange(e.NewValue)
    End Sub

    'private subs

    ''' <summary>
    ''' EventHandler for the event the notes of changes by the version with specified index.
    ''' </summary>
    ''' <param name="index">The index number.</param>
    Private Sub ShowVersionChange(index As Integer)
        Dim version = _versionKeys(index)
        Dim versionRow = _versionData.Rows.Find(version)
        Dim logRows = _versionData.GetTable("Logs").Select("Name='{0}'".Compose(version), "Row")
        If IsNothing(versionRow) Or IsNothing(logRows) Then OutputTextBox.Text = "" : Exit Sub

        Dim lines = {"{0} ({1}){CL}".Compose(versionRow.GetTranslation, versionRow.GetString("Date"))}.ToList
        logRows.ToList.ForEach(Sub(row) lines.Add("● {0}".Compose(row.GetTranslation)))
        OutputTextBox.Text = String.Join(vbCrLf, lines)
        OutputTextBox.Select()
        OutputTextBox.SelectionStart = 0
        OutputTextBox.SelectionLength = 0
        ltNext.Enabled = Not HorizontalScrollBar.Value = HorizontalScrollBar.Maximum
        ltPrevious.Enabled = Not HorizontalScrollBar.Value = HorizontalScrollBar.Minimum
    End Sub

End Class