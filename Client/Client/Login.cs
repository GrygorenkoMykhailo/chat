using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : Form
    {
       
        private NetworkStream _stream;
    
        public Login()
        {
            InitializeComponent();
            InitializeNetworkStream();

            UserField.Text = "Enter Email";
            UserField.ForeColor = Color.Gray;
            PassField.Text = "Enter Password";
            PassField.ForeColor = Color.Gray;
            PassField.UseSystemPasswordChar = false;


        }
        private void InitializeNetworkStream()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 8000);
                _stream = client.GetStream();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize network stream: {ex.Message}");
            }
        }

            private void UserField_Enter(object sender, EventArgs e)
        {
            if (UserField.Text == "Enter UserName")
            {
               UserField.Text = "";
                UserField.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void UserField_Leave(object sender, EventArgs e)
        {
            if (UserField.Text == "")
            {
                UserField.Text = "Enter UserName";
                UserField.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void PassField_Enter(object sender, EventArgs e)
        {
            if (PassField.Text == "Enter Password")
            {
                PassField.Text = "";
                PassField.ForeColor = System.Drawing.Color.Black;
                PassField.UseSystemPasswordChar = true;
            }
        }

        private void Pass1RegisterField_Leave(object sender, EventArgs e)
        {
            if (PassField.Text == "")
            {
                PassField.Text = "Enter Password";
                PassField.ForeColor = System.Drawing.Color.Gray;
                PassField.UseSystemPasswordChar = false;
            }
        }

        private async void SendAuthorizationRequest(string email, string password)
        {
            try
            {
                var authorizationRequest = new
                {
                    Type = "AUTHORIZATION",
                    Content = new
                    {
                        Email = email,
                        Password = password
                    }
                };

                string requestJson = JsonSerializer.Serialize(authorizationRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                await _stream.WriteAsync(requestBytes, 0, requestBytes.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                var response = JsonSerializer.Deserialize<Response>(responseJson);

                if (response.StatusCode == (int)System.Net.HttpStatusCode.OK)
                {
                    SaveAuthToken(response.Content);

                    
                    MessageBox.Show("Авторизация успешна!");
                    this.Hide();
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Авторизация не успешна.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке запроса авторизации: {ex.Message}");
            }
        }

        private void SaveAuthToken(string token)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "authToken.txt");

            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty);
            }

            File.WriteAllText(filePath, token);
        }


        private void RegisterButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.Show();
        }

        public class Response
        {
            public int StatusCode { get; set; }
            public string Content { get; set; }
        }

     
    }
}
