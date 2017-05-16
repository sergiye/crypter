using System;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace XtzCrypter
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
      Icon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule.FileName);
    }

      private void btnPlain_Click(object sender, EventArgs e)
    {
      txtCrypted.Text = txtPlain.Text.Encrypt(AesCryptoProvider.GetKey(txtPassword.Text));
    }

    private void btnCrypted_Click(object sender, EventArgs e)
    {
      try
      {
        txtPlain.Text = txtCrypted.Text.Decrypt(AesCryptoProvider.GetKey(txtPassword.Text));
      }
      catch (Exception)
      {
        MessageBox.Show("Error decrypting data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void btnEncodeFile_Click(object sender, EventArgs e)
    {
      var dlg = new OpenFileDialog
                {
                  CheckFileExists = true,
                  Multiselect = false,
                  RestoreDirectory = true,
                };
      if (dlg.ShowDialog() != DialogResult.OK) return;
      EncryptFile(txtPassword.Text, dlg.FileName);
    }

    private void btnDecodeFile_Click(object sender, EventArgs e)
    {
      var dlg = new OpenFileDialog
                {
                  CheckFileExists = true,
                  Multiselect = false,
                  RestoreDirectory = true
                };
      if (dlg.ShowDialog() != DialogResult.OK) return;
      DecryptFile(txtPassword.Text, dlg.FileName);
    }

    private void txtDropTarget_DragEnter(object sender, DragEventArgs e)
    {
      if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
    }

    private void txtPlain_DragDrop(object sender, DragEventArgs e)
    {
      var files = (string[])e.Data.GetData(DataFormats.FileDrop);
      foreach (string file in files)
        EncryptFile(txtPassword.Text, file);
    }

    private void txtCrypted_DragDrop(object sender, DragEventArgs e)
    {
      var files = (string[])e.Data.GetData(DataFormats.FileDrop);
      foreach (string file in files)
        DecryptFile(txtPassword.Text, file);
    }

    private static void EncryptFile(string password, string srcFile)
    {
      if ((File.GetAttributes(srcFile) & FileAttributes.Directory) == FileAttributes.Directory)
      {
        MessageBox.Show("Its a directory");
        return;
      }
      AesCryptoProvider.EncryptFile(AesCryptoProvider.GetKey(password), srcFile);
      MessageBox.Show("Encrypted file created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static void DecryptFile(string password, string srcFile)
    {
      if ((File.GetAttributes(srcFile) & FileAttributes.Directory) == FileAttributes.Directory)
      {
        MessageBox.Show("Its a directory");
        return;
      }
      try
      {
        AesCryptoProvider.DecryptFile(AesCryptoProvider.GetKey(password), srcFile);
        MessageBox.Show("Decrypted file created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception)
      {
        MessageBox.Show("Error decrypting data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }
  }
}
