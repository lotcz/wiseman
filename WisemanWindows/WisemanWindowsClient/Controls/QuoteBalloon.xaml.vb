Public Class QuoteBalloon

    Private ReadOnly Property WisemanApplication As WisemanWindowsClient.Application
        Get
            Return Application.Current
        End Get
    End Property

    Public Sub New(q As Quote)
        InitializeComponent()
        QuoteText.Content = q.Text
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        WisemanApplication.HideQuoteBalloon()
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        WisemanApplication.ShowMainWindow()
    End Sub

End Class
