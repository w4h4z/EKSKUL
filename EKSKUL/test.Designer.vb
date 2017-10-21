<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class test
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
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea13 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend13 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series25 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series26 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.TestBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EKSKULDataSet10 = New EKSKUL.EKSKULDataSet10()
        Me.TestTableAdapter = New EKSKUL.EKSKULDataSet10TableAdapters.testTableAdapter()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.EKSKULDataSet11 = New EKSKUL.EKSKULDataSet11()
        Me.View1BindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.View_1TableAdapter = New EKSKUL.EKSKULDataSet11TableAdapters.View_1TableAdapter()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        CType(Me.TestBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EKSKULDataSet10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EKSKULDataSet11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.View1BindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TestBindingSource
        '
        Me.TestBindingSource.DataMember = "test"
        Me.TestBindingSource.DataSource = Me.EKSKULDataSet10
        '
        'EKSKULDataSet10
        '
        Me.EKSKULDataSet10.DataSetName = "EKSKULDataSet10"
        Me.EKSKULDataSet10.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TestTableAdapter
        '
        Me.TestTableAdapter.ClearBeforeFill = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(66, 51)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(36, 30)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "+"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(42, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "S0001"
        '
        'Chart1
        '
        ChartArea13.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea13)
        Me.Chart1.DataSource = Me.View1BindingSource
        Legend13.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend13)
        Me.Chart1.Location = New System.Drawing.Point(21, 104)
        Me.Chart1.Name = "Chart1"
        Series25.ChartArea = "ChartArea1"
        Series25.Legend = "Legend1"
        Series25.Name = "Faisal"
        Series25.XValueMember = "nama_siswa"
        Series25.YValueMembers = "nilai_mid"
        Series26.ChartArea = "ChartArea1"
        Series26.Legend = "Legend1"
        Series26.Name = "Arum"
        Series26.XValueMember = "nama_siswa"
        Series26.YValueMembers = "nilai_final"
        Me.Chart1.Series.Add(Series25)
        Me.Chart1.Series.Add(Series26)
        Me.Chart1.Size = New System.Drawing.Size(487, 317)
        Me.Chart1.TabIndex = 6
        Me.Chart1.Text = "Chart1"
        '
        'EKSKULDataSet11
        '
        Me.EKSKULDataSet11.DataSetName = "EKSKULDataSet11"
        Me.EKSKULDataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'View1BindingSource
        '
        Me.View1BindingSource.DataMember = "View_1"
        Me.View1BindingSource.DataSource = Me.EKSKULDataSet11
        '
        'View_1TableAdapter
        '
        Me.View_1TableAdapter.ClearBeforeFill = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(157, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "xxxxx"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(225, 57)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(81, 30)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "captcha"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(206, 28)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 9
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(325, 22)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(76, 30)
        Me.Button3.TabIndex = 10
        Me.Button3.Text = "refresh"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(24, 51)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(36, 30)
        Me.Button4.TabIndex = 11
        Me.Button4.Text = "-"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'test
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(530, 444)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Chart1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Name = "test"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "test"
        CType(Me.TestBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EKSKULDataSet10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EKSKULDataSet11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.View1BindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents EKSKULDataSet10 As EKSKULDataSet10
    Friend WithEvents TestBindingSource As BindingSource
    Friend WithEvents TestTableAdapter As EKSKULDataSet10TableAdapters.testTableAdapter
    Friend WithEvents Button2 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Chart1 As DataVisualization.Charting.Chart
    Friend WithEvents EKSKULDataSet11 As EKSKULDataSet11
    Friend WithEvents View1BindingSource As BindingSource
    Friend WithEvents View_1TableAdapter As EKSKULDataSet11TableAdapters.View_1TableAdapter
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
End Class
