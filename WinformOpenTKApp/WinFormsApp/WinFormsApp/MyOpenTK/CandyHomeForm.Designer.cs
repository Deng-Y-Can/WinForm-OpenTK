namespace WinFormsApp.MyOpenTK
{
    partial class CandyHomeForm
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
            glControl1 = new OpenTK.GLControl.GLControl();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // glControl1
            // 
            glControl1.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl1.APIVersion = new Version(3, 3, 0, 0);
            glControl1.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            glControl1.IsEventDriven = true;
            glControl1.Location = new Point(21, 68);
            glControl1.Name = "glControl1";
            glControl1.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            glControl1.SharedContext = null;
            glControl1.Size = new Size(800, 600);
            glControl1.TabIndex = 0;
            glControl1.Load += glControl1_Load;
            glControl1.MouseDown += glControl1_MouseDown;
            glControl1.MouseMove += glControl1_MouseMove;
            glControl1.MouseUp += glControl1_MouseUp;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(982, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "视图";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6, toolStripMenuItem7 });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(53, 24);
            toolStripMenuItem1.Text = "视图";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(113, 26);
            toolStripMenuItem2.Text = "X+";
            toolStripMenuItem2.Click += toolStripMenuItemX_Positive_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(113, 26);
            toolStripMenuItem3.Text = "X-";
            toolStripMenuItem3.Click += toolStripMenuItemX_Negative_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(113, 26);
            toolStripMenuItem4.Text = "Y+";
            toolStripMenuItem4.Click += toolStripMenuItemY_Positive_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(113, 26);
            toolStripMenuItem5.Text = "Y-";
            toolStripMenuItem5.Click += toolStripMenuItemY_Negative_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(113, 26);
            toolStripMenuItem6.Text = "Z+";
            toolStripMenuItem6.Click += toolStripMenuItemZ_Positive_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(113, 26);
            toolStripMenuItem7.Text = "Z-";
            toolStripMenuItem7.Click += toolStripMenuItemZ_Negative_Click;
            // 
            // CandyHomeForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 753);
            Controls.Add(glControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "CandyHomeForm";
            Text = "CandyHomeForm";
            SizeChanged += CandyHomeForm_SizeChanged;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenTK.GLControl.GLControl glControl1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
    }
}