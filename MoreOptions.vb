Imports System.IO

Public Class MoreOptions

    Private Sub MoreOptions_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Main.Threads = Integer.Parse(NumericUpDown1.Value)
        If CheckBox2.Checked Then : Main.UseProxies = True : Else : Main.UseProxies = False : End If
        If ComboBox1.SelectedIndex = 0 Then : Main.ProxiesType = "HTTP" : ElseIf ComboBox1.SelectedIndex = 1 Then : Main.ProxiesType = "SOCKS4" : ElseIf ComboBox1.SelectedIndex = 2 Then : Main.ProxiesType = "SOCKS5" : End If
    End Sub

    Private Sub MoreOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NumericUpDown1.Value = Decimal.Parse(Main.Threads)
        ComboBox1.SelectedIndex = 0
        If Main.UseProxies Then : CheckBox2.Checked = True : Else : CheckBox2.Checked = False : End If
        If Main.ProxiesType = "HTTP" Then : ComboBox1.SelectedIndex = 0 : ElseIf Main.ProxiesType = "SOCKS4" Then : ComboBox1.SelectedIndex = 1 : ElseIf Main.ProxiesType = "SOCKS5" Then : ComboBox1.SelectedIndex = 2 : End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        For Each Line As String In File.ReadAllLines(OpenFileDialog1.FileName) : Main.Proxies.Add(Line) : Next
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            Button9.Enabled = True
            ComboBox1.Enabled = True
            Main.UseProxies = True
        Else
            Button9.Enabled = False
            ComboBox1.Enabled = False
            Main.UseProxies = False
        End If
    End Sub
End Class