Public Class WisemanClient

    Private Property Settings As WisemanSettings

    Public Sub New(Optional pSettings As WisemanSettings = Nothing)
        If pSettings Is Nothing Then
            pSettings = New WisemanSettings()
        End If
        Me.Settings = pSettings
    End Sub

    Public Function GetJSONURL()
        Return Settings.WisemanServerURL + "/json/default/json"
    End Function

    Public Async Function FetchQuotesFromJSON() As Task(Of String)
        Dim url = Me.GetJSONURL()

        Dim client As New System.Net.Http.HttpClient()
        Dim r As System.Net.Http.HttpResponseMessage = Await client.GetAsync(url)
        Dim s As String = Await r.Content.ReadAsStringAsync()
        Return s
    End Function

    Public Async Function FetchRandomQuote() As Task(Of Quote)
        Dim client As New System.Net.Http.HttpClient()
        Dim r As System.Net.Http.HttpResponseMessage = Await client.GetAsync(Me.Settings.WisemanServerURL + "/random/default/random")
        Dim s As String = Await r.Content.ReadAsStringAsync()
        Return Quote.FromJSON(s)
    End Function


End Class
