Imports FFACETools
Module Objects

    Public Enum Gates
        LessThan
        GreaterThan
        Equals
        NotEquals
    End Enum

    Public Enum Targets
        self
        target
        party
    End Enum

    Public Enum Triggers
        HP
        HPP
        MP
        MPP
        TP
        NAME
        STATUS
        DISTANCE
        EFFECT
        ASSIST
    End Enum

    Public Enum Reactions
        ATTACK
        SPELL
        JOB_ABILITY
        WEAPONSKILL
        MACRO
        INPUT
        KEYPRESS
        KEYDOWN
        KEYUP
        TRACK
        FIND
    End Enum

    Public Class Trigger
        Public Gate As Gates
        Public Target As Targets
        Public Type As Triggers
        Public Arg As String = ""

        Public Sub New(ByVal _Target As String, ByVal _Gate As String, ByVal _Type As String, ByVal _arg As String)
            Arg = _arg

            Select Case _Gate.ToLower
                Case "less"
                    Gate = Gates.LessThan
                Case "greater"
                    Gate = Gates.GreaterThan
                Case "equals"
                    Gate = Gates.Equals
                Case "notequals"
                    Gate = Gates.NotEquals
            End Select

            Select Case _Target.ToLower
                Case "party"
                    Target = Targets.party
                Case "self"
                    Target = Targets.self
                Case "target"
                    Target = Targets.target
            End Select

            Select Case _Type.ToLower
                Case "distance"
                    Type = Triggers.DISTANCE
                Case "effect"
                    Type = Triggers.EFFECT
                Case "hp"
                    Type = Triggers.HP
                Case "hpp"
                    Type = Triggers.HPP
                Case "mp"
                    Type = Triggers.MP
                Case "mpp"
                    Type = Triggers.MPP
                Case "name"
                    Type = Triggers.NAME
                Case "tp"
                    Type = Triggers.TP
                Case "status"
                    Type = Triggers.STATUS
                Case "assist"
                    Type = Triggers.ASSIST
            End Select
        End Sub
    End Class

    Public Class Reaction
        Public Type As Reactions
        Public Arg As String = ""

        Public Sub New(ByVal _Type As String, ByVal _argument As String)

            Arg = _argument

            Select Case _Type.ToLower
                Case "attack"
                    Type = Reactions.ATTACK
                Case "input"
                    Type = Reactions.INPUT
                Case "ability"
                    Type = Reactions.JOB_ABILITY
                Case "spell"
                    Type = Reactions.SPELL
                Case "weaponskill"
                    Type = Reactions.WEAPONSKILL
                Case "macro"
                    Type = Reactions.MACRO
                Case "keydown"
                    Type = Reactions.KEYDOWN
                Case "keypress"
                    Type = Reactions.KEYPRESS
                Case "keyup"
                    Type = Reactions.KEYUP
                Case "track"
                    Type = Reactions.TRACK
                Case "find"
                    Type = Reactions.FIND
            End Select
        End Sub
    End Class

    Public Class Gambit
        Public triggers As New List(Of Trigger)
        Public triggerGate As String
        Public NotGate As Boolean = False
        Public reaction As Reaction

        Public Sub New(ByVal LogicString As String)
            If LogicString.ToLower.Contains("and") Then
                triggerGate = "AND"
            ElseIf LogicString.ToLower.Contains("or") Then
                triggerGate = "OR"
            End If
            If LogicString.ToLower.Contains("not") Then NotGate = True
        End Sub
    End Class

    Public Class character
        Public Leader As Boolean = True
        Public gambits As New List(Of Gambit)
        Public Name As String = ""
        Public INSTANCE As FFACETools.FFACE = Nothing

        Public Sub New(ByVal _Name As String, ByVal _Leader As Boolean)
            Leader = _Leader
            Name = _Name
        End Sub

    End Class


End Module
