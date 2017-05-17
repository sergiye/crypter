using System;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Crypter;

namespace CrypterUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Icon = Icon.ExtractAssociatedIcon(Process.GetCurrentProcess().MainModule.FileName);
        }

        private SymmKeyInfo CurrentKey
        {
            get { return CryptoProvider.GetKey(txtPassword.Text); }
        }

        private void btnPlain_Click(object sender, EventArgs e)
        {
            txtCrypted.Text = txtPlain.Text.Encrypt(CurrentKey);
        }

        private void btnCrypted_Click(object sender, EventArgs e)
        {
            try
            {
                txtPlain.Text = txtCrypted.Text.Decrypt(CurrentKey);
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
            EncryptFile(dlg.FileName);
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
            DecryptFile(dlg.FileName);
        }

        private void txtDropTarget_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void txtPlain_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
                EncryptFile(file);
        }

        private void txtCrypted_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
                DecryptFile(file);
        }

        private void EncryptFile(string srcFile)
        {
            if ((File.GetAttributes(srcFile) & FileAttributes.Directory) == FileAttributes.Directory)
            {
                MessageBox.Show("Its a directory");
                return;
            }
            try
            {
                CryptoProvider.EncryptFileMem(CurrentKey, srcFile);
                MessageBox.Show("Encrypted file created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error encrypting file: " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DecryptFile(string srcFile)
        {
            if ((File.GetAttributes(srcFile) & FileAttributes.Directory) == FileAttributes.Directory)
            {
                MessageBox.Show("Its a directory");
                return;
            }
            try
            {
                CryptoProvider.DecryptFileMem(CurrentKey, srcFile);
                MessageBox.Show("Decrypted file created", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error decrypting file: " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
