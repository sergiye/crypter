using System;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace XtzCrypter
{
  public partial class MainForm : Form
  {
    private static string _appFileExt = ".xtz";

    public MainForm()
    {
      InitializeComponent();
      Icon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule.FileName);
    }

    private void txtPassword_TextChanged(object sender, EventArgs e)
    {
      //lblStrength.Text = txtPassword.Text;
    }

    private void btnPlain_Click(object sender, EventArgs e)
    {
      txtCrypted.Text = txtPlain.Text.Encrypt(CryptingHelper.GetKey(txtPassword.Text));
    }

    private void btnCrypted_Click(object sender, EventArgs e)
    {
      try
      {
        txtPlain.Text = txtCrypted.Text.Decrypt(CryptingHelper.GetKey(txtPassword.Text));
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
                  RestoreDirectory = true,
                  DefaultExt = _appFileExt,
                  Filter = "Encrypted files (*.xtz)|*.xtz"
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
      var destFile = srcFile + _appFileExt;
      AesCryptoProvider.EncryptFile(CryptingHelper.GetKey(password), srcFile, destFile);
      MessageBox.Show("Encrypted file created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private static void DecryptFile(string password, string srcFile)
    {
      if ((File.GetAttributes(srcFile) & FileAttributes.Directory) == FileAttributes.Directory)
      {
        MessageBox.Show("Its a directory");
        return;
      }
      if (!srcFile.EndsWith(_appFileExt))
      {
        MessageBox.Show(string.Format("File '{0}' is not valid encrypted data", srcFile), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      var destFile = srcFile.Substring(0, srcFile.LastIndexOf(_appFileExt, StringComparison.OrdinalIgnoreCase));
      try
      {
        AesCryptoProvider.DecryptFile(CryptingHelper.GetKey(password), srcFile, destFile);
        MessageBox.Show("Decrypted file created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception)
      {
        MessageBox.Show("Error decrypting data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }
  }
}
