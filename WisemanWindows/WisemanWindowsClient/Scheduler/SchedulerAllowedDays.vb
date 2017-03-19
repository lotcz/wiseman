Public Class SchedulerAllowedDays

    Public Value As Byte

    Public Sub New(Optional DaysValue As Byte = 0)
            Value = DaysValue
        End Sub

    Private _Days As Boolean()

    Public ReadOnly Property Days As Boolean()
            Get
                If _Days Is Nothing Then
                    _Days = DecodeDays(Value)
                End If
                Return _Days
            End Get
        End Property

        ''' <summary>
        ''' return True if given day is allowed. 0 - Monday, 6 - Sunday
        ''' </summary>
        Public Function IsDayAllowed(day As Byte) As Boolean
            Return Days(day)
        End Function

        ''' <summary>
        ''' return True if given day is allowed
        ''' </summary>
        Public Function IsDayOfWeekAllowed(day As System.DayOfWeek) As Boolean
            Dim b As Byte
            If day = DayOfWeek.Sunday Then
                b = 6
            Else
                b = Byte.Parse(day) - 1
            End If
            Return IsDayAllowed(b)
        End Function

        Public Function IsActive() As Boolean
            Return Value <> 0 And Value <> 127
        End Function

        Public Function GetDescription() As String
            Dim s As String = ""
            If IsActive() Then
                Dim DaysOfWeek As Array
                Dim allowedDays As New List(Of String)
                DaysOfWeek = System.Enum.GetValues(GetType(System.DayOfWeek))
                For Each d As System.DayOfWeek In DaysOfWeek
                    If IsDayOfWeekAllowed(d) Then
                        allowedDays.Add(d.ToString())
                    End If
                Next
            s = "V " + String.Join(", ", allowedDays.ToArray())
        Else
            s = "Každý den"
        End If
            Return s
        End Function

        Shared Function DecodeDays(ByVal value As Byte) As Boolean()
            Dim days(6) As Boolean
            Dim i As Byte = 0

            While value > 0
                If value Mod 2 > 0 Then
                    value -= 1
                    days(i) = True
                Else
                    days(i) = False
                End If
                value /= 2
                i += 1
            End While

            Return days
        End Function

        Shared Function EncodeDays(days As Boolean()) As Byte
            Dim b As Byte = 0
            For i As Byte = 0 To 6
                If days(i) Then
                    b += Math.Pow(2, i)
                End If
            Next
            Return b
        End Function

End Class
