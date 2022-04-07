
Public Class Generator

    Private ReadOnly fRnd As New Random

    Public ReadOnly Property Result As INode

    Public Sub New(Size As Integer)
        Result = If(Size = 0, CreateLeaf(), Generate(Size))
    End Sub

    Private Function Generate(Size As Integer) As INode
        Dim All, CanUse As New HashSet(Of Binary), N, Root As Binary
        For i As Integer = 1 To Size
            All.Add(New Binary(If(fRnd.Next(2) = 0, Sign.Add, Sign.Mul)))
        Next
        Root = If(All.Where(Function(X) X.Sign = Sign.Add).FirstOrDefault, All.First)
        CanUse.Add(Root)
        For Each N In All.Where(Function(X) X IsNot Root AndAlso X.Sign = Sign.Add)
            Assign(N, CanUse)
        Next
        For Each N In All.Where(Function(X) X IsNot Root AndAlso X.Sign = Sign.Mul)
            Assign(N, CanUse)
        Next
        Return FillLeaves(Root)
    End Function

    Private Sub Assign(N As INode, CanUse As HashSet(Of Binary))
        Dim Parent As Binary = CanUse(fRnd.Next(CanUse.Count))
        If Parent.Left Is Nothing AndAlso Parent.Right Is Nothing Then
            If fRnd.Next(2) = 0 Then
                Parent.Left = N
            Else
                Parent.Right = N
            End If
        Else
            If Parent.Left Is Nothing Then
                Parent.Left = N
            Else
                Parent.Right = N
            End If
            CanUse.Remove(Parent)
        End If
        CanUse.Add(N)
    End Sub

    Private Function FillLeaves(N As Binary) As INode
        If N.Left Is Nothing Then
            N.Left = CreateLeaf()
        Else
            FillLeaves(DirectCast(N.Left, Binary))
        End If
        If N.Right Is Nothing Then
            N.Right = CreateLeaf()
        Else
            FillLeaves(DirectCast(N.Right, Binary))
        End If
        Return N
    End Function

    Private Function CreateLeaf() As Leaf
        Return New Leaf(fRnd.Next(10))
    End Function

End Class
