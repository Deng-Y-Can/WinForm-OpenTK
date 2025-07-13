namespace WinFormsApp.Robot
{
    partial class PIDVisualizationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _mainLayoutPanel = new TableLayoutPanel();
            _paramLayoutPanel = new TableLayoutPanel();
            paramPanel = new Panel();
            verticalLayoutPanel = new TableLayoutPanel();
            _kpLabel = new Label();
            _kpTextBox = new TextBox();
            _kiLabel = new Label();
            _kiTextBox = new TextBox();
            _kdLabel = new Label();
            _kdTextBox = new TextBox();
            _dtLabel = new Label();
            _dtTextBox = new TextBox();
            _updateButton = new Button();
            _leftBgColorButton = new Button();
            _rightBgColorButton = new Button();
            _graphPanel = new DoubleBufferedPanel();
            _timer = new System.Windows.Forms.Timer(components);
            _mainLayoutPanel.SuspendLayout();
            _paramLayoutPanel.SuspendLayout();
            paramPanel.SuspendLayout();
            verticalLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _mainLayoutPanel
            // 
            _mainLayoutPanel.ColumnCount = 2;
            _mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            _mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            _mainLayoutPanel.Controls.Add(_paramLayoutPanel, 0, 0);
            _mainLayoutPanel.Controls.Add(_graphPanel, 1, 0);
            _mainLayoutPanel.Dock = DockStyle.Fill;
            _mainLayoutPanel.Location = new Point(0, 0);
            _mainLayoutPanel.Name = "_mainLayoutPanel";
            _mainLayoutPanel.RowCount = 1;
            _mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _mainLayoutPanel.Size = new Size(800, 450);
            _mainLayoutPanel.TabIndex = 0;
            // 
            // _paramLayoutPanel
            // 
            _paramLayoutPanel.ColumnCount = 1;
            _paramLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _paramLayoutPanel.Controls.Add(paramPanel, 0, 0);
            _paramLayoutPanel.Dock = DockStyle.Fill;
            _paramLayoutPanel.Location = new Point(3, 3);
            _paramLayoutPanel.Name = "_paramLayoutPanel";
            _paramLayoutPanel.RowCount = 1;
            _paramLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _paramLayoutPanel.Size = new Size(234, 444);
            _paramLayoutPanel.TabIndex = 0;
            // 
            // paramPanel
            // 
            paramPanel.BackColor = Color.LightGray;
            paramPanel.BorderStyle = BorderStyle.FixedSingle;
            paramPanel.Controls.Add(verticalLayoutPanel);
            paramPanel.Dock = DockStyle.Fill;
            paramPanel.Location = new Point(3, 3);
            paramPanel.Name = "paramPanel";
            paramPanel.Padding = new Padding(20);
            paramPanel.Size = new Size(228, 438);
            paramPanel.TabIndex = 0;
            // 
            // verticalLayoutPanel
            // 
            verticalLayoutPanel.AutoSize = true;
            verticalLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            verticalLayoutPanel.ColumnCount = 2;
            verticalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            verticalLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            verticalLayoutPanel.Controls.Add(_kpLabel, 0, 0);
            verticalLayoutPanel.Controls.Add(_kpTextBox, 1, 0);
            verticalLayoutPanel.Controls.Add(_kiLabel, 0, 1);
            verticalLayoutPanel.Controls.Add(_kiTextBox, 1, 1);
            verticalLayoutPanel.Controls.Add(_kdLabel, 0, 2);
            verticalLayoutPanel.Controls.Add(_kdTextBox, 1, 2);
            verticalLayoutPanel.Controls.Add(_dtLabel, 0, 3);
            verticalLayoutPanel.Controls.Add(_dtTextBox, 1, 3);
            verticalLayoutPanel.Controls.Add(_updateButton, 0, 4);
            verticalLayoutPanel.Controls.Add(_leftBgColorButton, 0, 5);
            verticalLayoutPanel.Controls.Add(_rightBgColorButton, 0, 6);
            verticalLayoutPanel.Dock = DockStyle.Fill;
            verticalLayoutPanel.Location = new Point(20, 20);
            verticalLayoutPanel.Name = "verticalLayoutPanel";
            verticalLayoutPanel.RowCount = 7;
            verticalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            verticalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            verticalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            verticalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            verticalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            verticalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            verticalLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            verticalLayoutPanel.Size = new Size(186, 396);
            verticalLayoutPanel.TabIndex = 0;
            // 
            // _kpLabel
            // 
            _kpLabel.AutoSize = true;
            _kpLabel.Dock = DockStyle.Left;
            _kpLabel.Location = new Point(3, 0);
            _kpLabel.Name = "_kpLabel";
            _kpLabel.Padding = new Padding(0, 5, 0, 0);
            _kpLabel.Size = new Size(30, 30);
            _kpLabel.TabIndex = 0;
            _kpLabel.Text = "Kp:";
            // 
            // _kpTextBox
            // 
            _kpTextBox.Location = new Point(77, 3);
            _kpTextBox.Name = "_kpTextBox";
            _kpTextBox.Size = new Size(100, 27);
            _kpTextBox.TabIndex = 1;
            _kpTextBox.Text = "1";
            // 
            // _kiLabel
            // 
            _kiLabel.AutoSize = true;
            _kiLabel.Dock = DockStyle.Left;
            _kiLabel.Location = new Point(3, 30);
            _kiLabel.Name = "_kiLabel";
            _kiLabel.Padding = new Padding(0, 5, 0, 0);
            _kiLabel.Size = new Size(25, 30);
            _kiLabel.TabIndex = 2;
            _kiLabel.Text = "Ki:";
            // 
            // _kiTextBox
            // 
            _kiTextBox.Location = new Point(77, 33);
            _kiTextBox.Name = "_kiTextBox";
            _kiTextBox.Size = new Size(100, 27);
            _kiTextBox.TabIndex = 3;
            _kiTextBox.Text = "0.1";
            // 
            // _kdLabel
            // 
            _kdLabel.AutoSize = true;
            _kdLabel.Dock = DockStyle.Left;
            _kdLabel.Location = new Point(3, 60);
            _kdLabel.Name = "_kdLabel";
            _kdLabel.Padding = new Padding(0, 5, 0, 0);
            _kdLabel.Size = new Size(30, 30);
            _kdLabel.TabIndex = 4;
            _kdLabel.Text = "Kd:";
            // 
            // _kdTextBox
            // 
            _kdTextBox.Location = new Point(77, 63);
            _kdTextBox.Name = "_kdTextBox";
            _kdTextBox.Size = new Size(100, 27);
            _kdTextBox.TabIndex = 5;
            _kdTextBox.Text = "0.01";
            // 
            // _dtLabel
            // 
            _dtLabel.AutoSize = true;
            _dtLabel.Dock = DockStyle.Left;
            _dtLabel.Location = new Point(3, 90);
            _dtLabel.Name = "_dtLabel";
            _dtLabel.Padding = new Padding(0, 5, 0, 0);
            _dtLabel.Size = new Size(57, 30);
            _dtLabel.TabIndex = 6;
            _dtLabel.Text = "时间间隔 (s):";
            // 
            // _dtTextBox
            // 
            _dtTextBox.Location = new Point(77, 93);
            _dtTextBox.Name = "_dtTextBox";
            _dtTextBox.Size = new Size(100, 27);
            _dtTextBox.TabIndex = 7;
            _dtTextBox.Text = "0.1";
            // 
            // _updateButton
            // 
            _updateButton.BackColor = Color.SteelBlue;
            verticalLayoutPanel.SetColumnSpan(_updateButton, 2);
            _updateButton.FlatAppearance.BorderSize = 0;
            _updateButton.FlatStyle = FlatStyle.Flat;
            _updateButton.ForeColor = Color.White;
            _updateButton.Location = new Point(3, 123);
            _updateButton.Name = "_updateButton";
            _updateButton.Size = new Size(180, 40);
            _updateButton.TabIndex = 8;
            _updateButton.Text = "更新参数";
            _updateButton.UseVisualStyleBackColor = false;
            _updateButton.Click += UpdateButton_Click;
            // 
            // _leftBgColorButton
            // 
            verticalLayoutPanel.SetColumnSpan(_leftBgColorButton, 2);
            _leftBgColorButton.Location = new Point(3, 173);
            _leftBgColorButton.Name = "_leftBgColorButton";
            _leftBgColorButton.Size = new Size(180, 40);
            _leftBgColorButton.TabIndex = 9;
            _leftBgColorButton.Text = "左侧背景颜色";
            _leftBgColorButton.UseVisualStyleBackColor = true;
            _leftBgColorButton.Click += LeftBgColorButton_Click;
            // 
            // _rightBgColorButton
            // 
            verticalLayoutPanel.SetColumnSpan(_rightBgColorButton, 2);
            _rightBgColorButton.Location = new Point(3, 223);
            _rightBgColorButton.Name = "_rightBgColorButton";
            _rightBgColorButton.Size = new Size(180, 40);
            _rightBgColorButton.TabIndex = 10;
            _rightBgColorButton.Text = "右侧背景颜色";
            _rightBgColorButton.UseVisualStyleBackColor = true;
            _rightBgColorButton.Click += RightBgColorButton_Click;
            // 
            // _graphPanel
            // 
            _graphPanel.Dock = DockStyle.Fill;
            _graphPanel.Location = new Point(243, 3);
            _graphPanel.Name = "_graphPanel";
            _graphPanel.Padding = new Padding(40);
            _graphPanel.Size = new Size(554, 444);
            _graphPanel.TabIndex = 1;
            _graphPanel.Paint += GraphPanel_Paint;
            // 
            // _timer
            // 
            _timer.Tick += Timer_Tick;
            // 
            // PIDVisualizationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(800, 450);
            Controls.Add(_mainLayoutPanel);
            Font = new Font("Segoe UI", 9F);
            Name = "PIDVisualizationForm";
            Text = "PIDVisualizationForm";
            _mainLayoutPanel.ResumeLayout(false);
            _paramLayoutPanel.ResumeLayout(false);
            paramPanel.ResumeLayout(false);
            paramPanel.PerformLayout();
            verticalLayoutPanel.ResumeLayout(false);
            verticalLayoutPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _mainLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel _paramLayoutPanel;
        private System.Windows.Forms.Panel paramPanel;
        private System.Windows.Forms.TableLayoutPanel verticalLayoutPanel;
        private System.Windows.Forms.Label _kpLabel;
        private System.Windows.Forms.TextBox _kpTextBox;
        private System.Windows.Forms.Label _kiLabel;
        private System.Windows.Forms.TextBox _kiTextBox;
        private System.Windows.Forms.Label _kdLabel;
        private System.Windows.Forms.TextBox _kdTextBox;
        private System.Windows.Forms.Label _dtLabel;
        private System.Windows.Forms.TextBox _dtTextBox;
        private System.Windows.Forms.Button _updateButton;
        private System.Windows.Forms.Button _leftBgColorButton;
        private System.Windows.Forms.Button _rightBgColorButton;
        private DoubleBufferedPanel _graphPanel;
        private System.Windows.Forms.Timer _timer;
    }
}