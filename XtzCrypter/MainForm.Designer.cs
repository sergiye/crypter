namespace XtzCrypter
{
  partial class MainForm
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
      this.panPlain = new System.Windows.Forms.Panel();
      this.btnEncodeFile = new System.Windows.Forms.Button();
      this.btnPlain = new System.Windows.Forms.Button();
      this.txtPlain = new System.Windows.Forms.TextBox();
      this.lblPlain = new System.Windows.Forms.Label();
      this.panCrypted = new System.Windows.Forms.Panel();
      this.btnDecodeFile = new System.Windows.Forms.Button();
      this.btnCrypted = new System.Windows.Forms.Button();
      this.txtCrypted = new System.Windows.Forms.TextBox();
      this.lblCrypted = new System.Windows.Forms.Label();
      this.panCommon = new System.Windows.Forms.Panel();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.lblPassword = new System.Windows.Forms.Label();
      this.panTools = new System.Windows.Forms.Panel();
      this.panPlain.SuspendLayout();
      this.panCrypted.SuspendLayout();
      this.panCommon.SuspendLayout();
      this.panTools.SuspendLayout();
      this.SuspendLayout();
      // 
      // panPlain
      // 
      this.panPlain.Controls.Add(this.btnEncodeFile);
      this.panPlain.Controls.Add(this.btnPlain);
      this.panPlain.Controls.Add(this.txtPlain);
      this.panPlain.Controls.Add(this.lblPlain);
      this.panPlain.Dock = System.Windows.Forms.DockStyle.Left;
      this.panPlain.Location = new System.Drawing.Point(0, 0);
      this.panPlain.Name = "panPlain";
      this.panPlain.Size = new System.Drawing.Size(396, 331);
      this.panPlain.TabIndex = 0;
      // 
      // btnEncodeFile
      // 
      this.btnEncodeFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.btnEncodeFile.Location = new System.Drawing.Point(239, 302);
      this.btnEncodeFile.Name = "btnEncodeFile";
      this.btnEncodeFile.Size = new System.Drawing.Size(70, 23);
      this.btnEncodeFile.TabIndex = 3;
      this.btnEncodeFile.Text = "Encrypt file";
      this.btnEncodeFile.UseVisualStyleBackColor = true;
      this.btnEncodeFile.Click += new System.EventHandler(this.btnEncodeFile_Click);
      // 
      // btnPlain
      // 
      this.btnPlain.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.btnPlain.Location = new System.Drawing.Point(163, 302);
      this.btnPlain.Name = "btnPlain";
      this.btnPlain.Size = new System.Drawing.Size(70, 23);
      this.btnPlain.TabIndex = 2;
      this.btnPlain.Text = "Encrypt";
      this.btnPlain.UseVisualStyleBackColor = true;
      this.btnPlain.Click += new System.EventHandler(this.btnPlain_Click);
      // 
      // txtPlain
      // 
      this.txtPlain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPlain.Location = new System.Drawing.Point(4, 20);
      this.txtPlain.Multiline = true;
      this.txtPlain.Name = "txtPlain";
      this.txtPlain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtPlain.Size = new System.Drawing.Size(389, 276);
      this.txtPlain.TabIndex = 1;
      // 
      // lblPlain
      // 
      this.lblPlain.AutoSize = true;
      this.lblPlain.Location = new System.Drawing.Point(4, 4);
      this.lblPlain.Name = "lblPlain";
      this.lblPlain.Size = new System.Drawing.Size(30, 13);
      this.lblPlain.TabIndex = 0;
      this.lblPlain.Text = "Plain";
      // 
      // panCrypted
      // 
      this.panCrypted.Controls.Add(this.btnDecodeFile);
      this.panCrypted.Controls.Add(this.btnCrypted);
      this.panCrypted.Controls.Add(this.txtCrypted);
      this.panCrypted.Controls.Add(this.lblCrypted);
      this.panCrypted.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panCrypted.Location = new System.Drawing.Point(396, 0);
      this.panCrypted.Name = "panCrypted";
      this.panCrypted.Size = new System.Drawing.Size(396, 331);
      this.panCrypted.TabIndex = 1;
      // 
      // btnDecodeFile
      // 
      this.btnDecodeFile.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.btnDecodeFile.Location = new System.Drawing.Point(239, 302);
      this.btnDecodeFile.Name = "btnDecodeFile";
      this.btnDecodeFile.Size = new System.Drawing.Size(70, 23);
      this.btnDecodeFile.TabIndex = 6;
      this.btnDecodeFile.Text = "Decrypt file";
      this.btnDecodeFile.UseVisualStyleBackColor = true;
      this.btnDecodeFile.Click += new System.EventHandler(this.btnDecodeFile_Click);
      // 
      // btnCrypted
      // 
      this.btnCrypted.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.btnCrypted.Location = new System.Drawing.Point(163, 302);
      this.btnCrypted.Name = "btnCrypted";
      this.btnCrypted.Size = new System.Drawing.Size(70, 23);
      this.btnCrypted.TabIndex = 5;
      this.btnCrypted.Text = "Decrypt";
      this.btnCrypted.UseVisualStyleBackColor = true;
      this.btnCrypted.Click += new System.EventHandler(this.btnCrypted_Click);
      // 
      // txtCrypted
      // 
      this.txtCrypted.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtCrypted.Location = new System.Drawing.Point(4, 20);
      this.txtCrypted.Multiline = true;
      this.txtCrypted.Name = "txtCrypted";
      this.txtCrypted.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtCrypted.Size = new System.Drawing.Size(389, 276);
      this.txtCrypted.TabIndex = 4;
      // 
      // lblCrypted
      // 
      this.lblCrypted.AutoSize = true;
      this.lblCrypted.Location = new System.Drawing.Point(4, 4);
      this.lblCrypted.Name = "lblCrypted";
      this.lblCrypted.Size = new System.Drawing.Size(43, 13);
      this.lblCrypted.TabIndex = 3;
      this.lblCrypted.Text = "Crypted";
      // 
      // panCommon
      // 
      this.panCommon.Controls.Add(this.txtPassword);
      this.panCommon.Controls.Add(this.lblPassword);
      this.panCommon.Dock = System.Windows.Forms.DockStyle.Top;
      this.panCommon.Location = new System.Drawing.Point(0, 0);
      this.panCommon.Name = "panCommon";
      this.panCommon.Size = new System.Drawing.Size(792, 38);
      this.panCommon.TabIndex = 3;
      // 
      // txtPassword
      // 
      this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPassword.Location = new System.Drawing.Point(120, 11);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(669, 20);
      this.txtPassword.TabIndex = 1;
      this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
      // 
      // lblPassword
      // 
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new System.Drawing.Point(12, 14);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new System.Drawing.Size(102, 13);
      this.lblPassword.TabIndex = 0;
      this.lblPassword.Text = "Password (or empty)";
      // 
      // panTools
      // 
      this.panTools.Controls.Add(this.panCrypted);
      this.panTools.Controls.Add(this.panPlain);
      this.panTools.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panTools.Location = new System.Drawing.Point(0, 38);
      this.panTools.Name = "panTools";
      this.panTools.Size = new System.Drawing.Size(792, 331);
      this.panTools.TabIndex = 3;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(792, 369);
      this.Controls.Add(this.panTools);
      this.Controls.Add(this.panCommon);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "XtzCrypter";
      this.panPlain.ResumeLayout(false);
      this.panPlain.PerformLayout();
      this.panCrypted.ResumeLayout(false);
      this.panCrypted.PerformLayout();
      this.panCommon.ResumeLayout(false);
      this.panCommon.PerformLayout();
      this.panTools.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panPlain;
    private System.Windows.Forms.Panel panCrypted;
    private System.Windows.Forms.Button btnPlain;
    private System.Windows.Forms.TextBox txtPlain;
    private System.Windows.Forms.Label lblPlain;
    private System.Windows.Forms.Button btnCrypted;
    private System.Windows.Forms.TextBox txtCrypted;
    private System.Windows.Forms.Label lblCrypted;
    private System.Windows.Forms.Panel panCommon;
    private System.Windows.Forms.Panel panTools;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Button btnEncodeFile;
    private System.Windows.Forms.Button btnDecodeFile;
  }
}

