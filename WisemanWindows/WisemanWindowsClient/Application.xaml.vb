Imports System.Windows.Markup
Imports System.IO
Imports System.Windows.Threading
Imports System.Windows.Controls.Primitives
Imports Hardcodet.Wpf.TaskbarNotification

Class Application

    Private TrayIcon As TaskbarIcon

    Public ReadOnly Property MyMainWindow As WisemanWindowsClient.MainWindow
        Get
            Return MainWindow
        End Get
    End Property

#Region "Settings"

    Public Settings As WisemanClientSettings

    Public Sub LoadSettings()
        Dim fileName As String = String.Format("{0}\wiseman.settings", Environment.CurrentDirectory)
        If File.Exists(fileName) Then
            Settings = WisemanClientSettings.LoadFromFile(fileName)
        Else
            Settings = New WisemanClientSettings()
            Settings.FilePath = fileName
        End If
    End Sub

    Public Sub SaveSettings()
        Settings.SaveToFile()
    End Sub

#End Region

#Region "Themes"

    Private _ThemeDictionary As ResourceDictionary

    Public Sub LoadTheme(themeName As String)
        Dim fileName As String = String.Format("{0}\Themes\{1}.xaml", Environment.CurrentDirectory, themeName)
        If File.Exists(fileName) Then
            Using fs As FileStream = New FileStream(fileName, FileMode.Open)
                If _ThemeDictionary IsNot Nothing Then
                    Resources.MergedDictionaries.Remove(_ThemeDictionary)
                End If
                _ThemeDictionary = CType(XamlReader.Load(fs), ResourceDictionary)
                Resources.MergedDictionaries.Add(_ThemeDictionary)
            End Using
        Else
            MessageBox.Show("File: " + fileName + " does not exist. Please re-enter.")
        End If
    End Sub

#End Region

#Region "Public app methods"

    Public Sub ShowMainWindow()
        If (Me.MainWindow) Is Nothing Then
            Me.MainWindow = New WisemanWindowsClient.MainWindow()
            Me.MainWindow.Show()
        End If
    End Sub

    Public Sub TestBalloon()
        TrayIcon.ShowBalloonTip("Hello World!", "This is my first balloon.", Me.TrayIcon.Icon)
    End Sub

    Public Sub TestCustomBalloon()
        Dim balloon As Balloon = New Balloon()
        TrayIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, 4000)
    End Sub

#End Region

#Region "Scheduler"

    Public WithEvents Scheduler As WisemanScheduler

    Public Sub InitializeScheduler(schedule As WisemanSchedule)
        If TypeOf (Scheduler) Is WisemanScheduler Then
            Scheduler.SchedulingChanged(schedule)
        Else
            Scheduler = New WisemanScheduler(schedule)
        End If
    End Sub

    Private Sub SchedulerEventTriggered()
        If MyMainWindow IsNot Nothing AndAlso MyMainWindow.IsLoaded Then
            MyMainWindow.LoadRandomQuote()
        Else
            ShowMainWindow()
        End If
    End Sub

    Public Delegate Sub NextPrimeDelegate()

    Private Sub OnSchedulerEvent() Handles Scheduler.ItIsTime
        Dispatcher.BeginInvoke(New NextPrimeDelegate(AddressOf SchedulerEventTriggered), DispatcherPriority.ContextIdle, Nothing)
    End Sub

#End Region

#Region "App level events"

    Private Sub App_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        LoadSettings()
        LoadTheme(Settings.ThemeName)
        TrayIcon = FindResource("NotifyIcon")
    End Sub

    Private Sub App_DispatcherUnhandledException(sender As Object, e As DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        MessageBox.Show(e.Exception.Message)
    End Sub

    Private Sub App_Exit() Handles Me.Exit
        SaveSettings()
        TrayIcon.Dispose()
    End Sub

#End Region

End Class
