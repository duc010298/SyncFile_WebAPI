using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sync_File_Client_Windows
{
    public partial class AuthenticationForm : Form
    {
        public AuthenticationForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void AuthenticationForm_Shown(object sender, EventArgs e)
        {
            Thread t = new Thread(RunValidateToken);
            t.Start();
        }

        private void RunValidateToken()
        {
            bool haveToken = CheckExistToken();
            if (!haveToken)
            {
                OpenLoginForm();
                return;
            }
            if (!CheckTokenAvailability().Result)
            {
                OpenLoginForm();
                return;
            }
            //Start program sync
            MessageBox.Show("Done");
        }

        private void OpenLoginForm()
        {
            Thread t = new Thread(OpenForm);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void OpenForm()
        {
            Action close = () => this.Close();
            if (InvokeRequired)
            {
                Invoke(close);
            }
            else
            {
                close();
            }
            Application.Run(new LoginForm());
        }

        private bool CheckExistToken()
        {
            if (!File.Exists(Config.AuthenticationInfoFilePath))
            {
                return false;
            }
            string token = null;
            try
            {
                using (FileStream serializationStream = new FileStream(Config.AuthenticationInfoFilePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    string tokenEncrypted = (string)formatter.Deserialize(serializationStream);
                    token = EncryptionHelper.Decrypt(tokenEncrypted);
                }
            }
            catch (Exception)
            {
                return false;
            }
            Config.Client.DefaultRequestHeaders.Add("Authorization", token);
            return true;
        }

        private async Task<bool> CheckTokenAvailability()
        {
            try
            {
                var response = Config.Client.GetAsync("TestToken").Result;
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content.Equals("Success");
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
