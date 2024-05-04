﻿using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Register : Form
    {
       

        public Register()
        {
            InitializeComponent();

            UserRegisterField.Text = "Enter UserName";
            UserRegisterField.ForeColor = System.Drawing.Color.Gray;

            EmailRegisterField.Text = "Enter Email";
            EmailRegisterField.ForeColor = System.Drawing.Color.Gray;

            Pass1RegisterField.Text = "Enter Password";
            Pass1RegisterField.ForeColor = System.Drawing.Color.Gray;
            Pass1RegisterField.UseSystemPasswordChar = false;

            Pass2RegisterField.Text = "Confirm Password";
            Pass2RegisterField.ForeColor = System.Drawing.Color.Gray;
            Pass2RegisterField.UseSystemPasswordChar = false;

            UserRegisterField.Enter += UserRegisterField_Enter;
            UserRegisterField.Leave += UserRegisterField_Leave;

            EmailRegisterField.Enter += EmailRegisterField_Enter;
            EmailRegisterField.Leave += EmailRegisterField_Leave;

            Pass1RegisterField.Enter += Pass1RegisterField_Enter;
            Pass1RegisterField.Leave += Pass1RegisterField_Leave;

            Pass2RegisterField.Enter += Pass2RegisterField_Enter;
            Pass2RegisterField.Leave += Pass2RegisterField_Leave;

            RegisterNewButton.Click += RegisterButton_Click;
        }

        private NetworkStream InitializeNetworkStream()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 8000);
                NetworkStream stream = client.GetStream();
                return stream;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации сетевого соединения: {ex.Message}");
                return null;
            }
        }
        private void UserRegisterField_Enter(object sender, EventArgs e)
        {
            if (UserRegisterField.Text == "Enter UserName")
            {
                UserRegisterField.Text = "";
                UserRegisterField.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void UserRegisterField_Leave(object sender, EventArgs e)
        {
            if (UserRegisterField.Text == "")
            {
                UserRegisterField.Text = "Enter UserName";
                UserRegisterField.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void EmailRegisterField_Enter(object sender, EventArgs e)
        {
            if (EmailRegisterField.Text == "Enter Email")
            {
                EmailRegisterField.Text = "";
                EmailRegisterField.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void EmailRegisterField_Leave(object sender, EventArgs e)
        {
            if (EmailRegisterField.Text == "")
            {
                EmailRegisterField.Text = "Enter Email";
                EmailRegisterField.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void Pass1RegisterField_Enter(object sender, EventArgs e)
        {
            if (Pass1RegisterField.Text == "Enter Password")
            {
                Pass1RegisterField.Text = "";
                Pass1RegisterField.ForeColor = System.Drawing.Color.Black;
                Pass1RegisterField.UseSystemPasswordChar = true;
            }
        }

        private void Pass1RegisterField_Leave(object sender, EventArgs e)
        {
            if (Pass1RegisterField.Text == "")
            {
                Pass1RegisterField.Text = "Enter Password";
                Pass1RegisterField.ForeColor = System.Drawing.Color.Gray;
                Pass1RegisterField.UseSystemPasswordChar = false;
            }
        }

        private void Pass2RegisterField_Enter(object sender, EventArgs e)
        {
            if (Pass2RegisterField.Text == "Confirm Password")
            {
                Pass2RegisterField.Text = "";
                Pass2RegisterField.ForeColor = System.Drawing.Color.Black;
                Pass2RegisterField.UseSystemPasswordChar = true;
            }
        }

        private void Pass2RegisterField_Leave(object sender, EventArgs e)
        {
            if (Pass2RegisterField.Text == "")
            {
                Pass2RegisterField.Text = "Confirm Password";
                Pass2RegisterField.ForeColor = System.Drawing.Color.Gray;
                Pass2RegisterField.UseSystemPasswordChar = false;
            }
        }

        private async void RegisterButton_Click(object sender, EventArgs e)
        {
            string username = UserRegisterField.Text;
            string email = EmailRegisterField.Text;
            string password1 = Pass1RegisterField.Text;
            string password2 = Pass2RegisterField.Text;

            if (password1 != password2)
            {
                MessageBox.Show("Passwords do not match. Please re-enter the passwords.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await SendRegistrationRequest(username, email, password1);
        }

        private async Task SendRegistrationRequest(string username, string email, string password)
        {
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }
            try
            {

                var registrationRequest = new
                {
                    Type = "REGISTRATION",
                    Content = JsonSerializer.Serialize(new
                    {
                        Username = username,
                        Email = email,
                        Password = password
                    })
                };

                string requestJson = JsonSerializer.Serialize(registrationRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);

                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                var response = JsonSerializer.Deserialize<Response>(responseJson);

                if (response.StatusCode == (int)System.Net.HttpStatusCode.Created)
                {
                    MessageBox.Show("Регистрация успешна!");
                    this.Hide();
                    Login login = new Login();
                    login.Show();
                }
                else
                {
                    MessageBox.Show($"Регистрация не успешна. Код ошибки: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке запроса регистрации: {ex.Message}");
            }
            stream.Close();
        }

        public class Response
        {
            public int StatusCode { get; set; }
            public string Content { get; set; }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}
