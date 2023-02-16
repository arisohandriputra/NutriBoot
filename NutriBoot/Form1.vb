Imports System.IO
Imports System.Diagnostics
Public Class Form1

    Dim proc As New Process
    Dim dura As Double


    Dim aModuleName As String = Diagnostics.Process.GetCurrentProcess.MainModule.ModuleName

    Dim aProcName As String = System.IO.Path.GetFileNameWithoutExtension(aModuleName)

#Region "USB EVENT"

    Private WM_DEVICECHANGE As Integer = &H219

    Public Enum WM_DEVICECHANGE_WPPARAMS As Integer
        DBT_CONFIGCHANGECANCELED = &H19
        DBT_CONFIGCHANGED = &H18
        DBT_CUSTOMEVENT = &H8006
        DBT_DEVICEARRIVAL = &H8000
        DBT_DEVICEQUERYREMOVE = &H8001
        DBT_DEVICEQUERYREMOVEFAILED = &H8002
        DBT_DEVICEREMOVECOMPLETE = &H8004
        DBT_DEVICEREMOVEPENDING = &H8003
        DBT_DEVICETYPESPECIFIC = &H8005
        DBT_DEVNODES_CHANGED = &H7
        DBT_QUERYCHANGECONFIG = &H17
        DBT_USERDEFINED = &HFFFF
    End Enum

    Private Structure DEV_BROADCAST_VOLUME
        Public dbcv_size As Int32
        Public dbcv_devicetype As Int32
        Public dbcv_reserved As Int32
        Public dbcv_unitmask As Int32
        Public dbcv_flags As Int16
    End Structure

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

        Try
            If m.Msg = WM_DEVICECHANGE Then
                Dim Volume As DEV_BROADCAST_VOLUME
                Volume = DirectCast(Runtime.InteropServices.Marshal.PtrToStructure(m.LParam, GetType(DEV_BROADCAST_VOLUME)), DEV_BROADCAST_VOLUME)



                If Not GetDriveLetterFromMask(Volume.dbcv_unitmask).ToString.Trim = String.Empty Then

                    Dim DriveLetter As String = (GetDriveLetterFromMask(Volume.dbcv_unitmask) & ":\")
                    Dim dlabel As String = (GetDriveLetterFromMask(Volume.dbcv_unitmask))
                    Select Case m.WParam

                        Case WM_DEVICECHANGE_WPPARAMS.DBT_DEVICEARRIVAL

                            TextBox1.Text = DriveLetter
                            lbvolume.Text = dlabel + ":"

                        Case WM_DEVICECHANGE_WPPARAMS.DBT_DEVICEREMOVECOMPLETE

                            TextBox1.Text = "Not Detected"
                            lbvolume.Text = ""
                    End Select

                End If

            End If

        Catch ex As Exception
        End Try

        MyBase.WndProc(m)

    End Sub

    Private Function GetDriveLetterFromMask(ByRef Unit As Int32) As Char
        For i As Integer = 0 To 25
            If Unit = (2 ^ i) Then
                Return Chr(Asc("A") + i)
            End If
        Next
        Return ""
    End Function

#End Region





    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Text = My.Settings.cek
        CommandPromptToolStripMenuItem.Image = Image.FromFile("./res/diskmgmt.png")
        Button3.Image = Image.FromFile("./res/up.png")
        Button2.Image = Image.FromFile("./res/iso.png")
        SettingsToolStripMenuItem.Image = Image.FromFile("./res/settings.dll")
        AboutToolStripMenuItem.Image = Image.FromFile("./res/about.dll")
        ToolStripDropDownButton1.Image = Image.FromFile("./res/help.dll")
        Me.Text = "[" + My.Computer.Name + "] - " + "NutriBoot"
        If Label1.Text = "ntfs" Then
            RadioButton1.Checked = True
            AlwaysSetFileSystemToNTFSToolStripMenuItem.Checked = True
            RadioButton2.Checked = False
            AlwaysSetFileSystemToFAT32ToolStripMenuItem.Checked = False
        ElseIf Label1.Text = "fat32" Then
            RadioButton2.Checked = True
            AlwaysSetFileSystemToFAT32ToolStripMenuItem.Checked = True
            RadioButton1.Checked = False
            AlwaysSetFileSystemToNTFSToolStripMenuItem.Checked = False
        Else

        End If
    End Sub

    Private Sub TextBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Click
        Me.ActiveControl = Nothing
    End Sub

    Private Sub TextBox1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseClick
        Me.ActiveControl = Nothing
    End Sub

    Private Sub TextBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseDown
        Me.ActiveControl = Nothing
    End Sub

    Private Sub TextBox1_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.MouseEnter
        Me.ActiveControl = Nothing
    End Sub


    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Me.ActiveControl = Nothing
    End Sub

    Private Sub TextBox2_ChangeUICues(ByVal sender As Object, ByVal e As System.Windows.Forms.UICuesEventArgs) Handles TextBox2.ChangeUICues

    End Sub

    Private Sub TextBox2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Click
        Me.ActiveControl = Nothing
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lbiso.Text = lbstrip.Text + OpenFileDialog1.FileName + lbstrip.Text
            TextBox2.Text = System.IO.Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)

        End If
    End Sub

    Private Sub TextBox2_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox2.MouseClick
        Me.ActiveControl = Nothing
    End Sub

    Private Sub TextBox2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox2.MouseDown
        Me.ActiveControl = Nothing
    End Sub

    Private Sub TextBox2_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.MouseEnter
        Me.ActiveControl = Nothing
    End Sub

    Private Sub TextBox2_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.MouseHover
        TextBox2.ForeColor = Color.YellowGreen
    End Sub

    Private Sub TextBox2_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.MouseLeave
        TextBox2.ForeColor = Color.Black
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Me.ActiveControl = Nothing
       
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.ActiveControl = Nothing
        If TextBox1.Text = "Not Detected" And TextBox2.Text = "Click here or Drag and Drop" Then
            MsgBox("please complete the contents of the requirements above.", vbExclamation, "Failed")
        ElseIf TextBox1.Text = "Not Detected" Then
            MsgBox("remove and reinsert your Flashdisk, so that it can be detected automatically.", vbExclamation, "Drive not Detected")
        ElseIf TextBox2.Text = "Click here or Drag and Drop" Then
            MsgBox("select your windows installation iso file", vbExclamation, "ISO file not Found")
        ElseIf RadioButton1.Checked = False And RadioButton2.Checked = False Then
            MsgBox("Please select your installation file system type", vbExclamation, "Select File System")
        Else
            If RadioButton1.Checked = True Then
                BackgroundWorker2.RunWorkerAsync()
            ElseIf RadioButton2.Checked = True Then
                BackgroundWorker3.RunWorkerAsync()
            End If
        End If
    End Sub


    Private Sub AlwaysSetFileSystemToNTFSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlwaysSetFileSystemToNTFSToolStripMenuItem.Click
        RadioButton1.Checked = True
        AlwaysSetFileSystemToNTFSToolStripMenuItem.Checked = True
        RadioButton2.Checked = False
        AlwaysSetFileSystemToFAT32ToolStripMenuItem.Checked = False
        My.Settings.cek = "ntfs"
        My.Settings.Save()
    End Sub

    Private Sub AlwaysSetFileSystemToFAT32ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlwaysSetFileSystemToFAT32ToolStripMenuItem.Click
        RadioButton1.Checked = False
        AlwaysSetFileSystemToNTFSToolStripMenuItem.Checked = False
        RadioButton2.Checked = True
        AlwaysSetFileSystemToFAT32ToolStripMenuItem.Checked = True
        My.Settings.cek = "fat32"
        My.Settings.Save()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lbiso.Text = lbstrip.Text + OpenFileDialog1.FileName + lbstrip.Text
            TextBox2.Text = System.IO.Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.ActiveControl = Nothing

        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            lbiso.Text = lbstrip.Text + OpenFileDialog1.FileName + lbstrip.Text
            TextBox2.Text = System.IO.Path.GetFileNameWithoutExtension(OpenFileDialog1.FileName)

        End If
    End Sub

    Private Sub ToolStripStatusLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel1.Click

    End Sub

    Private Sub DiskpartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiskpartToolStripMenuItem.Click
        If TextBox1.Text = "Not Detected" And TextBox2.Text = "Click here or Drag and Drop" Then
            MsgBox("please complete the contents of the requirements above.", vbExclamation, "Failed")
        ElseIf TextBox1.Text = "Not Detected" Then
            MsgBox("remove and reinsert your Flashdisk, so that it can be detected automatically.", vbExclamation, "Drive not Detected")
        ElseIf TextBox2.Text = "Click here or Drag and Drop" Then
            MsgBox("select your windows installation iso file", vbExclamation, "ISO file not Found")
            MsgBox("important : select the windows version of your windows iso file", vbExclamation, "Select Windows Version")
        ElseIf RadioButton1.Checked = False And RadioButton2.Checked = False Then
            MsgBox("Please select your installation file system type", vbExclamation, "Select File System")
        Else
            If RadioButton1.Checked = True Then
                BackgroundWorker2.RunWorkerAsync()
            ElseIf RadioButton2.Checked = True Then
                BackgroundWorker3.RunWorkerAsync()
            End If
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        About.Show()
    End Sub

    Private Sub DocumentationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

#Region "FORMAT"
    Function ntfs()
        Control.CheckForIllegalCrossThreadCalls = False
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim cmd As String = "/k format " + lbvolume.Text + " /fs:ntfs /q " + "&label " + lbvolume.Text + " " + TextBox2.Text + " &exit"
        startinfo.FileName = "cmd.exe"
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        proc.StartInfo = startinfo
        Application.DoEvents()
        proc.Start()
        Do
            Me.Hide()
        Loop Until proc.HasExited

        Return 0
    End Function

    Function fats()
        Control.CheckForIllegalCrossThreadCalls = False
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim cmd As String = "/k format " + lbvolume.Text + " /fs:fat32 /q " + "&label " + lbvolume.Text + " " + TextBox2.Text + " &exit"
        startinfo.FileName = "cmd.exe"
        startinfo.Arguments = cmd
        startinfo.UseShellExecute = False
        proc.StartInfo = startinfo
        Application.DoEvents()
        proc.Start()
        Do
            Me.Hide()
        Loop Until proc.HasExited

        Return 0
    End Function
#End Region

#Region "Extract"
    Function startransfer()

        Control.CheckForIllegalCrossThreadCalls = False
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim cmd As String = "x -y -o" + TextBox1.Text + " " + lbiso.Text
        startinfo.FileName = Application.StartupPath & "./lib/7zG.exe"
        startinfo.Arguments = cmd

        proc.StartInfo = startinfo
        Application.DoEvents()
        proc.Start()

        Do
            Me.Hide()
        Loop Until proc.HasExited

        Return 0
    End Function

    Function tpm()
        Control.CheckForIllegalCrossThreadCalls = False
        Dim file As String = My.Application.Info.DirectoryPath & "/lib/TPMBypass.zip"
        Dim out As String = lbstrip.Text + file + lbstrip.Text
        Dim startinfo As New System.Diagnostics.ProcessStartInfo
        Dim cmd As String = "x -y -o" + TextBox1.Text + " " + out
        startinfo.FileName = Application.StartupPath & "./lib/7zG.exe"
        startinfo.Arguments = cmd

        proc.StartInfo = startinfo
        Application.DoEvents()
        proc.Start()

        Do
            Me.Hide()
        Loop Until proc.HasExited

        Return 0
    End Function

    Function bypass()
        Dim procStartInfo As New ProcessStartInfo
        Dim procExecuting As New Process

        With procStartInfo
            .UseShellExecute = True
            .FileName = Application.StartupPath & "./lib/TPMBypass.cmd"
            .WindowStyle = ProcessWindowStyle.Normal
            .Verb = "runas" 'add this to prompt for elevation
        End With

        procExecuting = Process.Start(procStartInfo)
        Do

        Loop Until procExecuting.HasExited

        Return 0
    End Function
#End Region

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        startransfer()
    End Sub

    Private Sub BackgroundWorker2_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        ntfs()
    End Sub

    Private Sub BackgroundWorker2_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        extract.Show()
    End Sub

    Private Sub BackgroundWorker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        fats()
    End Sub

    Private Sub BackgroundWorker3_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker3.RunWorkerCompleted
        extract.Show()
    End Sub


    Private Sub BackgroundWorker4_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker4.DoWork
        bypass()
    End Sub

    Private Sub BackgroundWorker4_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker4.RunWorkerCompleted
        BackgroundWorker5.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker5_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker5.DoWork
        tpm()
    End Sub

    Private Sub BackgroundWorker5_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker5.RunWorkerCompleted
        startransfer()
    End Sub

    Private Sub BackgroundWorker6_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker6.DoWork

    End Sub

    Private Sub BackgroundWorker6_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker6.RunWorkerCompleted

    End Sub

    Private Sub OpenFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

    End Sub

    Private Sub BuyMeACoffeeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Process.Start("https://www.buymeacoffee.com/nutriboot")
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.ActiveControl = Nothing
        If Me.Height = "281" Then
            Me.Height = "332"
            Button3.Image = Image.FromFile("./res/up.png")
            Button3.Text = "Hide Settings"
            GroupBox4.Visible = True
        ElseIf Me.Height = "332" Then
            Me.Height = "281"
            Button3.Image = Image.FromFile("./res/down.png")
            Button3.Text = "Show Settings"
            GroupBox4.Visible = False
        End If
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub ToolStripDropDownButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripDropDownButton1.Click

    End Sub

    Private Sub DiskpartToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiskpartToolStripMenuItem1.Click
        Process.Start("diskpart.exe")
    End Sub

    Private Sub CommandPromptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CommandPromptToolStripMenuItem.Click
        Process.Start("diskmgmt.msc")
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        MsgBox("Creating Bootable USB Drives, Successfully", vbInformation, "Successfully")
        Application.Exit()
    End Sub
End Class
