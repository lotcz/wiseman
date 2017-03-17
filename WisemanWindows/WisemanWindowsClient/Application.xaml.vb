Imports System.Windows.Markup
Imports System.IO

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

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

    Private Sub App_Startup() Handles Me.Startup
        LoadTheme("MyTheme")
    End Sub

End Class
