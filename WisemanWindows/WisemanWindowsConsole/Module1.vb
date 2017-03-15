Imports Wiseman

Module Module1

    Private Property client As New WisemanClient(New Wiseman.WisemanSettings())

    Sub Main()
        Dim ki As ConsoleKeyInfo
        Do
            GetRandomQuote()
            ki = Console.ReadKey()
        Loop While Not ki.Key = ConsoleKey.Escape
    End Sub

    Private Sub GetRandomQuote()
        Dim ts As Task(Of Quote) = client.FetchRandomQuote()
        Dim q As Quote = ts.Result
        Console.WriteLine(q.Text)
        Console.WriteLine(String.Format("autor: {0}, zdroj: {1}", q.Author, q.Source))
    End Sub

End Module
