using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
            string token = null;
            string username = textBox1.Text.Trim().ToLower();
            string password = textBox2.Text.Trim().ToLower();
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and password cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var formContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });

            try
            {
                var response = Config.Client.PostAsync("login", formContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    token = response.Headers.GetValues("Authorization").FirstOrDefault();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to send request, check your connection and try again", "Take an error!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            CreateAuthenticationInfoFile(token);
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
