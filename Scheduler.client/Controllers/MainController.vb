Public Class MainController

    Private mCurrentLine As core.Line

    Public Property CurrentLine As core.Line
        Get
            Return mCurrentLine
        End Get
        Set(value As core.Line)
            mCurrentLine = value
        End Set
    End Property

    Public Property Treenodes() As TreeNode()
    Public Property CurrentPlan As core.PlanItem

    Public Sub UpdateLineNodes()

    End Sub

    Public Sub ValidatePlan()

    End Sub

    Public Sub CommitThePlan()

    End Sub


    Public Function getPlan() As List(Of core.PlanItem)
        'use current line
    End Function


    Public Function SavePlan() As List(Of core.PlanItem)
        'use cuurrent line

    End Function




End Class
