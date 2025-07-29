namespace WinFormsApp.WindowsTool
{
    partial class EncryptionTool
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlMain = new Panel();
            splitContainer = new SplitContainer();
            pnlParameters = new TableLayoutPanel();
            pnlAlgorithmSelection = new Panel();
            cmbAlgorithmType = new ComboBox();
            label1 = new Label();
            pnlActions = new FlowLayoutPanel();
            toolTip = new ToolTip(components);
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            pnlAlgorithmSelection.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(splitContainer);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Margin = new Padding(4, 4, 4, 4);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(13, 12, 13, 12);
            pnlMain.Size = new Size(1093, 706);
            pnlMain.TabIndex = 0;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.FixedPanel = FixedPanel.Panel2;
            splitContainer.Location = new Point(13, 12);
            splitContainer.Margin = new Padding(4, 4, 4, 4);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(pnlParameters);
            splitContainer.Panel1.Controls.Add(pnlAlgorithmSelection);
            splitContainer.Panel1.Padding = new Padding(6, 6, 6, 6);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(pnlActions);
            splitContainer.Panel2.Padding = new Padding(6, 6, 6, 6);
            splitContainer.Panel2MinSize = 80;
            splitContainer.Size = new Size(1067, 682);
            splitContainer.SplitterDistance = 575;
            splitContainer.SplitterWidth = 6;
            splitContainer.TabIndex = 0;
            // 
            // pnlParameters
            // 
            pnlParameters.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            pnlParameters.ColumnCount = 2;
            pnlParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            pnlParameters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F));
            pnlParameters.Dock = DockStyle.Fill;
            pnlParameters.Location = new Point(6, 59);
            pnlParameters.Margin = new Padding(4, 4, 4, 4);
            pnlParameters.Name = "pnlParameters";
            pnlParameters.Padding = new Padding(6, 6, 6, 6);
            pnlParameters.RowStyles.Add(new RowStyle());
            pnlParameters.Size = new Size(1055, 510);
            pnlParameters.TabIndex = 1;
            // 
            // pnlAlgorithmSelection
            // 
            pnlAlgorithmSelection.BackColor = Color.FromArgb(245, 245, 245);
            pnlAlgorithmSelection.BorderStyle = BorderStyle.FixedSingle;
            pnlAlgorithmSelection.Controls.Add(cmbAlgorithmType);
            pnlAlgorithmSelection.Controls.Add(label1);
            pnlAlgorithmSelection.Dock = DockStyle.Top;
            pnlAlgorithmSelection.Location = new Point(6, 6);
            pnlAlgorithmSelection.Margin = new Padding(0, 0, 0, 12);
            pnlAlgorithmSelection.Name = "pnlAlgorithmSelection";
            pnlAlgorithmSelection.Padding = new Padding(10, 9, 10, 9);
            pnlAlgorithmSelection.Size = new Size(1055, 53);
            pnlAlgorithmSelection.TabIndex = 0;
            // 
            // cmbAlgorithmType
            // 
            cmbAlgorithmType.Dock = DockStyle.Fill;
            cmbAlgorithmType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAlgorithmType.FormattingEnabled = true;
            cmbAlgorithmType.Location = new Point(113, 9);
            cmbAlgorithmType.Margin = new Padding(0);
            cmbAlgorithmType.Name = "cmbAlgorithmType";
            cmbAlgorithmType.Size = new Size(930, 28);
            cmbAlgorithmType.TabIndex = 1;
            toolTip.SetToolTip(cmbAlgorithmType, "选择加密算法类别");
            cmbAlgorithmType.SelectedIndexChanged += cmbAlgorithmType_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Left;
            label1.Font = new Font("微软雅黑", 9F, FontStyle.Bold);
            label1.Location = new Point(10, 9);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(103, 33);
            label1.TabIndex = 0;
            label1.Text = "算法类型:";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlActions
            // 
            pnlActions.BackColor = Color.FromArgb(245, 245, 245);
            pnlActions.Dock = DockStyle.Fill;
            pnlActions.Location = new Point(6, 6);
            pnlActions.Margin = new Padding(4, 4, 4, 4);
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(6, 6, 6, 6);
            pnlActions.Size = new Size(1055, 89);
            pnlActions.TabIndex = 0;
            pnlActions.WrapContents = false;
            // 
            // EncryptionTool
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1093, 706);
            Controls.Add(pnlMain);
            Font = new Font("微软雅黑", 9F);
            ForeColor = Color.FromArgb(50, 50, 50);
            Margin = new Padding(4, 4, 4, 4);
            MinimumSize = new Size(1023, 639);
            Name = "EncryptionTool";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "加密工具 - 高级加密算法套件";
            pnlMain.ResumeLayout(false);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            pnlAlgorithmSelection.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.SplitContainer splitContainer; // 新增分割容器
        private System.Windows.Forms.TableLayoutPanel pnlParameters;
        private System.Windows.Forms.Panel pnlAlgorithmSelection;
        private System.Windows.Forms.ComboBox cmbAlgorithmType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel pnlActions; // 按钮区域改用FlowLayoutPanel
        private System.Windows.Forms.ToolTip toolTip;

    }
}