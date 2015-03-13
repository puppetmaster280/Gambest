Imports Gambest.Commands
Imports Gambest.Objects
Imports System.Xml
Imports FFACETools
Module Main
    Private Delay As Integer = 0
    Sub Main()
        Dim processes As Process() = Process.GetProcessesByName("pol")
        If processes.Length = 0 Then
            OutError(0, "Please make sure ffxi is running and open.", True)
        End If

        Dim filepath As String = My.Application.Info.DirectoryPath() + "\main.xml"
        Dim Args As String() = Environment.GetCommandLineArgs()

        If Args.Length > 1 Then  'User loaded a file, 
            filepath = Args(1)   'use that instead of main.xml
        End If

        If Not My.Computer.FileSystem.FileExists(filepath) Then 'File couldn't be found.
            If Not My.Computer.FileSystem.FileExists(filepath) Then
                OutError(0, "xml not found.", True)
            End If
        End If

        Dim CharList As List(Of character) = Parse(filepath, Delay)



        For Each character As character In CharList
            For Each ffxi As Process In processes
                If ffxi.MainWindowTitle = character.Name Then
                    Dim instance As New FFACE(ffxi.Id)
                    character.INSTANCE = New FFACE(ffxi.Id)
                End If
            Next
        Next

    End Sub

End Module
