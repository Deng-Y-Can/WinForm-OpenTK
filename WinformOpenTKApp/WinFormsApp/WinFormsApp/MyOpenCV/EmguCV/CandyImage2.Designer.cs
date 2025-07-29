namespace WinFormsApp.MyOpenCV.EmguCV
{
    partial class CandyImage2
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            selectionFillBrush.Dispose();
            selectionPen.Dispose();
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            cropToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            zoomInToolStripMenuItem = new ToolStripMenuItem();
            zoomOutToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            resetZoomToolStripMenuItem = new ToolStripMenuItem();
            toolStrip = new ToolStrip();
            colorReplaceButton = new ToolStripButton();
            colorPickerButton = new ToolStripButton();
            rangeColorReplaceButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            modeComboBox = new ToolStripComboBox();
            selectAreaButton = new ToolStripButton();
            replaceImageButton = new ToolStripButton();
            addTextButton = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            zoomInButton = new ToolStripButton();
            zoomOutButton = new ToolStripButton();
            zoomResetButton = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripLabel2 = new ToolStripLabel();
            effectComboBox = new ToolStripComboBox();
            applyEffectButton = new ToolStripButton();
            statusStrip = new StatusStrip();
            statusLbl = new ToolStripStatusLabel();
            imagePanel = new Panel();
            picBox = new PictureBox();
            menuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            imagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picBox).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 3, 0, 3);
            menuStrip.Size = new Size(1050, 27);
            menuStrip.TabIndex = 3;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { importToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator4, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(44, 21);
            fileToolStripMenuItem.Text = "文件";
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.I;
            importToolStripMenuItem.Size = new Size(168, 22);
            importToolStripMenuItem.Text = "导入图片";
            importToolStripMenuItem.Click += ImportButton_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            exportToolStripMenuItem.Size = new Size(168, 22);
            exportToolStripMenuItem.Text = "导出图片";
            exportToolStripMenuItem.Click += ExportButton_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(165, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            exitToolStripMenuItem.Size = new Size(168, 22);
            exitToolStripMenuItem.Text = "退出";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { cropToolStripMenuItem, toolStripSeparator5, undoToolStripMenuItem, redoToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(44, 21);
            editToolStripMenuItem.Text = "编辑";
            // 
            // cropToolStripMenuItem
            // 
            cropToolStripMenuItem.Name = "cropToolStripMenuItem";
            cropToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            cropToolStripMenuItem.Size = new Size(180, 22);
            cropToolStripMenuItem.Text = "截取图片";
            cropToolStripMenuItem.Click += CropButton_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(177, 6);
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(180, 22);
            undoToolStripMenuItem.Text = "撤销";
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            redoToolStripMenuItem.Size = new Size(180, 22);
            redoToolStripMenuItem.Text = "重做";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { zoomInToolStripMenuItem, zoomOutToolStripMenuItem, toolStripSeparator6, resetZoomToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 21);
            viewToolStripMenuItem.Text = "视图";
            // 
            // zoomInToolStripMenuItem
            // 
            zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            zoomInToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Oemplus;
            zoomInToolStripMenuItem.Size = new Size(200, 22);
            zoomInToolStripMenuItem.Text = "放大";
            zoomInToolStripMenuItem.Click += ZoomInButton_Click;
            // 
            // zoomOutToolStripMenuItem
            // 
            zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            zoomOutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.OemMinus;
            zoomOutToolStripMenuItem.Size = new Size(200, 22);
            zoomOutToolStripMenuItem.Text = "缩小";
            zoomOutToolStripMenuItem.Click += ZoomOutButton_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(197, 6);
            // 
            // resetZoomToolStripMenuItem
            // 
            resetZoomToolStripMenuItem.Name = "resetZoomToolStripMenuItem";
            resetZoomToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D0;
            resetZoomToolStripMenuItem.Size = new Size(200, 22);
            resetZoomToolStripMenuItem.Text = "重置缩放";
            resetZoomToolStripMenuItem.Click += ZoomResetButton_Click;
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { colorReplaceButton, colorPickerButton, rangeColorReplaceButton, toolStripSeparator1, toolStripLabel1, modeComboBox, selectAreaButton, replaceImageButton, addTextButton, toolStripSeparator2, zoomInButton, zoomOutButton, zoomResetButton, toolStripSeparator3, toolStripLabel2, effectComboBox, applyEffectButton });
            toolStrip.Location = new Point(0, 27);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(1050, 25);
            toolStrip.TabIndex = 4;
            toolStrip.Text = "toolStrip1";
            // 
            // colorReplaceButton
            // 
            colorReplaceButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            colorReplaceButton.Name = "colorReplaceButton";
            colorReplaceButton.Size = new Size(60, 22);
            colorReplaceButton.Text = "颜色替换";
            colorReplaceButton.Click += ColorReplaceButton_Click;
            // 
            // colorPickerButton
            // 
            colorPickerButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            colorPickerButton.Name = "colorPickerButton";
            colorPickerButton.Size = new Size(48, 22);
            colorPickerButton.Text = "取色器";
            colorPickerButton.Click += ColorPickerButton_Click;
            // 
            // rangeColorReplaceButton
            // 
            rangeColorReplaceButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            rangeColorReplaceButton.Name = "rangeColorReplaceButton";
            rangeColorReplaceButton.Size = new Size(84, 22);
            rangeColorReplaceButton.Text = "范围颜色替换";
            rangeColorReplaceButton.Click += RangeColorReplaceButton_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(59, 22);
            toolStripLabel1.Text = "选择模式:";
            // 
            // modeComboBox
            // 
            modeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            modeComboBox.Name = "modeComboBox";
            modeComboBox.Size = new Size(93, 25);
            modeComboBox.SelectedIndexChanged += modeComboBox_SelectedIndexChanged;
            // 
            // selectAreaButton
            // 
            selectAreaButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            selectAreaButton.Name = "selectAreaButton";
            selectAreaButton.Size = new Size(60, 22);
            selectAreaButton.Text = "框选区域";
            selectAreaButton.Click += SelectAreaButton_Click;
            // 
            // replaceImageButton
            // 
            replaceImageButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            replaceImageButton.Name = "replaceImageButton";
            replaceImageButton.Size = new Size(60, 22);
            replaceImageButton.Text = "替换图片";
            replaceImageButton.Click += ReplaceImageButton_Click;
            // 
            // addTextButton
            // 
            addTextButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            addTextButton.Name = "addTextButton";
            addTextButton.Size = new Size(60, 22);
            addTextButton.Text = "添加文字";
            addTextButton.Click += AddTextButton_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // zoomInButton
            // 
            zoomInButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            zoomInButton.Name = "zoomInButton";
            zoomInButton.Size = new Size(36, 22);
            zoomInButton.Text = "放大";
            zoomInButton.Click += ZoomInButton_Click;
            // 
            // zoomOutButton
            // 
            zoomOutButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            zoomOutButton.Name = "zoomOutButton";
            zoomOutButton.Size = new Size(36, 22);
            zoomOutButton.Text = "缩小";
            zoomOutButton.Click += ZoomOutButton_Click;
            // 
            // zoomResetButton
            // 
            zoomResetButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            zoomResetButton.Name = "zoomResetButton";
            zoomResetButton.Size = new Size(60, 22);
            zoomResetButton.Text = "重置缩放";
            zoomResetButton.Click += ZoomResetButton_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 25);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(59, 22);
            toolStripLabel2.Text = "图片特效:";
            // 
            // effectComboBox
            // 
            effectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            effectComboBox.Name = "effectComboBox";
            effectComboBox.Size = new Size(139, 25);
            // 
            // applyEffectButton
            // 
            applyEffectButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            applyEffectButton.Name = "applyEffectButton";
            applyEffectButton.Size = new Size(60, 22);
            applyEffectButton.Text = "应用特效";
            applyEffectButton.Click += ApplyEffectButton_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { statusLbl });
            statusStrip.Location = new Point(0, 859);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1050, 22);
            statusStrip.TabIndex = 5;
            statusStrip.Text = "statusStrip1";
            // 
            // statusLbl
            // 
            statusLbl.Name = "statusLbl";
            statusLbl.Size = new Size(32, 17);
            statusLbl.Text = "就绪";
            // 
            // imagePanel
            // 
            imagePanel.AutoScroll = true;
            imagePanel.Controls.Add(picBox);
            imagePanel.Dock = DockStyle.Fill;
            imagePanel.Location = new Point(0, 52);
            imagePanel.Margin = new Padding(4, 4, 4, 4);
            imagePanel.Name = "imagePanel";
            imagePanel.Size = new Size(1050, 807);
            imagePanel.TabIndex = 6;
            // 
            // picBox
            // 
            picBox.Location = new Point(0, 0);
            picBox.Margin = new Padding(4, 4, 4, 4);
            picBox.Name = "picBox";
            picBox.Size = new Size(467, 425);
            picBox.TabIndex = 0;
            picBox.TabStop = false;
            picBox.Paint += ImageBox_Paint;
            picBox.MouseDown += ImageBox_MouseDown;
            picBox.MouseMove += ImageBox_MouseMove;
            picBox.MouseUp += ImageBox_MouseUp;
            picBox.MouseWheel += PicBox_MouseWheel;
            // 
            // CandyImage2
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1050, 881);
            Controls.Add(imagePanel);
            Controls.Add(statusStrip);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Margin = new Padding(4, 4, 4, 4);
            Name = "CandyImage2";
            Text = "Candy Image 2";
            Load += CandyImage2_Load;
            Resize += CandyImage2_Resize;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            imagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picBox).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cropToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem resetZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton colorReplaceButton;
        private System.Windows.Forms.ToolStripButton colorPickerButton;
        private System.Windows.Forms.ToolStripButton selectAreaButton;
        private System.Windows.Forms.ToolStripButton replaceImageButton;
        private System.Windows.Forms.ToolStripButton addTextButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLbl;
        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.ToolStripButton zoomInButton;
        private System.Windows.Forms.ToolStripButton zoomOutButton;
        private System.Windows.Forms.ToolStripButton zoomResetButton;
        private System.Windows.Forms.ToolStripComboBox modeComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox effectComboBox;
        private System.Windows.Forms.ToolStripButton applyEffectButton;
        private System.Windows.Forms.ToolStripButton rangeColorReplaceButton;
    }
}