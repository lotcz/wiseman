Public Class JSONObject
    Inherits Dictionary(Of String, JSONObject)

    Public Property Type As JSONTypeEnum = JSONTypeEnum.JSONString

    Public Property Value As Object = Nothing

    Public Property IsObject As Boolean
        Get
            Return Type = JSONTypeEnum.JSONObject
        End Get
        Set(value As Boolean)
            If value Then
                Type = JSONTypeEnum.JSONObject
            End If
        End Set
    End Property

    Public Property IsString As Boolean
        Get
            Return Type = JSONTypeEnum.JSONString
        End Get
        Set(value As Boolean)
            If value Then
                Type = JSONTypeEnum.JSONString
            End If
        End Set
    End Property

    Public Property IsList As Boolean
        Get
            Return Type = JSONTypeEnum.JSONList
        End Get
        Set(value As Boolean)
            If value Then
                Type = JSONTypeEnum.JSONList
            End If
        End Set
    End Property

    Shared Function Parse(ByRef jsonStr As String) As JSONObject
        Return JSONParser.Parse(jsonStr)
    End Function

    Public Shadows Function ToString() As String
        Return Value
    End Function

End Class
