Imports Wiseman

Class MainWindow

    Private Property client As New WisemanClient()

    Private ReadOnly Property WisemanApplication As WisemanWindowsClient.Application
        Get
            Return Application.Current
        End Get
    End Property

    Public Async Sub LoadRandomQuote()
        Dim q As Quote = Await client.FetchRandomQuote()
        quoteTextBlock.Text = q.Text
        AuthorTextBlock.Content = q.Author
        SourceTextBlock.Text = q.Source
    End Sub

    Private Sub NextButton_Click(sender As Object, e As RoutedEventArgs) Handles NextButton.Click
        LoadRandomQuote()
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        LoadRandomQuote()
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As RoutedEventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub SettingsButton_Click(sender As Object, e As RoutedEventArgs) Handles SettingsButton.Click
        Dim window As New SettingsWindow()
        window.Owner = Me
        window.ShowDialog()
    End Sub

    Private Sub HeaderControl_OnDragRequested() Handles HeaderControl.OnDragMoveRequested
        Me.DragMove()
    End Sub

    Private Sub HeaderControl_OnCloseRequested() Handles HeaderControl.OnCloseRequested
        Me.Close()
    End Sub

    Private Sub TestButton_Click(sender As Object, e As RoutedEventArgs) Handles TestButton.Click
        ' WisemanApplication.TestBalloon()
        Dim schedule As New WisemanSchedule()
        schedule.ScheduleType = WisemanScheduleTypeEnum.Periodically
        schedule.ScheduleDays = New SchedulerAllowedDays(0)
        schedule.ScheduleTime = New DateTime(2017, 1, 1, 0, 0, 20)
        WisemanApplication.InitializeScheduler(schedule)
    End Sub

    Private Sub TestButton2_Click(sender As Object, e As RoutedEventArgs) Handles Test2Button.Click
        WisemanApplication.TestCustomBalloon()
    End Sub

End Class
