Imports Gambest.Commands
Imports Gambest.Objects
Imports System.Xml
Imports FFACETools
Module Main
    Private Delay As Integer = 0
    Private partycount As Integer = 0
    Sub Main()
        Dim path As String = My.Application.Info.DirectoryPath()

        Dim statelist As String = path + "\states.txt"
        Dim effectlist As String = path + "\effects.txt"
        Dim filepath As String = path + "\main.xml" ' path to default xml

        Dim statewriter As New System.IO.StreamWriter(statelist)
        statewriter.Write(String.Join(vbNewLine, (System.Enum.GetNames(GetType(FFACETools.Status)))))
        statewriter.Close() ' creates spelllist.txt reference file for user.

        Dim effectwriter As New System.IO.StreamWriter(effectlist)
        effectwriter.Write(String.Join(vbNewLine, (System.Enum.GetNames(GetType(StatusEffect)))))
        effectwriter.Close() ' creates spelllist.txt reference file for user.

        Dim processes As Process() = Process.GetProcessesByName("pol") 'find all open ffxi instances
        If processes.Length = 0 Then ' none exist
            OutError(0, "Please make sure ffxi is running and open.", True)
        End If


        Dim Args As String() = Environment.GetCommandLineArgs() 'see if user loaded a non-default xml

        If Args.Length > 1 Then  'User loaded a file, 
            filepath = Args(1)   'use that instead of main.xml
        End If

        If Not My.Computer.FileSystem.FileExists(filepath) Then 'File couldn't be found.
            OutError(0, "xml not found.", True)
        End If

        Dim CharList As List(Of character) = Parse(filepath, Delay, partycount) 'parse the xml

        Dim threadlist As New List(Of System.Threading.Thread) ' time to make threads!

        For Each character As character In CharList
            For Each ffxi As Process In processes
                If ffxi.MainWindowTitle = character.Name Then 'verify character exists in xml AND is logged on
                    Dim instance As New FFACE(ffxi.Id)
                    character.INSTANCE = instance 'bind their fface instance to their gambits
                    Dim charObj As New monitorObject ' initialize them
                    charObj.delay = Delay 'set their variables
                    charObj.player = character
                    charObj.partycount = partycount

                    threadlist.Add(New System.Threading.Thread(AddressOf charObj.Monitor)) 'add it to the stack
                End If
            Next
        Next

        If threadlist.ToArray.Length = 0 Then
            OutError(0, "No characters found, check charnames in xml.", True)
        End If

        For Each thread As System.Threading.Thread In threadlist
            thread.Start() 'commence running
        Next

    End Sub

End Module
