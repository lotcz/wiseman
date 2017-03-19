Public Class StringHelper

    Public Enum StringAlignment
        Left
        Right
    End Enum

    ''' <summary>
    ''' Return a string of fised length filled with one single character.
    ''' </summary>
    ''' <param name="Chr">character to use</param>
    ''' <param name="Length">length of returned string</param>
    ''' <remarks>If you call for example StringOfCharacter("0",4) returned value will be "0000".</remarks>
    Public Shared Function StringOfCharacter(ByVal Chr As Char, ByVal Length As Integer) As String
        Dim sStr As String = String.Empty

        While sStr.Length < Length
            sStr += Chr
        End While

        Return sStr
    End Function

    ''' <summary>
    ''' Returns portion of specified string. Always returns string value even if index and length are out of bounds or input text is Nothing
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="index">zero-based position of your substring's first character</param>
    ''' <param name="length"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Substring(ByVal text As String, ByVal index As Integer, Optional ByVal length As Integer = -1) As String
        If text Is Nothing Then
            text = String.Empty
        End If

        If index < 0 Then
            index = 0
        End If

        If length < 0 Then
            length = text.Length - index
        End If

        If (text.Length >= index) Then
            If (text.Length < (index + length)) Then
                length = text.Length - index
            End If
            Return text.Substring(index, length)
        Else
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Returns string of defined length. Longer string is truncated, shorter string is added leading or trailing characters depending on defined Alignment.
    ''' </summary>
    ''' <param name="Text">String that you want to process.</param>
    ''' <param name="Length">Length you want the string to be formatted to</param>
    ''' <param name="Alignment">If Left, trailing characters are put to the end of the string. If Right, leading characters are put to beginning of the string.</param>
    ''' <param name="FillingChar">Character to fill remaining space with.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FixedLength(ByVal Text As String, ByVal Length As Integer, Optional ByVal Alignment As StringAlignment = StringAlignment.Left, Optional ByVal FillingChar As Char = " ") As String
        Text = Substring(Text, 0, Length)

        If Text.Length < Length Then
            If Alignment = StringAlignment.Left Then
                Text = Text + StringOfCharacter(FillingChar, Length - Text.Length)
            Else
                Text = StringOfCharacter(FillingChar, Length - Text.Length) + Text
            End If
        End If

        Return Text
    End Function

    ''' <summary>
    ''' Returns string of fixed length containing number prefixed with zeros to fit desired size. Decimal separators are removed.
    ''' </summary>
    ''' <param name="Number"></param>
    ''' <param name="Length"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function FixedLengthNumber(ByVal Number As Object, ByVal Length As Integer) As String
        Return FixedLength(Number.ToString().Replace(",", "").Replace(".", ""), Length, StringAlignment.Right, "0")
    End Function

#Region "Format date and time"

    Public Shared Function FormatTime(ts As TimeSpan) As String
        If ts.Days > 0 Then
            Return String.Format("{0}d {1}:{2}:{3}", ts.Days, FixedLengthNumber(ts.Hours, 2), FixedLengthNumber(ts.Minutes, 2), FixedLengthNumber(ts.Seconds, 2))
        Else
            Return String.Format("{0}:{1}:{2}", FixedLengthNumber(ts.Hours, 2), FixedLengthNumber(ts.Minutes, 2), FixedLengthNumber(ts.Seconds, 2))
        End If
    End Function

    Public Shared Function FormatTimeForHumanBeings(ts As TimeSpan) As String
        Dim s As String = ""
        If ts.Hours > 0 Then
            s += String.Format("{0} hours, ", ts.Hours)
        End If
        If ts.Minutes > 0 Then
            s += String.Format("{0} minutes, ", ts.Minutes)
        End If
        If ts.Seconds > 0 Then
            s += String.Format("{0} seconds, ", ts.Seconds)
        End If
        If s.Length > 0 Then
            s = s.Substring(0, s.Length - 2)
        End If
        Return s
    End Function

#End Region

End Class
