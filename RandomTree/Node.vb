
Public Enum Sign
    Add
    Mul
End Enum

Public Interface INode

End Interface

Public Class Binary
    Implements INode

    Public ReadOnly Sign As Sign
    Public Left, Right As INode

    Public Sub New(Sign As Sign)
        Me.Sign = Sign
    End Sub

    Public Overrides Function ToString() As String
        Return Left.ToString & SerializeSign() & Right.ToString
    End Function

    Private Function SerializeSign() As String
        Select Case Sign
            Case Sign.Add : Return "+"
            Case Sign.Mul : Return "*"
            Case Else : Throw New Exception("WTF?")
        End Select
    End Function

End Class

Public Class Leaf
    Implements INode

    Public ReadOnly Value As Integer

    Public Sub New(Value As Integer)
        Me.Value = Value
    End Sub

    Public Overrides Function ToString() As String
        Return Value.ToString
    End Function

End Class