Public Class FakeHeaderControl

    Public Event OnCloseRequested()

    Public Event OnDragMoveRequested()

    Private Sub FakeWindowHead_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles FakeWindowHead.MouseDown
        If (e.ChangedButton = MouseButton.Left) Then
            RaiseEvent OnDragMoveRequested()
        End If
    End Sub

    Private Sub FakeWindowClose_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles FakeWindowClose.MouseUp
        If (e.ChangedButton = MouseButton.Left) Then
            RaiseEvent OnCloseRequested()
        End If
    End Sub

End Class
