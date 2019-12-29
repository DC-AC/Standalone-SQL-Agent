Imports System
Imports ConfigSettings


Public Class ConfigFile
    Shared Sub AddConnectionString(ByVal ConnectionString As String, ByVal InstanceName As String)
        Dim InstanceCount As Integer
        Try
            InstanceCount = ReadSetting("InstanceCount")
        Catch
            WriteSetting("InstanceCount", 1)
        End Try

        If InstanceCount = Nothing Then
            WriteSetting("InstanceCount", 0)
            InstanceCount = 0
        End If

        While InstanceCount <> ReadSetting("InstanceCount")
            If ReadSetting("Instance" + InstanceCount.ToString) = InstanceName Then
                RemoveSetting(InstanceName)
                RemoveSetting(ReturnInstanceName(InstanceCount))
                WriteSetting("InstanceName", ConnectionString)
                Exit Sub
            End If
            InstanceCount += 1
        End While

        InstanceCount = ReadSetting("InstanceCount") + 1
        WriteSetting("InstanceCount", InstanceCount)
        WriteSetting("Instance" + InstanceCount.ToString, InstanceName)
        WriteSetting(InstanceName, ConnectionString)
    End Sub

    Overloads Shared Function ReturnConnectionString(ByVal InstanceId As Integer)
        Dim InstanceName As String

        InstanceName = ReturnInstanceName(InstanceId)

        Return ReturnConnectionString(InstanceName)
    End Function
    Overloads Shared Function ReturnConnectionString(ByVal InstanceName As String)
        Dim ConnectionString As String

        InstanceName = ReadSetting(InstanceName)

        ConnectionString = ReadSetting(InstanceName)



        Return ConnectionString
    End Function

    Shared Function ReturnInstanceName(ByVal InstanceId As Integer)
        If ReadSetting("InstanceCount") < InstanceId Then Return Nothing
        Dim PrivateInstanceName As String

        PrivateInstanceName = "Instance" + InstanceId.ToString

        Return PrivateInstanceName
    End Function
    Shared Function ReturnRealInstanceName(ByVal InstanceName)
        Return ReadSetting(InstanceName)
    End Function

    Shared Function ReturnInstanceCount()
        Dim InstanceCount As Integer

        Try
            InstanceCount = ReadSetting("InstanceCount")
        Catch ex As Exception
            WriteSetting("InstanceCount", 0)
            InstanceCount = 0
        End Try

        Return InstanceCount

    End Function
End Class
