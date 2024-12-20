﻿namespace InfoCare_Final
{
    partial class AdminLogin
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            HomeButton = new Guna.UI2.WinForms.Guna2CircleButton();
            LoginButton = new Guna.UI2.WinForms.Guna2Button();
            ShowpasswordCheckbox = new CheckBox();
            PasswordTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            UsernameTextbox = new Guna.UI2.WinForms.Guna2TextBox();
            guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            guna2PictureBox2 = new Guna.UI2.WinForms.Guna2PictureBox();
            guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).BeginInit();
            SuspendLayout();
            // 
            // HomeButton
            // 
            HomeButton.Cursor = Cursors.Hand;
            HomeButton.DisabledState.BorderColor = Color.DarkGray;
            HomeButton.DisabledState.CustomBorderColor = Color.DarkGray;
            HomeButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            HomeButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            HomeButton.FillColor = Color.Transparent;
            HomeButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            HomeButton.ForeColor = Color.White;
            HomeButton.Image = Properties.Resources.HOME_LOGO;
            HomeButton.Location = new Point(13, 14);
            HomeButton.Name = "HomeButton";
            HomeButton.ShadowDecoration.CustomizableEdges = customizableEdges1;
            HomeButton.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            HomeButton.Size = new Size(48, 39);
            HomeButton.TabIndex = 29;
            HomeButton.Click += HomeButton_Click;
            // 
            // LoginButton
            // 
            LoginButton.BorderRadius = 20;
            LoginButton.Cursor = Cursors.Hand;
            LoginButton.CustomizableEdges = customizableEdges2;
            LoginButton.DisabledState.BorderColor = Color.DarkGray;
            LoginButton.DisabledState.CustomBorderColor = Color.DarkGray;
            LoginButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            LoginButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            LoginButton.FillColor = Color.FromArgb(102, 162, 205);
            LoginButton.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point);
            LoginButton.ForeColor = Color.Black;
            LoginButton.Location = new Point(235, 297);
            LoginButton.Name = "LoginButton";
            LoginButton.ShadowDecoration.CustomizableEdges = customizableEdges3;
            LoginButton.Size = new Size(169, 56);
            LoginButton.TabIndex = 26;
            LoginButton.Text = "Log In";
            LoginButton.Click += LoginButton_Click;
            // 
            // ShowpasswordCheckbox
            // 
            ShowpasswordCheckbox.AutoSize = true;
            ShowpasswordCheckbox.Cursor = Cursors.Hand;
            ShowpasswordCheckbox.Location = new Point(52, 265);
            ShowpasswordCheckbox.Name = "ShowpasswordCheckbox";
            ShowpasswordCheckbox.Size = new Size(132, 24);
            ShowpasswordCheckbox.TabIndex = 25;
            ShowpasswordCheckbox.Text = "Show Password";
            ShowpasswordCheckbox.UseVisualStyleBackColor = true;
            ShowpasswordCheckbox.CheckedChanged += ShowpasswordCheckbox_CheckedChanged;
            // 
            // PasswordTextbox
            // 
            PasswordTextbox.BorderColor = Color.Transparent;
            PasswordTextbox.BorderRadius = 10;
            PasswordTextbox.Cursor = Cursors.IBeam;
            PasswordTextbox.CustomizableEdges = customizableEdges4;
            PasswordTextbox.DefaultText = "";
            PasswordTextbox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            PasswordTextbox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            PasswordTextbox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            PasswordTextbox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            PasswordTextbox.FillColor = Color.FromArgb(102, 162, 205);
            PasswordTextbox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            PasswordTextbox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            PasswordTextbox.ForeColor = Color.White;
            PasswordTextbox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            PasswordTextbox.Location = new Point(39, 205);
            PasswordTextbox.Name = "PasswordTextbox";
            PasswordTextbox.PasswordChar = '●';
            PasswordTextbox.PlaceholderForeColor = Color.Black;
            PasswordTextbox.PlaceholderText = "Password";
            PasswordTextbox.SelectedText = "";
            PasswordTextbox.ShadowDecoration.CustomizableEdges = customizableEdges5;
            PasswordTextbox.Size = new Size(365, 45);
            PasswordTextbox.TabIndex = 24;
            // 
            // UsernameTextbox
            // 
            UsernameTextbox.BorderColor = Color.Transparent;
            UsernameTextbox.BorderRadius = 10;
            UsernameTextbox.Cursor = Cursors.IBeam;
            UsernameTextbox.CustomizableEdges = customizableEdges6;
            UsernameTextbox.DefaultText = "";
            UsernameTextbox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            UsernameTextbox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            UsernameTextbox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            UsernameTextbox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            UsernameTextbox.FillColor = Color.FromArgb(102, 162, 205);
            UsernameTextbox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            UsernameTextbox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            UsernameTextbox.ForeColor = Color.White;
            UsernameTextbox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            UsernameTextbox.Location = new Point(39, 143);
            UsernameTextbox.Name = "UsernameTextbox";
            UsernameTextbox.PasswordChar = '\0';
            UsernameTextbox.PlaceholderForeColor = Color.Black;
            UsernameTextbox.PlaceholderText = "Username";
            UsernameTextbox.SelectedText = "";
            UsernameTextbox.ShadowDecoration.CustomizableEdges = customizableEdges7;
            UsernameTextbox.Size = new Size(365, 45);
            UsernameTextbox.TabIndex = 23;
            // 
            // guna2PictureBox1
            // 
            guna2PictureBox1.CustomizableEdges = customizableEdges8;
            guna2PictureBox1.Image = Properties.Resources.LOGO_INFO_CARE;
            guna2PictureBox1.ImageRotate = 0F;
            guna2PictureBox1.Location = new Point(571, -9);
            guna2PictureBox1.Name = "guna2PictureBox1";
            guna2PictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges9;
            guna2PictureBox1.Size = new Size(115, 101);
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            guna2PictureBox1.TabIndex = 22;
            guna2PictureBox1.TabStop = false;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Impact", 22F, FontStyle.Regular, GraphicsUnit.Point);
            guna2HtmlLabel1.Location = new Point(81, 79);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(244, 47);
            guna2HtmlLabel1.TabIndex = 21;
            guna2HtmlLabel1.Text = "LOG IN AS ADMIN";
            // 
            // guna2PictureBox2
            // 
            guna2PictureBox2.BackColor = Color.FromArgb(174, 206, 229);
            guna2PictureBox2.CustomizableEdges = customizableEdges10;
            guna2PictureBox2.Image = Properties.Resources.ADMIN_LOGO;
            guna2PictureBox2.ImageRotate = 0F;
            guna2PictureBox2.Location = new Point(475, 143);
            guna2PictureBox2.Name = "guna2PictureBox2";
            guna2PictureBox2.ShadowDecoration.CustomizableEdges = customizableEdges11;
            guna2PictureBox2.Size = new Size(136, 126);
            guna2PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            guna2PictureBox2.TabIndex = 30;
            guna2PictureBox2.TabStop = false;
            // 
            // guna2CirclePictureBox1
            // 
            guna2CirclePictureBox1.Image = Properties.Resources.Screenshot_2024_11_15_2114051;
            guna2CirclePictureBox1.ImageRotate = 0F;
            guna2CirclePictureBox1.Location = new Point(446, 112);
            guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            guna2CirclePictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges12;
            guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2CirclePictureBox1.Size = new Size(197, 195);
            guna2CirclePictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            guna2CirclePictureBox1.TabIndex = 31;
            guna2CirclePictureBox1.TabStop = false;
            // 
            // AdminLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1022, 766);
            ControlBox = false;
            Controls.Add(guna2PictureBox2);
            Controls.Add(guna2CirclePictureBox1);
            Controls.Add(HomeButton);
            Controls.Add(LoginButton);
            Controls.Add(ShowpasswordCheckbox);
            Controls.Add(PasswordTextbox);
            Controls.Add(UsernameTextbox);
            Controls.Add(guna2PictureBox1);
            Controls.Add(guna2HtmlLabel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AdminLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Load += AdminLogin_Load;
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)guna2PictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleButton HomeButton;
        private Guna.UI2.WinForms.Guna2Button LoginButton;
        private CheckBox ShowpasswordCheckbox;
        private Guna.UI2.WinForms.Guna2TextBox PasswordTextbox;
        private Guna.UI2.WinForms.Guna2TextBox UsernameTextbox;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox2;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
    }
}