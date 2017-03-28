Imports Wiseman

<Serializable>
Public Class WisemanClientSettings
    Inherits WisemanSettings

    Public ThemeName As String = "Cimrman"

    Public Schedule As WisemanSimpleSchedulesEnum = WisemanSimpleSchedulesEnum.Daily

    Public LastDisplayTime As Nullable(Of DateTime) = Nothing

End Class
