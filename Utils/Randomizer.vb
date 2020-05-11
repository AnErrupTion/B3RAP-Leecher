Public Class Randomizer

    Public Shared Function RandomUserAgent() As String
        Select Case New Random().Next(18)
            Case 0 : Return UpdatedUserAgents.IEUserAgent
            Case 1 : Return UpdatedUserAgents.OperaUserAgent
            Case 2 : Return UpdatedUserAgents.ChromeUserAgent
            Case 3 : Return UpdatedUserAgents.FirefoxUserAgent
            Case 4 : Return UpdatedUserAgents.EdgeUserAgent
            Case 5 : Return UpdatedUserAgents.BraveUserAgent
            Case 6 : Return UpdatedUserAgents.ChromiumEdgeUserAgent
            Case 7 : Return UpdatedUserAgents.OperaMiniUserAgent
            Case 8 : Return UpdatedUserAgents.GooglebotUserAgent
            Case 9 : Return UpdatedUserAgents.BingbotUserAgent
            Case 10 : Return UpdatedUserAgents.YahoobotUserAgent
            Case 11 : Return UpdatedUserAgents.BaiduspiderUserAgent
            Case 12 : Return UpdatedUserAgents.YandexbotUserAgent
            Case 13 : Return UpdatedUserAgents.SogouTestspiderUserAgent
            Case 14 : Return UpdatedUserAgents.KonquerorUserAgent
            Case 15 : Return UpdatedUserAgents.ExabotUserAgent
            Case 16 : Return UpdatedUserAgents.FacebookExtHitUserAgent
            Case 17 : Return UpdatedUserAgents.AlexabotUserAgent
        End Select
        Return Nothing
    End Function

End Class
