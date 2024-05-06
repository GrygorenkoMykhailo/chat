using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Client.Login;
using Client.classes;
using System.Text.Json.Serialization;

namespace Client
{
    public partial class Login : Form
    {
        

        public Login()
        {

            InitializeComponent();
            

            UserField.Text = "Enter Email";
            UserField.ForeColor = System.Drawing.Color.Gray;

            PassField.Text = "Enter Password";
            PassField.ForeColor = System.Drawing.Color.Gray;
            PassField.UseSystemPasswordChar = false;

            UserField.Enter += UserField_Enter;
            UserField.Leave += UserField_Leave;

            PassField.Enter += PassField_Enter;
            PassField.Leave += PassField_Leave;
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

        private void PassField_Leave(object sender, EventArgs e)
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
            NetworkStream stream = InitializeNetworkStream();
            if (stream == null)
            {
                MessageBox.Show("Ошибка при инициализации сетевого соединения.");
                return;
            }
            try
            {
                var authorizationRequest = new
                {
                    Type = "AUTHORIZATION",
                    Content = JsonSerializer.Serialize(new
                    {
                        Email = email,
                        Password = password
                    })
                };

                string requestJson = JsonSerializer.Serialize(authorizationRequest);
                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                await stream.WriteAsync(requestBytes, 0, requestBytes.Length);

                byte[] buffer = new byte[1024 * 48];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                var response = JsonSerializer.Deserialize<Response>(responseJson);

                if (response.StatusCode == (int)System.Net.HttpStatusCode.OK)
                {
                    var authData = JsonSerializer.Deserialize<User>(response.Content, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    });

                    MessageBox.Show("Авторизация успешна!");
                    this.Hide();
                    MainForm mainForm = new MainForm(authData);
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
            stream.Close();
        }
       


        private void LoginButton_Click(object sender, EventArgs e)
        {
            string email = UserField.Text;
            string password = PassField.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите электронную почту и пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SendAuthorizationRequest(email, password);
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
