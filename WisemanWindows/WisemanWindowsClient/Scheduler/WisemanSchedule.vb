Public Enum WisemanScheduleTypeEnum
    AtCertainTime
    Periodically
    OneTime
End Enum

Public Class WisemanSchedule

    Public ScheduleEnabled As Boolean = True

    Public ScheduleType As WisemanScheduleTypeEnum = WisemanScheduleTypeEnum.AtCertainTime

    Public ScheduleTime As DateTime

    Public ScheduleDays As SchedulerAllowedDays

    Public ScheduleLastRun As Nullable(Of DateTime)

    Shared Function GetScheduleDescription(ScheduleType As WisemanScheduleTypeEnum, ScheduleTime As DateTime, ScheduleDays As SchedulerAllowedDays)
        Dim d As String = ScheduleDays.GetDescription()

        Dim s As String = ""
        Select Case ScheduleType
            Case WisemanScheduleTypeEnum.AtCertainTime
                s += String.Format("At {0}. {1}.", StringHelper.FormatTime(ScheduleTime.TimeOfDay), d)
            Case WisemanScheduleTypeEnum.Periodically
                s += String.Format("Every {0}. {1}.", StringHelper.FormatTimeForHumanBeings(ScheduleTime.TimeOfDay), d)
            Case WisemanScheduleTypeEnum.OneTime
                s += String.Format("One time on {0}.", ScheduleTime.ToString())
        End Select

        Return s
    End Function

    Public ReadOnly Property ScheduleDescription As String
        Get
            Return GetScheduleDescription(ScheduleType, ScheduleTime, ScheduleDays)
        End Get
    End Property

    Private Property _NextScheduledDateTime As Nullable(Of DateTime) = Nothing

    Public ReadOnly Property NextScheduledDateTime As Nullable(Of DateTime)
        Get
            If Not _NextScheduledDateTime.HasValue Then
                _NextScheduledDateTime = GetNextScheduledDateTime()
            End If
            Return _NextScheduledDateTime
        End Get
    End Property

    Public Function GetNextScheduledDateTime() As Nullable(Of DateTime)
        Dim NextDateTime As Nullable(Of DateTime) = Nothing
        Dim Now As DateTime = DateTime.Now

        If ScheduleEnabled Then
            Select Case ScheduleType
                Case WisemanScheduleTypeEnum.AtCertainTime
                    NextDateTime = New DateTime(Now.Year, Now.Month, Now.Day, ScheduleTime.Hour, ScheduleTime.Minute, ScheduleTime.Second)
                    If (ScheduleTime.TimeOfDay <= Now.TimeOfDay) Then
                        NextDateTime = NextDateTime.Value.AddDays(1)
                    End If
                    'if days are specified, loop until schedule is on allowed day
                    If ScheduleDays.IsActive Then
                        While Not ScheduleDays.IsDayOfWeekAllowed(NextDateTime.Value.DayOfWeek)
                            NextDateTime = NextDateTime.Value.AddDays(1)
                        End While
                    End If
                Case WisemanScheduleTypeEnum.Periodically
                    If ScheduleLastRun.HasValue Then
                        NextDateTime = ScheduleLastRun.Value.Add(ScheduleTime.TimeOfDay)
                    Else
                        NextDateTime = Now
                    End If
                    'if days are specified, set time to midnight and loop until schedule is on allowed day
                    If ScheduleDays.IsActive AndAlso Not ScheduleDays.IsDayOfWeekAllowed(NextDateTime.Value.DayOfWeek) Then
                        NextDateTime.Value.Subtract(NextDateTime.Value.TimeOfDay)
                        While Not ScheduleDays.IsDayOfWeekAllowed(NextDateTime.Value.DayOfWeek)
                            NextDateTime = NextDateTime.Value.AddDays(1)
                        End While
                    End If
                Case WisemanScheduleTypeEnum.OneTime
                    NextDateTime = ScheduleTime
            End Select
        End If

        Return NextDateTime
    End Function

    Public Sub ResetSchedule()
        _NextScheduledDateTime = GetNextScheduledDateTime()
    End Sub

End Class