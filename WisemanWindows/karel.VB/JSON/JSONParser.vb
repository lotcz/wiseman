Public Class JSONParser

    Private Property jsonStr As String

    Private Property position As Integer = 0

    Public Sub New(JSONString As String)
        Me.jsonStr = JSONString
    End Sub

    Shared Function Parse(ByVal jsonStr As String) As JSONObject
        If jsonStr IsNot Nothing AndAlso jsonStr.Length > 0 Then
            Dim parser As New JSONParser(jsonStr)
            Return parser.Parse()
        Else
            Return Nothing
        End If
    End Function

    Public Function Parse() As JSONObject
        If jsonStr IsNot Nothing AndAlso jsonStr.Length > position Then
            Dim chr As Char = jsonStr(position)

            If chr = "{" Then
                position += 1
                Return ParseObject()
            ElseIf chr = "[" Then
                position += 1
                'jsonObj.IsList = True
            ElseIf chr = "'" Or chr = """" Then
                position += 1
                Return ParseString()
            Else
                Return ParseNumber()
            End If
        Else
            Return Nothing
        End If
    End Function

    Private Function ParseObject() As JSONObject
        Dim jsonObj As New JSONObject()
        jsonObj.IsObject = True

        If jsonStr IsNot Nothing AndAlso jsonStr.Length > position Then
            Dim name As String = String.Empty
            Dim value As JSONObject
            Dim chr As Char

            Do While jsonStr.Length > position
                chr = jsonStr(position)
                If chr = "}" Then
                    position += 1
                    Return jsonObj
                Else
                    name = ParseName()
                    value = Parse()
                    jsonObj(name) = value
                    position += 1
                End If
            Loop
        End If

        Return jsonObj
    End Function

    Private Function ParseName() As String
        If jsonStr IsNot Nothing AndAlso jsonStr.Length > position Then
            Dim str As String = String.Empty
            Dim chr As Char

            Do While jsonStr.Length > position
                chr = jsonStr(position)
                position += 1
                If chr = ":" Then
                    Return str
                ElseIf Not chr = """" Then
                    str += chr
                End If
            Loop
        Else
            'ERROR
        End If
    End Function

    Private Function ParseString() As JSONObject
        If jsonStr IsNot Nothing AndAlso jsonStr.Length > position Then
            Dim jsonObj As New JSONObject()
            jsonObj.IsString = True
            Dim str As String = String.Empty
            Dim chr As Char

            Do While jsonStr.Length > position
                chr = jsonStr(position)
                position += 1
                If chr = "'" Or chr = """" Then
                    jsonObj.Value = str
                    Return jsonObj
                ElseIf chr = "\" Then
                    str += ParseEscaped()
                Else
                    str += chr
                End If
            Loop
        Else
            Return Nothing
        End If
    End Function

    Private Function ParseNumber() As JSONObject
        If jsonStr IsNot Nothing AndAlso jsonStr.Length > position Then
            Dim jsonObj As New JSONObject()
            jsonObj.IsString = True
            Dim str As String = String.Empty
            Dim chr As Char

            Do While jsonStr.Length > position
                chr = jsonStr(position)
                position += 1
                If chr = " " Or chr = "," Then
                    jsonObj.Value = str
                    Return jsonObj
                Else
                    str += chr
                End If
            Loop
        Else
            Return Nothing
        End If
    End Function

    Private Function ParseEscaped() As String
        If jsonStr IsNot Nothing AndAlso jsonStr.Length > position Then
            Dim str As String = String.Empty
            Dim chr As Char

            Do While jsonStr.Length > position
                chr = jsonStr(position)
                position += 1
                If chr = "u" Then
                    Return ParseUTF()
                Else
                    Return chr
                End If
            Loop
        Else
            Return Nothing
        End If
    End Function

    Private Function ParseUTF() As Char
        If jsonStr IsNot Nothing AndAlso jsonStr.Length > (position + 4) Then
            position += 4
            Return Char.ConvertFromUtf32(Convert.ToUInt32(jsonStr.Substring(position - 4, 4), 16))
        Else
            Return Nothing
        End If
    End Function

End Class
