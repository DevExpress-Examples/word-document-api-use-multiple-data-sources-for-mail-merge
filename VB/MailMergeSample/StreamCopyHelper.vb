Imports System.IO

Public Module StreamCopyHelper

    Public Sub Copy(ByVal src As Stream, ByVal dst As Stream)
        Const bufferSize As Integer = 32768
        Dim buffer As Byte() = New Byte(32767) {}
        Dim bytesRead As Integer = 0
        Do
            bytesRead = src.Read(buffer, 0, bufferSize)
            dst.Write(buffer, 0, bytesRead)
        Loop While bytesRead = bufferSize
    End Sub
End Module
