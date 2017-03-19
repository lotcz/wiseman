Public Class FakeHeaderControl

    Public Shared ReadOnly TitleProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(String), GetType(FakeHeaderControl), New FrameworkPropertyMetadata(Nothing))

    Public Property Title As String
        Get
            Return GetValue(TitleProperty)
        End Get
        Set(ByVal value As String)
            SetValue(TitleProperty, value)
        End Set
    End Property

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
