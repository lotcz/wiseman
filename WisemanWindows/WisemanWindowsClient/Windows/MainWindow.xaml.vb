Imports Wiseman

Class MainWindow

    Private ReadOnly Property WisemanApplication As WisemanWindowsClient.Application
        Get
            Return Application.Current
        End Get
    End Property

    Private CurrentQuote As Quote

    Public Async Sub LoadRandomQuote()
        LoadingAnimControl.Show()
        Try
            Dim q As Quote = Await WisemanApplication.Client.FetchRandomQuote()
            DisplayQuote(q)
        Catch e As Exception
            quoteTextBlock.Text = "Error"
            CurrentQuote = Nothing
        End Try
        LoadingAnimControl.Hide()
    End Sub

    Public Sub DisplayQuote(q As Quote)
        CurrentQuote = q
        quoteTextBlock.Text = q.Text
        AuthorTextRun.Text = q.Author
        OriginTextRun.Text = q.Origin
    End Sub

    Public Sub CloseWindow()
        Me.Close()
    End Sub

    Private Sub NextButton_Click(sender As Object, e As RoutedEventArgs) Handles NextButton.Click
        LoadRandomQuote()
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If Not TypeOf (CurrentQuote) Is Quote Then
            LoadRandomQuote()
        End If
    End Sub

    Private Sub SettingsButton_Click(sender As Object, e As RoutedEventArgs) Handles SettingsButton.MouseLeftButtonUp
        Dim window As New SettingsWindow()
        window.Owner = Me
        window.ShowDialog()
    End Sub

    Private Sub HeaderControl_OnDragRequested() Handles HeaderControl.OnDragMoveRequested
        Me.DragMove()
    End Sub

    Private Sub HeaderControl_OnCloseRequested() Handles HeaderControl.OnCloseRequested
        CloseWindow()
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As RoutedEventArgs) Handles CloseButton.Click
        CloseWindow()
    End Sub

End Class
