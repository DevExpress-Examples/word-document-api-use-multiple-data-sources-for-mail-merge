Imports System.IO

Public NotInheritable Class StreamCopyHelper

    Private Sub New()
    End Sub

    Public Shared Sub Copy(ByVal src As Stream, ByVal dst As Stream)
        Const bufferSize As Integer = 32768
        Dim buffer(bufferSize - 1) As Byte
        Dim bytesRead As Integer = 0
        Do
            bytesRead = src.Read(buffer, 0, bufferSize)
            dst.Write(buffer, 0, bytesRead)

        Loop While bytesRead = bufferSize
    End Sub
End Class