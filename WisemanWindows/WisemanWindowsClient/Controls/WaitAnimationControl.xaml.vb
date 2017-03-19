Imports System.Windows.Media.Animation

Public Class WaitAnimationControl

    Public InitiallyHidden As Boolean = True
    Private myStory As Storyboard

    Private Sub Control_Loaded() Handles Me.Initialized
        myStory = Me.FindResource("myStory")
        If InitiallyHidden Then
            Hide()
        Else
            Show()
        End If
    End Sub

    Public Sub Show()
        Me.Visibility = Visibility.Visible
        myStory.Begin()
    End Sub

    Public Sub Hide()
        Me.Visibility = Visibility.Collapsed
        myStory.Stop()
    End Sub

End Class
