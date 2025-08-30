namespace _3dedit
{
    partial class KeybindSetup
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
            this.keybindSetsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.addNewLayout = new System.Windows.Forms.Button();
            this.keybindsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.DeleteLayout = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.keybindSetsPanel.SuspendLayout();
            this.keybindsPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // keybindSetsPanel
            // 
            this.keybindSetsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.keybindSetsPanel.AutoScroll = true;
            this.keybindSetsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.keybindSetsPanel.Controls.Add(this.addNewLayout);
            this.keybindSetsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.keybindSetsPanel.Location = new System.Drawing.Point(12, 12);
            this.keybindSetsPanel.Name = "keybindSetsPanel";
            this.keybindSetsPanel.Size = new System.Drawing.Size(195, 383);
            this.keybindSetsPanel.TabIndex = 0;
            this.keybindSetsPanel.WrapContents = false;
            // 
            // addNewLayout
            // 
            this.addNewLayout.Location = new System.Drawing.Point(3, 3);
            this.addNewLayout.Name = "addNewLayout";
            this.addNewLayout.Size = new System.Drawing.Size(163, 36);
            this.addNewLayout.TabIndex = 0;
            this.addNewLayout.Text = "Add New Layout";
            this.addNewLayout.UseVisualStyleBackColor = true;
            this.addNewLayout.Click += new System.EventHandler(this.AddNewLayout_Click);
            // 
            // keybindsPanel
            // 
            this.keybindsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keybindsPanel.AutoScroll = true;
            this.keybindsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.keybindsPanel.Controls.Add(this.flowLayoutPanel1);
            this.keybindsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.keybindsPanel.Location = new System.Drawing.Point(213, 12);
            this.keybindsPanel.Name = "keybindsPanel";
            this.keybindsPanel.Size = new System.Drawing.Size(588, 383);
            this.keybindsPanel.TabIndex = 1;
            this.keybindsPanel.WrapContents = false;
            // 
            // DeleteLayout
            // 
            this.DeleteLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteLayout.Location = new System.Drawing.Point(679, 410);
            this.DeleteLayout.Name = "DeleteLayout";
            this.DeleteLayout.Size = new System.Drawing.Size(122, 30);
            this.DeleteLayout.TabIndex = 3;
            this.DeleteLayout.Text = "Delete Layout";
            this.DeleteLayout.UseVisualStyleBackColor = true;
            this.DeleteLayout.Click += new System.EventHandler(this.DeleteLayout_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.textBox1);
            this.flowLayoutPanel1.Controls.Add(this.comboBox1);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(573, 28);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(94, 22);
            this.textBox1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(103, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.numericUpDown1);
            this.flowLayoutPanel2.Controls.Add(this.checkBox1);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(230, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(349, 22);
            this.flowLayoutPanel2.TabIndex = 3;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(3, 3);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(129, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(95, 20);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // KeybindSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 452);
            this.Controls.Add(this.DeleteLayout);
            this.Controls.Add(this.keybindsPanel);
            this.Controls.Add(this.keybindSetsPanel);
            this.Name = "KeybindSetup";
            this.Text = "Keybind Setup";
            this.Load += new System.EventHandler(this.KeybindSetup_Load);
            this.keybindSetsPanel.ResumeLayout(false);
            this.keybindsPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel keybindSetsPanel;
        private System.Windows.Forms.Button addNewLayout;
        private System.Windows.Forms.FlowLayoutPanel keybindsPanel;
        private System.Windows.Forms.Button DeleteLayout;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}