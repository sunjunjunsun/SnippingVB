Imports System.IO
Imports System.Threading

Public Class Form1
    '成员变量放置处
    'Private sw As StreamWriter
    'Private sr As StreamReader
    Private Sub WriteContent(Sn As String, PathS As String)
        Dim fs As New FileStream(PathS + "\\" + "Content.txt", FileMode.Append)
        Dim sw As New StreamWriter(fs)
        sw.WriteLine(Sn.ToUpper())
        sw.Close()
        fs.Close()
    End Sub



    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Sn"></param>
    ''' <param name="Ma"></param>
    ''' <param name="Lie"></param>
    ''' <remarks></remarks>
    Private Sub SnippingMethods(Sn As String, Ma As String, Lie As String)
        Me.Hide()
        Dim p1 As New Point(0, 0)
        Dim p2 As New Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - 40)
        Dim pic As New Bitmap(p2.X, p2.Y - 40)
        Dim parentPath As String = TextBox4.Text
        Using g As Graphics = Graphics.FromImage(pic)
            Thread.Sleep(260)
            g.CopyFromScreen(p1, p1, p2)
            g.Dispose()
            'Me.BackgroundImage = pic
            'pic.Save("D:\\" + Sn + ".jpg")
            Dim timefolder As String = DateTime.Now.ToString("mmddss")
            Dim timefolder2 As String = DateTime.Now.ToString("MMdd")
            Dim dt As DateTime = DateTime.Now
            Dim dateStr1 As String = "20:00:00"
            Dim dateStr2 As String = dt.ToString("HH:mm:ss")
            Dim t1 As DateTime = Convert.ToDateTime(dateStr1)
            Dim t2 As DateTime = Convert.ToDateTime(dateStr2)
            Dim compNum As Int16 = DateTime.Compare(t1, t2)
            If (compNum > 0) Then
                '继续创建第二天（当天的）的文件夹，并且往里面加上图片
                Dim saveRealPath As String = parentPath + "\\" + Lie + "\\" + timefolder2 + "\\" + Sn + "__" + Ma + "__" + timefolder + "VB" + ".jpg"
                Dim filepaht As String = parentPath + "\\" + Lie + "\\" + timefolder2
                If (Directory.Exists(filepaht)) Then
                Else
                    Directory.CreateDirectory(filepaht)
                End If
                pic.Save(saveRealPath)
                WriteContent(Sn, filepaht)
            ElseIf (compNum < 0) Then
                '创建第二天的文件夹，并且往里面加上图片
                Dim tommorm As String = DateTime.Now.AddDays(+1).ToString("MMdd")
                Dim saveRealPath As String = parentPath + "\\" + Lie + "\\" + tommorm + "\\" + Sn + "__" + Ma + "__" + timefolder + "VB" + ".jpg"
                Dim filepaht As String = parentPath + "\\" + Lie + "\\" + tommorm
                If (Directory.Exists(filepaht)) Then
                Else
                    Directory.CreateDirectory(filepaht)
                End If
                pic.Save(saveRealPath)
                WriteContent(Sn, filepaht)
            End If
        End Using
        Me.Show()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (TextBox1.Text = "") Then
            MessageBox.Show("请确框内有值")
            Return
        End If
        If (TextBox2.Text = "") Then
            MessageBox.Show("请确框内有值")
            Return
        End If
        If (TextBox3.Text = "") Then
            MessageBox.Show("请确框内有值")
            Return
        End If
        '写入多行数据
        '以追加的形式写入多行数据
        'Dim i As Integer
        'sw = New StreamWriter("D:/21.txt", True, System.Text.Encoding.Default)
        'sw.WriteLine("test")
        'sw.Flush()
        'sw.Close()
        'sw = Nothing
        'Dim SnStr As String
        'SnStr = TextBox1.Text.ToUpper
        SnippingMethods(TextBox3.Text, TextBox1.Text, TextBox2.Text)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (Directory.Exists("C:\classifiedDefects\data")) Then
        Else
            Directory.CreateDirectory("C:\classifiedDefects\data")
        End If
        RadioButton1.Checked = True
        FileSystemWatcher1.Path = "C:\classifiedDefects\data"
        'FileSystemWatcher1.Filter = ".txt"
        TextBox4.Text = "\\172.26.120.75\aoiaxi\AXI\5DX\5DX不良"
        TextBox1.Text = "V810-8057S2"
        TextBox2.Text = "R12"
        TextBox3.Text = "B77880486041900A"
    End Sub

    Private Sub WriteLineByMachine(maStr As String)
        If (maStr.Equals("V810-3325S2EX")) Then
            TextBox2.Text = "Q12"
        ElseIf (maStr.Equals("V810-3327S2EX")) Then
            TextBox2.Text = "I12"
        ElseIf (maStr.Equals("V810-3323S2EX")) Then
            TextBox2.Text = "I22"
        ElseIf (maStr.Equals("V810-3328S2EX")) Then
            TextBox2.Text = "H12"
        ElseIf (maStr.Equals("V810-8096S2")) Then
            TextBox2.Text = "J12"
        ElseIf (maStr.Equals("V810-8085S2")) Then
            TextBox2.Text = "K12"
        ElseIf (maStr.Equals("V810-8070S2")) Then
            TextBox2.Text = "K22"
        ElseIf (maStr.Equals("V810-8064S2")) Then
            TextBox2.Text = "L12"
        ElseIf (maStr.Equals("V810-8057S2")) Then
            TextBox2.Text = "L22"
        ElseIf (maStr.Equals("V810-8086S2")) Then
            TextBox2.Text = "P12"
        End If


    End Sub

    Private Sub FileSystemWatcher1_Created(sender As Object, e As FileSystemEventArgs) Handles FileSystemWatcher1.Created
        'Dim fs As New FileStream("d:\logs.txt", FileMode.Append)
        'Dim sw As New StreamWriter(fs)
        'sw.WriteLine(Now() & Microsoft.VisualBasic.vbTab & "创建" & e.FullPath)
        'sw.Close()
        'fs.Close()
        Dim SnStr1 As String
        If (e.FullPath.EndsWith("BoardStatus.txt")) Then
            Thread.Sleep(350)
            Dim fs1 As New FileStream(e.FullPath, FileMode.Open)
            Dim rw As New StreamReader(fs1)
            'Dim b() As String
            SnStr1 = Split(rw.ReadLine, ";")(1)
            TextBox3.Text = SnStr1.ToUpper.Trim()
            Me.Text = SnStr1.ToUpper.Trim()
            rw.Close()
            fs1.Close()
            Dim b() As String
            Dim nameArr() As String
            b = Split(e.FullPath, "\")
            'Dim b2 As String
            'For Each b3 As String In b
            'Console.WriteLine(b3)
            'Next
            'V810-8085S2[@$@]2020-12-04-21-55-51  3
            nameArr = Split(b(3), "[")
            TextBox1.Text = nameArr(0)
            WriteLineByMachine(nameArr(0))
            'For Each b4 As String In nameArr
            'Console.WriteLine(b4)
            'Next
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        TextBox4.Text = "\\172.26.120.75\aoiaxi\AXI\5DX\5DX不良"
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        TextBox4.Text = "\\172.26.12.16\aoi\5DX\5DX不良"
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim path1 As String = "\\172.26.120.75\aoiaxi\AXI\5DX\5DX不良"
        System.Diagnostics.Process.Start("explorer", path1)
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim path1 As String = "\\172.26.12.16\aoi\5DX\5DX不良"
        System.Diagnostics.Process.Start("explorer", path1)
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim path1 As String = "C:\ITF\XMLCEXPORT"
        System.Diagnostics.Process.Start("explorer", path1)
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Dim path1 As String = "C:\AXI\CBackup\" + DateTime.Now.ToString("yyyyMMdd")
        System.Diagnostics.Process.Start("explorer", path1)
    End Sub
End Class
