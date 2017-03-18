Imports System.Runtime.Serialization
Imports System.IO

<Serializable()>
Public Class SerializableSettings

    <NonSerialized()>
    Public FilePath As String

    Shared Function LoadFromFile(path As String) As SerializableSettings
        Dim formatter As IFormatter = New Formatters.Binary.BinaryFormatter()
        Dim stream As Stream = New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim settings As SerializableSettings = formatter.Deserialize(stream)
        stream.Close()
        settings.FilePath = path
        Return settings
    End Function

    Public Sub SaveToFile()
        Dim formatter As IFormatter = New Formatters.Binary.BinaryFormatter()
        Dim stream As Stream = New FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None)
        formatter.Serialize(stream, Me)
        stream.Close()
    End Sub

End Class
