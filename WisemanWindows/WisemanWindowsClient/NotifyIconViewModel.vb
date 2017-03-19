Public Class NotifyIconViewModel

    Public ReadOnly Property ShowCommand As ICommand
        Get
            Return New ShowMainWindowCommand()
        End Get
    End Property

    Public ReadOnly Property ShutdownCommand As ICommand
        Get
            Return New ShutdownApplicationCommand()
        End Get
    End Property

    Public Class ShowMainWindowCommand
        Implements ICommand

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            Dim WisemanApplication As WisemanWindowsClient.Application = Application.Current
            WisemanApplication.ShowMainWindow()
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    End Class

    Public Class ShutdownApplicationCommand
        Implements ICommand

        Public CommandAction As Action

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            Dim WisemanApplication As WisemanWindowsClient.Application = Application.Current
            WisemanApplication.Shutdown()
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return True
        End Function

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

    End Class

End Class