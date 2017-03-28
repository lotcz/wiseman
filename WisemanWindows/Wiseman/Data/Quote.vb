Public Class Quote

    Public Property Text As String

    Public Property Author As String

    Public Property Origin As String

    Shared Function FromJSON(str As String) As Quote
        Dim jsonObj As JSONObject = JSONObject.Parse(str)
        Dim q As New Quote()
        q.Author = jsonObj("author_name").ToString()
        q.Origin = jsonObj("origin_name").ToString()
        q.Text = jsonObj("quote_text").ToString()
        Return q
    End Function
End Class
