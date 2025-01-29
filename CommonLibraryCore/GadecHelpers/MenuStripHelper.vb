'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Provides methode for creating a <see cref="ContextMenuStrip"/> from the data held in a database (-collection).
''' <para>The database should contains the fields:</para>
''' <para>- Name: The tag of the menu-item;</para>
''' <para>- Format: The format of the menu-item;</para>
''' <para>- Picture: The name of the image in the <see cref="Resources.ResourceManager"/>;</para>
''' <para>- EN, NL, DE and/or FR: Description in different languages.</para>
''' <para>Format can held the following text:</para>
''' <para>- Normal, Bold or Italic: Type of font to use;</para>
''' <para>- Black, Red, Blue or Green: Color of the font.</para>
''' <para>- Divider: For a <see cref="ToolStripSeparator"/> on the context menu;</para>
''' <para>- ComboBox: For a <see cref="ToolStripComboBox"/> on the context menu, which requires a separate database in the same collection with the same name as the menu item.</para>
''' <para>The combobox database should contains the fields:</para>
''' <para>- Name: The tag of the combobox-item;</para>
''' <para>- Data: The data which can be used when the user clicks the item;</para>
''' <para>- EN, NL, DE and/or FR: Description in different languages.</para>
''' </summary>
Public Class MenuStripHelper

    ''' <summary>
    ''' Creates a context menu from the data held in a database.
    ''' </summary>
    ''' <param name="contextMenuData">The database with the context menu data.</param>
    ''' <param name="eventAction">The <see cref="EventHandler"/> to use when user clicked an item.</param>
    ''' <param name="resourceManager">The <see cref="Resources.ResourceManager"/> where images are stored.</param>
    ''' <returns>The context menu.</returns>
    Public Shared Function Create(contextMenuData As DataTable, eventAction As EventHandler, Optional resourceManager As Resources.ResourceManager = Nothing) As ContextMenuStrip
        If IsNothing(contextMenuData) Then Return Nothing

        If IsNothing(resourceManager) Then resourceManager = My.Resources.ResourceManager
        Dim output = New ContextMenuStrip
        For Each row In contextMenuData.Select
            Dim tag = row.GetString("Name")
            Dim format = row.GetString("Format")
            Select Case format
                Case "Divider"
                    Dim toolStripItem = New ToolStripSeparator With {.Tag = tag, .Name = tag}
                    output.Items.Add(toolStripItem)
                Case "ComboBox"
                    Dim toolStripItem = CreateToolStripComboBox(row.GetTranslation, contextMenuData.GetTable(tag))
                    AddHandler toolStripItem.SelectedIndexChanged, eventAction
                    output.Items.Add(toolStripItem)
                Case "Button"
                    Dim toolStripItem = CreateToolStripButton(row, resourceManager.GetObject(row.GetString("Picture")))
                    AddHandler toolStripItem.Click, eventAction
                    output.Items.Add(toolStripItem)
                Case Else
                    Dim toolStripItem = output.Items.Add(row.GetTranslation, resourceManager.GetObject(row.GetString("Picture")), eventAction)
                    WireUpToolStripItem(toolStripItem, row)
                    output.Items.Add(toolStripItem)
            End Select
        Next
        Return output
    End Function

    'private subs

    ''' <summary>
    ''' Sets the name, tag and textformat of the <see cref="ToolStripItem"/>.
    ''' </summary>
    ''' <param name="contextMenuItem">The <see cref="ToolStripItem"/>.</param>
    ''' <param name="contextMenuRow">The record with the context menu item data.</param>
    Private Shared Sub WireUpToolStripItem(contextMenuItem As ToolStripItem, contextMenuRow As DataRow)
        If IsNothing(contextMenuRow) Then Exit Sub

        contextMenuItem.Tag = contextMenuRow.GetString("Name")
        contextMenuItem.Name = contextMenuRow.GetString("Name")
        Dim format = contextMenuRow.GetString("Format")
        Select Case True
            Case format.Contains("Bold") : contextMenuItem.Font = FontHelper.SansSerifBold
            Case format.Contains("Italic") : contextMenuItem.Font = FontHelper.SansSerifItalic
        End Select
        Select Case True
            Case format.Contains("Red") : contextMenuItem.ForeColor = Color.DarkRed
            Case format.Contains("Blue") : contextMenuItem.ForeColor = Color.DarkBlue
            Case format.Contains("Green") : contextMenuItem.ForeColor = Color.DarkGreen
        End Select
    End Sub

    'private functions

    ''' <summary>
    ''' Creates a <see cref="ToolStripButton"/>.
    ''' </summary>
    ''' <param name="contextMenuRow">The record with the context menu item data.</param>
    ''' <param name="image">The <see cref="Image"/> to show on the left of the button.</param>
    ''' <returns>The <see cref="ToolStripButton"/>.</returns>
    Private Shared Function CreateToolStripButton(contextMenuRow As DataRow, image As Image) As ToolStripButton
        If IsNothing(contextMenuRow) Then Return Nothing

        Dim output = New ToolStripButton With {
            .Tag = contextMenuRow.GetString("Name"),
            .Name = contextMenuRow.GetString("Name"),
            .Text = contextMenuRow.GetTranslation,
            .Image = image
        }
        Dim format = contextMenuRow.GetString("Format")
        Select Case True
            Case format.Contains("Bold") : output.Font = FontHelper.SansSerifBold
            Case format.Contains("Italic") : output.Font = FontHelper.SansSerifItalic
        End Select
        Select Case True
            Case format.Contains("Red") : output.ForeColor = Color.DarkRed
            Case format.Contains("Blue") : output.ForeColor = Color.DarkBlue
            Case format.Contains("Green") : output.ForeColor = Color.DarkGreen
        End Select
        Return output
    End Function

    ''' <summary>
    ''' Creates a <see cref="ToolStripComboBox"/>.
    ''' </summary>
    ''' <param name="title">The title of the combobox.</param>
    ''' <param name="contextMenuComboBoxData">The database with the context menu combobox data.</param>
    ''' <returns>The <see cref="ToolStripComboBox"/>.</returns>
    Private Shared Function CreateToolStripComboBox(title As String, contextMenuComboBoxData As DataTable) As ToolStripComboBox
        If IsNothing(contextMenuComboBoxData) Then Return Nothing

        Dim output = New ToolStripComboBox With {
            .Tag = contextMenuComboBoxData.TableName,
            .Name = contextMenuComboBoxData.TableName,
            .DropDownStyle = ComboBoxStyle.DropDown
        }
        Dim keys = contextMenuComboBoxData.GetStringsFromColumn("Name")
        Dim preSelectedIndex = 0
        output.Items.Add(title)
        Dim preSelectedItem = Registerizer.UserSetting("{0}_Selected".Compose(contextMenuComboBoxData.TableName))
        For Each key In keys
            output.Items.Add(key)
            If key = preSelectedItem Then preSelectedIndex = output.Items.Count - 1
        Next
        If preSelectedIndex > -1 Then output.SelectedIndex = preSelectedIndex
        output.Width = 160
        Return output
    End Function

End Class
