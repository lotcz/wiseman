Imports System.Windows.Markup
Imports System.IO
Imports System.Windows.Threading

Class Application

#Region "Public app properties"

    Public Property Settings As WisemanClientSettings

#End Region

#Region "Public app methods"

    Public Sub LoadSettings()
        Dim fileName As String = String.Format("{0}\Settings.xml", Environment.CurrentDirectory)
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

    Public Sub LoadTheme(themeName As String)
        Dim fileName As String = String.Format("{0}\Themes\{1}.xaml", Environment.CurrentDirectory, themeName)
        If File.Exists(fileName) Then
            Using fs As FileStream = New FileStream(fileName, FileMode.Open)
                Dim dic As ResourceDictionary = Nothing
                dic = CType(XamlReader.Load(fs), ResourceDictionary)
                Resources.MergedDictionaries.Clear()
                Resources.MergedDictionaries.Add(dic)
            End Using
        Else
            MessageBox.Show("File: " + fileName + " does not exist. Please re-enter.")
        End If
    End Sub

#End Region

#Region "App level events"

    Private Sub App_Startup() Handles Me.Startup
        LoadSettings()
        LoadTheme(Settings.ThemeName)
    End Sub

    Private Sub App_DispatcherUnhandledException(sender As Object, e As DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        MessageBox.Show(e.Exception.Message)
    End Sub

    Private Sub App_Exit() Handles Me.Exit
        SaveSettings()
    End Sub

#End Region

End Class
