Public Class SettingsWindow

    Private IsReady As Boolean = False

    Private ReadOnly Property WisemanApplication As WisemanWindowsClient.Application
        Get
            Return Application.Current
        End Get
    End Property

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim radio As RadioButton = ThemeSelectionButtons.FindName(String.Format("{0}ThemeButton", WisemanApplication.Settings.ThemeName))
        If TypeOf (radio) Is RadioButton Then
            radio.IsChecked = True
        End If
        Dim radio2 As RadioButton = SchedulerSelectionButtons.FindName(String.Format("{0}SchedulerButton", WisemanApplication.Settings.Schedule.ToString))
        If TypeOf (radio2) Is RadioButton Then
            radio2.IsChecked = True
        End If
        IsReady = True
    End Sub

    Private Sub ThemeRadioButton_Checked(sender As Object, e As RoutedEventArgs) Handles WhiteThemeButton.Checked, CimrmanThemeButton.Checked
        If IsReady Then
            Dim radio As RadioButton = sender
            WisemanApplication.Settings.ThemeName = radio.Tag
            WisemanApplication.LoadTheme(WisemanApplication.Settings.ThemeName)
        End If
    End Sub

    Private Sub ScheduleRadioButton_Checked(sender As Object, e As RoutedEventArgs) Handles NeverSchedulerButton.Checked, DailySchedulerButton.Checked, HourlySchedulerButton.Checked
        If IsReady Then
            Dim radio As RadioButton = sender
            WisemanApplication.Settings.Schedule = [Enum].Parse(GetType(WisemanSimpleSchedulesEnum), radio.Tag.ToString())
            WisemanApplication.InitializeScheduler()
        End If
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As RoutedEventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub HeaderControl_OnDragRequested() Handles HeaderControl.OnDragMoveRequested
        Me.DragMove()
    End Sub

    Private Sub HeaderControl_OnCloseRequested() Handles HeaderControl.OnCloseRequested
        Me.Close()
    End Sub

End Class
