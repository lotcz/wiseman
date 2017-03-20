Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.IO
Imports System.Reflection

Public Class CustomInstaller

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        AddHandler Me.Committed, AddressOf MyInstaller_Committed
    End Sub

    ' Event handler for 'Committed' event.
    Private Sub MyInstaller_Committed(ByVal sender As Object, ByVal e As InstallEventArgs)
        Try
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\WisemanWindowsClient.exe")
        Catch
            ' Do nothing... 
        End Try
    End Sub


End Class
