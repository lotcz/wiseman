Imports Wiseman

Class MainWindow

    Private Property client As New WisemanClient()

    Private Async Sub LoadRandomQuote()
        Dim q As Quote = Await client.FetchRandomQuote()
        quoteTextBlock.Text = q.Text
        AuthorTextBlock.Text = q.Author
        SourceTextBlock.Text = q.Source
    End Sub

    Private Sub NextButton_Click(sender As Object, e As RoutedEventArgs) Handles NextButton.Click
        LoadRandomQuote()
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        LoadRandomQuote()
    End Sub

    Private Sub FakeWindowHead_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles FakeWindowHead.MouseDown
        If (e.ChangedButton = MouseButton.Left) Then
            Me.DragMove()
        End If
    End Sub

    Private Sub FakeWindowClose_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles FakeWindowClose.MouseUp
        If (e.ChangedButton = MouseButton.Left) Then
            Me.Close()
        End If
    End Sub

    Private Sub NotifButton_Click(sender As Object, e As RoutedEventArgs) Handles NotifButton.Click
        Dim app As WisemanWindowsClient.Application = Application.Current
        app.LoadTheme("MyNewTheme")
    End Sub

End Class
