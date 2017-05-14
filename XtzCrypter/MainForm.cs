using System;
using System.Drawing;
using System.Diagnostics;
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

    private SymmKeyInfo GetKey()
    {
      SymmKeyInfo key = null;
      var pass = txtPassword.Text;
      if (!string.IsNullOrWhiteSpace(pass))
      {
        var passData = pass.GetHash(CryptingHelper.HashType.Sha256).FromBase64Bytes();
        var keyData = new byte[24];
        var ivData = new byte[16];
        Buffer.BlockCopy(passData, 0, keyData, 0, keyData.Length);
        Buffer.BlockCopy(passData, passData.Length - ivData.Length, ivData, 0, ivData.Length);
        key = new SymmKeyInfo(keyData.ToBase64(), ivData.ToBase64());
      }
      return key;
    }

    private void txtPassword_TextChanged(object sender, EventArgs e)
    {
      //lblStrength.Text = txtPassword.Text;
    }

    private void btnPlain_Click(object sender, EventArgs e)
    {
      txtCrypted.Text = txtPlain.Text.Encrypt(GetKey());
    }

    private void btnCrypted_Click(object sender, EventArgs e)
    {
      try
      {
        txtPlain.Text = txtCrypted.Text.Decrypt(GetKey());
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
      var srcFileName = dlg.FileName;
      var destFileName = srcFileName + _appFileExt;
      AesCryptoProvider.EncryptFile(GetKey(), srcFileName, destFileName);
      MessageBox.Show("Encrypted file created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
      try
      {
        var srcFileName = dlg.FileName;
        if (!srcFileName.EndsWith(_appFileExt))
        {
          MessageBox.Show("File is not recognized as valid encrypted data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        var destFileName = srcFileName.Substring(0, srcFileName.LastIndexOf(_appFileExt, StringComparison.OrdinalIgnoreCase));
        AesCryptoProvider.DecryptFile(GetKey(), srcFileName, destFileName);
        MessageBox.Show("Encrypted file created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception)
      {
        MessageBox.Show("Error decrypting data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }
  }
}
