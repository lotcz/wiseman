Imports System.Windows.Markup
Imports System.IO
Imports System.Windows.Threading
Imports System.Windows.Controls.Primitives
Imports Hardcodet.Wpf.TaskbarNotification

Class Application

    Private TrayIcon As TaskbarIcon

    Private CurrentQuote As Quote

    Public Client As WisemanClient

    Public ReadOnly Property MyMainWindow As WisemanWindowsClient.MainWindow
        Get
            If TypeOf (MainWindow) Is WisemanWindowsClient.MainWindow Then
                Return MainWindow
            Else
                Return Nothing
            End If
        End Get
    End Property

#Region "Settings"

    Public Settings As WisemanClientSettings

    Public Sub LoadSettings()
        Dim fileName As String = String.Format("{0}\wiseman.settings", Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData))
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
            'Using fs As FileStream = New FileStream(fileName, FileMode.Open)
            Using fs As FileStream = File.OpenRead(fileName)
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
        StopScheduler()
        If TypeOf MyMainWindow Is WisemanWindowsClient.MainWindow AndAlso MyMainWindow.IsLoaded Then
            MyMainWindow.BringIntoView()
        Else
            Me.MainWindow = New WisemanWindowsClient.MainWindow()
            If TypeOf (CurrentQuote) Is Quote Then
                MyMainWindow.DisplayQuote(CurrentQuote)
            End If
            MyMainWindow.Show()
        End If
        HideQuoteBalloon()
    End Sub

    Public Sub HideMainWindow()
        If TypeOf MyMainWindow Is WisemanWindowsClient.MainWindow AndAlso MyMainWindow.IsLoaded Then
            MyMainWindow.Close()
        End If
        Me.MainWindow = Nothing
    End Sub

    Private Sub DisplayQuoteBalloon(q As Quote)
        Dim balloon As QuoteBalloon = New QuoteBalloon(q)
        TrayIcon.ShowCustomBalloon(balloon, PopupAnimation.Slide, Nothing)
    End Sub

    Public Sub HideQuoteBalloon()
        TrayIcon.CloseBalloon()
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

    Public Sub InitializeSchedulerInTestMode()
        ' initialize scheduler to run every 20 seconds
        Dim schedule As New WisemanSchedule()
        schedule.ScheduleType = WisemanScheduleTypeEnum.Periodically
        schedule.ScheduleDays = New SchedulerAllowedDays(0)
        schedule.ScheduleTime = New DateTime(2017, 1, 1, 0, 0, 20)
        InitializeScheduler(schedule)
    End Sub

    Public Sub StopScheduler()
        If TypeOf (Scheduler) Is WisemanScheduler Then
            Scheduler.StopScheduler()
        End If
    End Sub

    Private Async Sub LoadRandomQuote()
        Try
            CurrentQuote = Await Client.FetchRandomQuote()
            DisplayQuoteBalloon(CurrentQuote)
        Catch e As Exception
            CurrentQuote = Nothing
        End Try
    End Sub

    Private Sub SchedulerEventTriggered()
        LoadRandomQuote()
    End Sub

    Public Delegate Sub SchedulerEventTriggeredDelegate()

    Private Sub OnSchedulerEvent() Handles Scheduler.ItIsTime
        Dispatcher.BeginInvoke(New SchedulerEventTriggeredDelegate(AddressOf SchedulerEventTriggered), DispatcherPriority.ContextIdle, Nothing)
    End Sub

#End Region

#Region "App level events"

    Private Sub App_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
        LoadSettings()
        LoadTheme(Settings.ThemeName)
        TrayIcon = FindResource("NotifyIcon")
        Client = New WisemanClient()
        LoadRandomQuote()
        InitializeSchedulerInTestMode()
    End Sub

    Private Sub App_DispatcherUnhandledException(sender As Object, e As DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        MessageBox.Show(e.Exception.Message)
    End Sub

    Private Sub App_Exit() Handles Me.Exit
        HideMainWindow()
        SaveSettings()
        HideQuoteBalloon()
        TrayIcon.Dispose()
    End Sub

#End Region

End Class
