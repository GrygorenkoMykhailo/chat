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
        private TcpClient _client;
        private NetworkStream _stream;
        private const string _serverAddress = "127.0.0.1"; 
        private const int _serverPort = 12345; 

        public Login()
        {
            InitializeComponent();
        }

        

        private async void ConnectToServer()
        {
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(_serverAddress, _serverPort);
                _stream = _client.GetStream();

                MessageBox.Show("Подключение установлено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения: {ex.Message}");
            }
        }

        private async void SendAuthorizationRequest(string email, string password)
        {
            try
            {
                // Создайте запрос авторизации в формате JSON
                var authorizationRequest = new
                {
                    Type = "AUTHORIZATION",
                    Content = new
                    {
                        Email = email,
                        Password = password
                    }
                };

                // Сериализуйте запрос в строку JSON
                string requestJson = JsonSerializer.Serialize(authorizationRequest);

                byte[] requestBytes = Encoding.UTF8.GetBytes(requestJson);
                await _stream.WriteAsync(requestBytes, 0, requestBytes.Length);

                byte[] buffer = new byte[1024];
                int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                string responseJson = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                // Десериализуйте ответ JSON?
                var response = JsonSerializer.Deserialize<Response>(responseJson);

                if (response.StatusCode == (int)System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Авторизация успешна!");

                    this.Hide();
                    Form1 form = new Form1();
                    form.Show();
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

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string email = UserField.Text;
            string password = PassField.Text;

            SendAuthorizationRequest(email, password);
        }
        private void RegisterButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.Show();
        }

        // Класс ответа сервера
        public class Response
        {
            public int StatusCode { get; set; }
            public string Content { get; set; }
        }

        // Закрывайте соединение при завершении работы формы
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_client != null)
            {
                _client.Close();
            }
        }
    }
}
