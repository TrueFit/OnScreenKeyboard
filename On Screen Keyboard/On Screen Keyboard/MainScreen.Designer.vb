<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainScreen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnRun = New System.Windows.Forms.Button()
        Me.txtInputFile = New System.Windows.Forms.TextBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.txtOutputResults = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnRun
        '
        Me.btnRun.Location = New System.Drawing.Point(323, 12)
        Me.btnRun.Name = "btnRun"
        Me.btnRun.Size = New System.Drawing.Size(75, 23)
        Me.btnRun.TabIndex = 1
        Me.btnRun.Text = "Run"
        Me.btnRun.UseVisualStyleBackColor = True
        '
        'txtInputFile
        '
        Me.txtInputFile.Location = New System.Drawing.Point(12, 12)
        Me.txtInputFile.Multiline = True
        Me.txtInputFile.Name = "txtInputFile"
        Me.txtInputFile.ReadOnly = True
        Me.txtInputFile.Size = New System.Drawing.Size(305, 60)
        Me.txtInputFile.TabIndex = 0
        Me.txtInputFile.Text = """C:\sample.txt"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'txtOutputResults
        '
        Me.txtOutputResults.Location = New System.Drawing.Point(12, 96)
        Me.txtOutputResults.Multiline = True
        Me.txtOutputResults.Name = "txtOutputResults"
        Me.txtOutputResults.Size = New System.Drawing.Size(386, 136)
        Me.txtOutputResults.TabIndex = 2
        '
        'MainScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 244)
        Me.Controls.Add(Me.txtOutputResults)
        Me.Controls.Add(Me.txtInputFile)
        Me.Controls.Add(Me.btnRun)
        Me.Name = "MainScreen"
        Me.Text = "On Screen Keyboard"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRun As System.Windows.Forms.Button
    Friend WithEvents txtInputFile As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtOutputResults As System.Windows.Forms.TextBox

End Class
