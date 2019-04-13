using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Sync_File_Client_Windows
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            CreateAuthenticationInfoFile("");
            //Start program here
        }

        private void CreateAuthenticationInfoFile(string token)
        {
            string ecryptionString = EncryptionHelper.Encrypt(token);
            try
            {
                using (FileStream fs = new FileStream(Config.AuthenticationInfoFilePath, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, ecryptionString);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to create token file", "Take an error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
