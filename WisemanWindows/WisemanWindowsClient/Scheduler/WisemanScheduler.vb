Imports System.Timers

Public Class WisemanScheduler

    Public Event ItIsTime()

    Private Property Schedule As WisemanSchedule

    Private WithEvents SchedulerTimer As Timer

    Public Sub New(defaultSchedule As WisemanSchedule)
        Schedule = defaultSchedule
        Reload()
    End Sub

    Public Sub SchedulingChanged(newSchedule As WisemanSchedule)
        Schedule = newSchedule
        Reload()
    End Sub

    Public Sub StopScheduler()
        StopTimer()
    End Sub

    Private Sub StopTimer()
        If TypeOf (SchedulerTimer) Is Timer Then
            SchedulerTimer.Stop()
        End If
        SchedulerTimer = Nothing
    End Sub

    Private Sub Reload()
        StopTimer()
        ScheduleNextEvent()
    End Sub

    Private Sub ScheduleNextEvent()
        StopTimer()

        Dim interval As TimeSpan = Schedule.NextScheduledDateTime.Value.Subtract(DateTime.Now)
        If interval.TotalSeconds > 0 Then
            If Not TypeOf (SchedulerTimer) Is Timer Then
                SchedulerTimer = New Timer(interval.TotalMilliseconds)
            End If
            SchedulerTimer.Start()
        Else
            'next run time is in the past, run immediately
            TriggerTheEvent()
        End If

    End Sub

    Private Sub TimerTick(sender As Object, e As ElapsedEventArgs) Handles SchedulerTimer.Elapsed
        StopTimer()
        TriggerTheEvent()
    End Sub

    Private Sub TriggerTheEvent()
        If Schedule.ScheduleType = WisemanScheduleTypeEnum.OneTime Then
            Schedule.ScheduleEnabled = False
        End If

        Schedule.ScheduleLastRun = DateTime.Now
        Schedule.ResetSchedule()

        RaiseEvent ItIsTime()

        Reload()
    End Sub

End Class
