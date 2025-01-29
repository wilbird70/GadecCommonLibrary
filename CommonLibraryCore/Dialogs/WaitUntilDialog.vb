'Gadec Engineerings Software (c) 2022

''' <summary>
''' <para><see cref="WaitUntilDialog"/> provides a dialog to wait for a criterion to occur.</para>
''' </summary>
Public Class WaitUntilDialog
    ''' <summary>
    ''' Determines whether the criterion has become true.
    ''' </summary>
    Public ReadOnly Property Successfully As Boolean = False

    Private ReadOnly _criterion As Func(Of Boolean)
    Private ReadOnly _timer As New Timer
    Private _count As Integer

    ''' <summary>
    ''' Initializes a new instance the <see cref="WaitUntilDialog"/>.
    ''' <para><see cref="WaitUntilDialog"/> provides a dialog to wait for a criterion to occur.</para>
    ''' </summary>
    ''' <param name="criterion">Criterion to evaluate.</param>
    ''' <param name="expirationTime">Maximum time to wait for criterion.</param>
    Sub New(criterion As Func(Of Boolean), expirationTime As Integer)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        _criterion = criterion
        If _criterion.Invoke Then _Successfully = True : Exit Sub

        ProgressBar.Maximum = expirationTime * 50
        _timer.Interval = 20
        _timer.Enabled = True
        AddHandler _timer.Tick, AddressOf TimerTickEventHandler
        Translator.TranslateControls(Me)
        Me.ShowDialog()
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the user clicks the Cancel button.
    ''' <para>It closes the dialogbox.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles ltCancel.Click, Me.Closed
        Me.Hide()
    End Sub

    ''' <summary>
    ''' EventHandler for the event that occurs when the timer ticks.
    ''' <para>It evaluates the criterion (closes the dailog if true) and displays the time progress.</para>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TimerTickEventHandler(sender As Object, e As EventArgs)
        _count += 1
        If _criterion.Invoke Then _Successfully = True : Me.Hide()
        If _count > ProgressBar.Maximum Then Me.Hide() : Exit Sub

        ProgressBar.Value = _count
        ProgressBar.Value = _count - 1
        Me.Refresh()
    End Sub

End Class