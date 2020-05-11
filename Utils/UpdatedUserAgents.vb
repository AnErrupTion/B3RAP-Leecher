Public Class UpdatedUserAgents

    Private Shared ReadOnly UserAgents As String() = {"Mozilla/5.0 ({0}; WOW64; Trident/{1}; rv:{2}) like Gecko", "Mozilla/5.0 ({0}; {1}) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{2} Safari/537.36 OPR/{3}", "Mozilla/5.0 ({0}; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36", "Mozilla/5.0 ({0}; Win64; x64; rv:69.0) Gecko/20100101 Firefox/69.0", "Mozilla/5.0 ({0}; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36 Edge/18.18362", "Mozilla/5.0 ({0}; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Brave Chrome/76.0.3809.132 Safari/537.36", "Mozilla/5.0 ({0}; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.34 Safari/537.36 Edg/78.0.276.11", "Mozilla/5.0 (Linux; U; {0}; SM-J710F Build/M1AJQ; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/77.0.3865.73 Mobile Safari/537.36 OPR/44.1.2254.143214", "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)", "Mozilla/5.0 (compatible; Bingbot/2.0; +http://www.bing.com/bingbot.htm)", "Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)", "Mozilla/5.0 (compatible; Baiduspider/2.0; +http://www.baidu.com/search/spider.html)", "Mozilla/5.0 (compatible; YandexBot/3.0; +http://yandex.com/bots)", "Sogou-Test-Spider/4.0 (compatible; MSIE 5.5; Windows 98)", "Mozilla/5.0 (compatible; Konqueror/3.5; Linux) KHTML/3.5.5 (like Gecko) (Exabot-Thumbnails)", "Mozilla/5.0 (compatible; Exabot/3.0; +http://www.exabot.com/go/robot)", "facebookexternalhit/{0} (+http://www.facebook.com/externalhit_uatext.php)", "ia_archiver (+http://www.alexa.com/site/help/webmasters; crawler@alexa.com)"}

    Public Shared Function IEUserAgent() As String
        Dim windowsVersion As String = RandomWindowsVersion()
        Dim version As String
        Dim trident As String

        If windowsVersion.Contains("NT 5.1") Then
            version = "9.0"
            trident = "5.0"
        ElseIf windowsVersion.Contains("NT 6.0") Then
            version = "9.0"
            trident = "5.0"
        Else
            version = "11.0"
            trident = "7.0"
        End If

        Return String.Format(UserAgents(0), windowsVersion, trident, version)
    End Function

    Public Shared Function OperaUserAgent() As String
        Dim windowsVersion As String = RandomWindowsVersion()
        Dim chromeVersion As String = String.Empty
        Dim operaVersion As String = String.Empty
        Dim systemType As String

        If windowsVersion.Contains("NT 5.1") OrElse windowsVersion.Contains("NT 6.0") Then
            chromeVersion = "49.0.2623.112"
            operaVersion = "36.0.2130.80"
            systemType = "WOW64"
        Else
            systemType = "Win64; x64"

            Select Case New Random().Next(2)
                Case 0
                    chromeVersion = "76.0.3809.132"
                    operaVersion = "63.0.3368.71"
                Case 1
                    chromeVersion = "76.0.3809.132"
                    operaVersion = "63.0.3368.54789"
            End Select
        End If

        Return String.Format(UserAgents(1), windowsVersion, systemType, chromeVersion, operaVersion)
    End Function

    Public Shared Function ChromeUserAgent() As String
        Dim windowsVersion As String = RandomWindowsVersion()
        Return String.Format(UserAgents(2), windowsVersion)
    End Function

    Public Shared Function FirefoxUserAgent() As String
        Dim windowsVersion As String = RandomWindowsVersion()
        Return String.Format(UserAgents(3), windowsVersion)
    End Function

    Public Shared Function EdgeUserAgent() As String
        Dim windowsVersion As String = RandomWindowsVersion()
        Return String.Format(UserAgents(4), windowsVersion)
    End Function

    Public Shared Function BraveUserAgent() As String
        Dim windowsVersion As String = RandomWindowsVersion()
        Return String.Format(UserAgents(5), windowsVersion)
    End Function

    Public Shared Function ChromiumEdgeUserAgent() As String
        Dim windowsVersion As String = RandomWindowsVersion()
        Return String.Format(UserAgents(6), windowsVersion)
    End Function

    Public Shared Function OperaMiniUserAgent() As String
        Dim androidVersion As String = RandomAndroidVersion()
        Return String.Format(UserAgents(7), androidVersion)
    End Function

    Public Shared Function GooglebotUserAgent() As String
        Return String.Format(UserAgents(8))
    End Function

    Public Shared Function BingbotUserAgent() As String
        Return String.Format(UserAgents(9))
    End Function

    Public Shared Function YahoobotUserAgent() As String
        Return String.Format(UserAgents(10))
    End Function

    Public Shared Function BaiduspiderUserAgent() As String
        Return String.Format(UserAgents(11))
    End Function

    Public Shared Function YandexbotUserAgent() As String
        Return String.Format(UserAgents(12))
    End Function

    Public Shared Function SogouTestspiderUserAgent() As String
        Return String.Format(UserAgents(13))
    End Function

    Public Shared Function KonquerorUserAgent() As String
        Return String.Format(UserAgents(14))
    End Function

    Public Shared Function ExabotUserAgent() As String
        Return String.Format(UserAgents(15))
    End Function

    Public Shared Function FacebookExtHitUserAgent() As String
        Dim version As String = String.Empty

        Select Case New Random().Next(2)
            Case 0
                version = "1.0"
            Case 1
                version = "1.1"
        End Select

        Return String.Format(UserAgents(16), version)
    End Function

    Public Shared Function AlexabotUserAgent() As String
        Return String.Format(UserAgents(17))
    End Function

    Private Shared Function RandomWindowsVersion() As String
        Dim windowsVersion As String = "Windows NT "

        Select Case New Random().Next(6)
            Case 0
                windowsVersion += "5.1"
            Case 1
                windowsVersion += "6.0"
            Case 2
                windowsVersion += "6.1"
            Case 3
                windowsVersion += "6.2"
            Case 4
                windowsVersion += "6.3"
            Case 5
                windowsVersion += "10.0"
        End Select

        Return windowsVersion
    End Function

    Private Shared Function RandomAndroidVersion() As String
        Dim androidVersion As String = "Android "

        Select Case New Random().Next(28)
            Case 0
                androidVersion += "1.0"
            Case 1
                androidVersion += "1.1"
            Case 2
                androidVersion += "1.5"
            Case 3
                androidVersion += "1.6"
            Case 4
                androidVersion += "2.0"
            Case 5
                androidVersion += "2.1"
            Case 6
                androidVersion += "2.2"
            Case 7
                androidVersion += "2.2.3"
            Case 8
                androidVersion += "2.3"
            Case 9
                androidVersion += "2.3.7"
            Case 10
                androidVersion += "3.0"
            Case 11
                androidVersion += "3.2.6"
            Case 12
                androidVersion += "4.0"
            Case 13
                androidVersion += "4.0.4"
            Case 14
                androidVersion += "4.1"
            Case 15
                androidVersion += "4.3.1"
            Case 16
                androidVersion += "4.4"
            Case 17
                androidVersion += "4.4.4"
            Case 18
                androidVersion += "5.0"
            Case 19
                androidVersion += "5.1.1"
            Case 20
                androidVersion += "6.0"
            Case 21
                androidVersion += "6.0.1"
            Case 22
                androidVersion += "7.0"
            Case 23
                androidVersion += "7.1.2"
            Case 24
                androidVersion += "8.0"
            Case 25
                androidVersion += "8.1"
            Case 26
                androidVersion += "9.0"
            Case 27
                androidVersion += "10.0"
        End Select

        Return androidVersion
    End Function

End Class
