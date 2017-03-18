Public Class SettingsWindow

    Private ReadOnly Property WisemanApplication As WisemanWindowsClient.Application
        Get
            Return Application.Current
        End Get
    End Property

    Private Sub RadioButton_Checked(sender As Object, e As RoutedEventArgs) Handles WhiteThemeButton.Checked, BlackThemeButton.Checked, BlueThemeButton.Checked
        If IsLoaded Then
            Dim radio As RadioButton = sender
            WisemanApplication.Settings.ThemeName = radio.Tag
            WisemanApplication.LoadTheme(WisemanApplication.Settings.ThemeName)
        End If
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Dim radio As RadioButton = ThemeSelectionButtons.FindName(String.Format("{0}ThemeButton", WisemanApplication.Settings.ThemeName))
        If TypeOf (radio) Is RadioButton Then
            radio.IsChecked = True
        End If
    End Sub

End Class
