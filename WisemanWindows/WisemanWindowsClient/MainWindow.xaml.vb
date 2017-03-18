Imports Wiseman

Class MainWindow

    Private Property client As New WisemanClient()

    Private ReadOnly Property WisemanApplication As WisemanWindowsClient.Application
        Get
            Return Application.Current
        End Get
    End Property

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

    Private Sub TestButton_Click(sender As Object, e As RoutedEventArgs) Handles TestButton.Click
        Dim window As New SettingsWindow()
        window.Owner = Me
        window.ShowDialog()
    End Sub

End Class
