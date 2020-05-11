Public Class LeechOptions

    Private Sub LeechOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.leechoptions = "emailpass" Then
            RadioButton1.Checked = True
        ElseIf My.Settings.leechoptions = "userpass" Then
            RadioButton2.Checked = True
        ElseIf My.Settings.leechoptions = "proxies" Then
            RadioButton4.Checked = True
        ElseIf My.Settings.leechoptions = "emailonly" Then
            RadioButton3.Checked = True
        ElseIf My.Settings.leechoptions = "custom" Then
            RadioButton5.Checked = True
            TextBox1.Text = My.Settings.customregex
        End If
    End Sub

    Private Sub LeechOptions_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If RadioButton1.Checked = True Then
            My.Settings.leechoptions = "emailpass"
            My.Settings.Save()
        ElseIf RadioButton2.Checked = True Then
            My.Settings.leechoptions = "userpass"
            My.Settings.Save()
        ElseIf RadioButton4.Checked = True Then
            My.Settings.leechoptions = "proxies"
            My.Settings.Save()
        ElseIf RadioButton3.Checked = True Then
            My.Settings.leechoptions = "emailonly"
            My.Settings.Save()
        ElseIf RadioButton5.Checked = True Then
            If TextBox1.Text = Nothing Then
                MsgBox("Please insert a custom regex!")
            Else
                My.Settings.leechoptions = "custom"
                My.Settings.Save()

                My.Settings.customregex = TextBox1.Text
                My.Settings.Save()
            End If
        End If
    End Sub
End Class