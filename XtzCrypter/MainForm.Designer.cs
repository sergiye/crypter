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
      this.btnEncodeFile = new System.Windows.Forms.Button();
      this.btnPlain = new System.Windows.Forms.Button();
      this.txtPlain = new System.Windows.Forms.TextBox();
      this.btnDecodeFile = new System.Windows.Forms.Button();
      this.btnCrypted = new System.Windows.Forms.Button();
      this.txtCrypted = new System.Windows.Forms.TextBox();
      this.panCommon = new System.Windows.Forms.Panel();
      this.txtPassword = new System.Windows.Forms.TextBox();
      this.lblPassword = new System.Windows.Forms.Label();
      this.panTools = new System.Windows.Forms.Panel();
      this.panel1 = new System.Windows.Forms.Panel();
      this.gbxPlain = new System.Windows.Forms.GroupBox();
      this.gbxCrypted = new System.Windows.Forms.GroupBox();
      this.panCommon.SuspendLayout();
      this.panTools.SuspendLayout();
      this.panel1.SuspendLayout();
      this.gbxPlain.SuspendLayout();
      this.gbxCrypted.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnEncodeFile
      // 
      this.btnEncodeFile.Location = new System.Drawing.Point(6, 81);
      this.btnEncodeFile.Name = "btnEncodeFile";
      this.btnEncodeFile.Size = new System.Drawing.Size(35, 23);
      this.btnEncodeFile.TabIndex = 3;
      this.btnEncodeFile.Text = "F->";
      this.btnEncodeFile.UseVisualStyleBackColor = true;
      this.btnEncodeFile.Click += new System.EventHandler(this.btnEncodeFile_Click);
      // 
      // btnPlain
      // 
      this.btnPlain.Location = new System.Drawing.Point(6, 6);
      this.btnPlain.Name = "btnPlain";
      this.btnPlain.Size = new System.Drawing.Size(35, 23);
      this.btnPlain.TabIndex = 2;
      this.btnPlain.Text = "->";
      this.btnPlain.UseVisualStyleBackColor = true;
      this.btnPlain.Click += new System.EventHandler(this.btnPlain_Click);
      // 
      // txtPlain
      // 
      this.txtPlain.AllowDrop = true;
      this.txtPlain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtPlain.Location = new System.Drawing.Point(3, 16);
      this.txtPlain.Multiline = true;
      this.txtPlain.Name = "txtPlain";
      this.txtPlain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtPlain.Size = new System.Drawing.Size(224, 121);
      this.txtPlain.TabIndex = 1;
      this.txtPlain.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtPlain_DragDrop);
      this.txtPlain.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtDropTarget_DragEnter);
      // 
      // btnDecodeFile
      // 
      this.btnDecodeFile.Location = new System.Drawing.Point(6, 110);
      this.btnDecodeFile.Name = "btnDecodeFile";
      this.btnDecodeFile.Size = new System.Drawing.Size(35, 23);
      this.btnDecodeFile.TabIndex = 6;
      this.btnDecodeFile.Text = "<-F";
      this.btnDecodeFile.UseVisualStyleBackColor = true;
      this.btnDecodeFile.Click += new System.EventHandler(this.btnDecodeFile_Click);
      // 
      // btnCrypted
      // 
      this.btnCrypted.Location = new System.Drawing.Point(6, 35);
      this.btnCrypted.Name = "btnCrypted";
      this.btnCrypted.Size = new System.Drawing.Size(35, 23);
      this.btnCrypted.TabIndex = 5;
      this.btnCrypted.Text = "<-";
      this.btnCrypted.UseVisualStyleBackColor = true;
      this.btnCrypted.Click += new System.EventHandler(this.btnCrypted_Click);
      // 
      // txtCrypted
      // 
      this.txtCrypted.AllowDrop = true;
      this.txtCrypted.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCrypted.Location = new System.Drawing.Point(3, 16);
      this.txtCrypted.Multiline = true;
      this.txtCrypted.Name = "txtCrypted";
      this.txtCrypted.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtCrypted.Size = new System.Drawing.Size(224, 121);
      this.txtCrypted.TabIndex = 4;
      this.txtCrypted.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtCrypted_DragDrop);
      this.txtCrypted.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtDropTarget_DragEnter);
      // 
      // panCommon
      // 
      this.panCommon.Controls.Add(this.txtPassword);
      this.panCommon.Controls.Add(this.lblPassword);
      this.panCommon.Dock = System.Windows.Forms.DockStyle.Top;
      this.panCommon.Location = new System.Drawing.Point(0, 0);
      this.panCommon.Name = "panCommon";
      this.panCommon.Size = new System.Drawing.Size(507, 38);
      this.panCommon.TabIndex = 3;
      // 
      // txtPassword
      // 
      this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPassword.Location = new System.Drawing.Point(120, 11);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(384, 20);
      this.txtPassword.TabIndex = 1;
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
      this.panTools.Controls.Add(this.gbxCrypted);
      this.panTools.Controls.Add(this.panel1);
      this.panTools.Controls.Add(this.gbxPlain);
      this.panTools.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panTools.Location = new System.Drawing.Point(0, 38);
      this.panTools.Name = "panTools";
      this.panTools.Size = new System.Drawing.Size(507, 140);
      this.panTools.TabIndex = 3;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnDecodeFile);
      this.panel1.Controls.Add(this.btnEncodeFile);
      this.panel1.Controls.Add(this.btnPlain);
      this.panel1.Controls.Add(this.btnCrypted);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(230, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(47, 140);
      this.panel1.TabIndex = 7;
      // 
      // gbxPlain
      // 
      this.gbxPlain.Controls.Add(this.txtPlain);
      this.gbxPlain.Dock = System.Windows.Forms.DockStyle.Left;
      this.gbxPlain.Location = new System.Drawing.Point(0, 0);
      this.gbxPlain.Name = "gbxPlain";
      this.gbxPlain.Size = new System.Drawing.Size(230, 140);
      this.gbxPlain.TabIndex = 5;
      this.gbxPlain.TabStop = false;
      this.gbxPlain.Text = "Plain";
      // 
      // gbxCrypted
      // 
      this.gbxCrypted.Controls.Add(this.txtCrypted);
      this.gbxCrypted.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbxCrypted.Location = new System.Drawing.Point(277, 0);
      this.gbxCrypted.Name = "gbxCrypted";
      this.gbxCrypted.Size = new System.Drawing.Size(230, 140);
      this.gbxCrypted.TabIndex = 6;
      this.gbxCrypted.TabStop = false;
      this.gbxCrypted.Text = "Crypted";
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(507, 178);
      this.Controls.Add(this.panTools);
      this.Controls.Add(this.panCommon);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "XtzCrypter";
      this.panCommon.ResumeLayout(false);
      this.panCommon.PerformLayout();
      this.panTools.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.gbxPlain.ResumeLayout(false);
      this.gbxPlain.PerformLayout();
      this.gbxCrypted.ResumeLayout(false);
      this.gbxCrypted.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Button btnPlain;
    private System.Windows.Forms.TextBox txtPlain;
    private System.Windows.Forms.Button btnCrypted;
    private System.Windows.Forms.TextBox txtCrypted;
    private System.Windows.Forms.Panel panCommon;
    private System.Windows.Forms.Panel panTools;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Label lblPassword;
    private System.Windows.Forms.Button btnEncodeFile;
    private System.Windows.Forms.Button btnDecodeFile;
    private System.Windows.Forms.GroupBox gbxCrypted;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.GroupBox gbxPlain;
  }
}

