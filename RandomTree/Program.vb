
Module Program

    Sub Main(args As String())
        Dim N As INode
        For i As Integer = 1 To 50
            N = New Generator(5).Result
            Validate(N, False)
            Console.WriteLine(N)
        Next
    End Sub

    Private Sub Validate(N As INode, HasMul As Boolean)
        If TypeOf N Is Binary Then
            With DirectCast(N, Binary)
                If HasMul AndAlso .Sign = Sign.Add Then Throw New Exception("Invalid")
                Validate(.Left, HasMul OrElse .Sign = Sign.Mul)
                Validate(.Right, HasMul OrElse .Sign = Sign.Mul)
            End With
        End If
    End Sub

End Module
