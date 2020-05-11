Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.IO
Imports xNet

Public Class Main

    Dim Errors As Integer
    Dim ProgErrors As Integer
    Dim AllResult As Integer

    Dim StopThread As Boolean

    ReadOnly AppName As String = "B3RAP Leecher"
    ReadOnly AppVersion As String = "0.6"
    ReadOnly AppCompany As String = "B3RAP Softwares"

    Dim ScrapThr As Thread
    Public Threads As Integer = 150

    Public Proxies As New List(Of String)
    Public ProxiesType As String
    Public UseProxies As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Errors = 0
            ProgErrors = 0
            TextBox2.Clear()
            Label3.Text = "None"
            Label4.Text = "None"
            Label9.Text = "0"
            Label10.Text = "0"
            Label11.Text = "0"
            TextBox1.Enabled = False
            TextBox3.Enabled = False
            TextBox6.Enabled = False
            Button1.Enabled = False
            Button2.Enabled = True
            Button4.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            StopThread = False
            ScrapThr = New Thread(AddressOf GetAccounts) : ScrapThr.Start()
        Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub GetAccounts()
        Try
            ThreadPool.SetMinThreads(Threads, Threads)
            ThreadPool.SetMaxThreads(Threads, Threads)

            For Each Keyword As String In TextBox1.Lines
                If StopThread Then Exit Sub
                For Each Website As String In TextBox3.Lines
                    If StopThread Then Exit Sub
                    For Each Engine As String In TextBox6.Lines
                        If StopThread Then Exit Sub
                        Leech(Engine, Keyword, Website)
                    Next
                Next
            Next
        Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub Leech(SearchEngine As String, Keyword As String, Website As String)
        Try
request:    Using Req As New HttpRequest
                If StopThread Then Exit Sub

                Req.UserAgent = Randomizer.RandomUserAgent
                If UseProxies Then
                    Req.Type = ProxiesType
                    Req.Proxy = Proxies(New Random().Next(Proxies.Count))
                End If

                Label4.Text = Keyword
                Label14.Text = Website
                Label3.Text = "Getting links..."

                Dim Respo As String = Req.Start(HttpMethod.GET, New Uri(SearchEngine & "site:" & Website & "+" & Keyword), Nothing).ToString
                Dim Rege As MatchCollection = New Regex("(https:\/\/" & Website & "\/\w+)").Matches(Respo)

                If Rege.Count = 0 Then : Label3.Text = "Done!" : Else
                    TextBox5.Clear()

                    For i = 0 To Rege.Count - 1
                        If StopThread Then Exit Sub
                        TextBox5.AppendText(Rege(i).Value & vbNewLine)
                    Next

                    Dim TextLines = TextBox5.Text.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)
                    TextBox5.Text = String.Join(vbNewLine, TextLines.Distinct.ToArray())
                    Label3.Text = "Got " & TextBox5.Lines.Count & " links."

                    For i = 0 To TextBox5.Lines.Count - 1
                        If StopThread Then Exit Sub
                        AppendResult(Req.Start(HttpMethod.GET, New Uri(TextBox5.Lines(i)), Nothing).ToString)
                    Next
                End If
            End Using
        Catch
            Errors += 1
            Label9.Text = Errors.ToString
            GoTo request
        End Try
    End Sub

    Private Sub AppendResult(Respo As String)
        Try
            If Respo.Count = 0 Or Not Respo.Count > 1 Or Respo.Count < 1 Then : Label3.Text = "Done!" : Else
                Label3.Text = "Getting result..."
                If My.Settings.leechoptions = "emailpass" Then
                    For Each I As Match In New Regex("([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}):([a-zA-Z0-9_\-\.]+)").Matches(Respo)
                        If StopThread Then Exit Sub
                        TextBox2.AppendText(I.Value & vbCrLf)
                    Next
                ElseIf My.Settings.leechoptions = "userpass" Then
                    For Each I As Match In New Regex("[a-z0-9_-]{3,16}:([a-zA-Z0-9_\-\.]+)").Matches(Respo)
                        If StopThread Then Exit Sub
                        TextBox2.AppendText(I.Value & vbCrLf)
                    Next
                ElseIf My.Settings.leechoptions = "proxies" Then
                    For Each I As Match In New Regex("(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})(?=[^\d])\s*:?\s*(\d{2,5})").Matches(Respo)
                        If StopThread Then Exit Sub
                        TextBox2.AppendText(I.Value & vbCrLf)
                    Next
                ElseIf My.Settings.leechoptions = "emailonly" Then
                    For Each I As Match In New Regex("([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})").Matches(Respo)
                        If StopThread Then Exit Sub
                        TextBox2.AppendText(I.Value & vbCrLf)
                    Next
                ElseIf My.Settings.leechoptions = "custom" Then
                    For Each I As Match In New Regex(My.Settings.customregex).Matches(Respo)
                        If StopThread Then Exit Sub
                        TextBox2.AppendText(I.Value & vbCrLf)
                    Next
                End If
            End If
        Catch
            Errors += 1
            Label9.Text = Errors.ToString
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try : LeechOptions.Show() : Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try : MsgBox(AppName & " v" & AppVersion & " by " & AppCompany & " (YouTube Channel ID : " & AppCompany.Replace(" ", Nothing) & ").") : Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            Dim TextLines = TextBox2.Text.Split(New String() {vbNewLine}, StringSplitOptions.RemoveEmptyEntries)
            TextBox2.Text = String.Join(vbNewLine, TextLines.Distinct.ToArray())
            Label11.Text = (AllResult + 1).ToString
            MsgBox("Removed duplicates.")
        Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Try
            AllResult = TextBox2.Lines.Count - 1
            Label11.Text = AllResult.ToString
            If TextBox2.Text = Nothing Then
                Button6.Enabled = False
                Button5.Enabled = False
            Else
                Button6.Enabled = True
                Button5.Enabled = True
            End If
        Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try : SaveFileDialog1.ShowDialog() : Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            File.AppendAllText(SaveFileDialog1.FileName, TextBox2.Text)
            MsgBox("Sucessfully wrote the result in " & Path.GetFileName(SaveFileDialog1.FileName) & ".")
        Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            StopThread = True
            ScrapThr.Abort()
            TextBox1.Enabled = True
            TextBox3.Enabled = True
            TextBox6.Enabled = True
            Button1.Enabled = True
            Button2.Enabled = False
            Button4.Enabled = True
            Button8.Enabled = True
            Button9.Enabled = True
            Button10.Enabled = True
            Label3.Text = "Stopped."
            Label4.Text = "None"
        Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CheckForIllegalCrossThreadCalls = False
            Text = AppName & " v" & AppVersion
            Label1.Text = "Keywords (" & TextBox1.Lines.Count & ") :"
            Label6.Text = "Paste Sites (" & TextBox3.Lines.Count & ") :"
            Label13.Text = "Search Engines (" & TextBox6.Lines.Count & ") :"
        Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try : Label1.Text = "Keywords (" & TextBox1.Lines.Count & ") :" : Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Try : Label6.Text = "Paste Sites (" & TextBox3.Lines.Count & ") :" : Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try : MoreOptions.Show() : Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Try : Dim T As New Thread(AddressOf Tester) : T.Start() : Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub Tester()
        Try
            Dim Valid As Integer
            Button9.Enabled = False
            For Each Website As String In TextBox3.Lines
                Using req As New HttpRequest
                    req.UserAgent = Randomizer.RandomUserAgent
                    If UseProxies Then
                        req.Type = ProxiesType
                        req.Proxy = Proxies(New Random().Next(Proxies.Count))
                    End If
                    If req.Start(HttpMethod.GET, New Uri("http://" & Website), Nothing).IsOK Then
                        Valid += 1
                        Button9.Text = "Test (" & Valid & "/" & TextBox3.Lines.Count & ")"
                    Else : MsgBox("Invalid paste site : " & Website) : End If
                End Using
            Next
            Button9.Enabled = True
            Button9.Text = "Test"
            MsgBox("Finished testing the paste sites!")
        Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        Try : Label13.Text = "Search Engines (" & TextBox6.Lines.Count & ") :" : Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Try : Dim T As New Thread(AddressOf Tester2) : T.Start() : Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub

    Private Sub Tester2()
        Try
            Dim Valid As Integer
            Button10.Enabled = False
            For Each Website As String In TextBox6.Lines
                Using req As New HttpRequest
                    req.UserAgent = Randomizer.RandomUserAgent
                    If UseProxies Then
                        req.Type = ProxiesType
                        req.Proxy = Proxies(New Random().Next(Proxies.Count))
                    End If
                    If req.Start(HttpMethod.GET, New Uri(Website & "hello"), Nothing).IsOK Then
                        Valid += 1
                        Button10.Text = "Test (" & Valid & "/" & TextBox6.Lines.Count & ")"
                    Else : MsgBox("Invalid search engine : " & Website) : End If
                End Using
            Next
            Button10.Enabled = True
            Button10.Text = "Test"
            MsgBox("Finished testing the search engines!")
        Catch
            ProgErrors += 1
            Label10.Text = ProgErrors.ToString
        End Try
    End Sub
End Class