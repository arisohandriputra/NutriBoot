Public Class extract

    Private Sub extract_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub


    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        If ProgressBar1.Value = 20 Then
            Label1.Text = "• Make Back Up for USB Firmware Settings ..."
        ElseIf ProgressBar1.Value = 50 Then
            Label1.Text = "• Installing USB Firmware Control ..."
        ElseIf ProgressBar1.Value = 70 Then
            Label1.Text = "• Preparing Finished ..."
        ElseIf ProgressBar1.Value = 80 Then
            Label1.Text = "• Load 7-zip Extraction Tools ..."
        End If
        ProgressBar1.Increment(1)
        If ProgressBar1.Value = 100 Then
            Timer1.Stop()
            Me.Close()
            If Form1.CheckBox1.Checked = True Then
                Form1.BackgroundWorker4.RunWorkerAsync()
            ElseIf Form1.CheckBox1.Checked = False Then
                Form1.startransfer()
            End If
        End If
    End Sub
End Class